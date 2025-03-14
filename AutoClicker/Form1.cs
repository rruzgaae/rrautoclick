using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoClicker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Hotkey atama zamanını tutacak değişken
        private DateTime lastHotkeySetTime = DateTime.MinValue;

        // Otomatik tıklama durumunu kontrol eden değişken
        bool click = false;

        // Kullanıcının belirlediği hotkey'i tutan değişken
        private string hotkey = "";

        // Config dosyasının yolu
        private const string ConfigFilePath = "config.ini";

        // Mouse olaylarını simüle etmek için Windows API fonksiyonları
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo);

        // Klavye tuşlarının durumunu kontrol etmek için Windows API fonksiyonu
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        // Mouse sol tık basma ve bırakma olayları için sabitler
        const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        const uint MOUSEEVENTF_LEFTUP = 0x04;

        // Mouse sağ tık basma ve bırakma olayları için sabitler
        const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        const uint MOUSEEVENTF_RIGHTUP = 0x10;

        // Form yüklendiğinde çalışacak metod
        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true; // Varsayılan olarak ilk radio buton seçili
            LoadHotkeyFromConfig(); // Config dosyasından hotkey'i yükle
            TimerHotkeyTick.Start(); // Hotkey kontrol timer'ını başlat
        }

        // Config dosyasından hotkey'i yükleyen metod
        private void LoadHotkeyFromConfig()
        {
            if (File.Exists(ConfigFilePath)) // Config dosyası var mı kontrol et
            {
                try
                {
                    string[] lines = File.ReadAllLines(ConfigFilePath); // Dosyayı satır satır oku
                    foreach (string line in lines)
                    {
                        if (line.StartsWith("hotkey=")) // "hotkey=" ile başlayan satırı bul
                        {
                            hotkey = line.Substring(7); // "hotkey=" kısmını atla ve hotkey'i al
                            buttonStart.Text = $"Başlat ({hotkey})"; // Buton metnini güncelle
                            buttonStop.Text = $"Durdur ({hotkey})";
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
            }
        }

        // Hotkey ayarları butonuna tıklanınca çalışacak metod
        private void buttonHotkeySetting_Click(object sender, EventArgs e)
        {
            HotkeySettingsForm hotkeyForm = new HotkeySettingsForm(); // Hotkey ayarları formunu aç
            hotkeyForm.ShowDialog();

            if (!string.IsNullOrEmpty(hotkeyForm.SelectedKey)) // Eğer bir hotkey seçildiyse
            {
                hotkey = hotkeyForm.SelectedKey; // Yeni hotkey'i kaydet
                SaveHotkeyToConfig(); // Yeni hotkey'i config dosyasına kaydet
                buttonStart.Text = $"Başlat ({hotkey})"; // Buton metnini güncelle
                buttonStop.Text = $"Durdur ({hotkey})";
                TimerHotkeyTick.Start(); // Hotkey kontrol timer'ını başlat
            }
            else
            {
                TimerHotkeyTick.Stop(); // Hotkey seçilmediyse timer'ı durdur
            }
        }

        // Hotkey'i config dosyasına kaydeden metod
        private void SaveHotkeyToConfig()
        {
            try
            {
                string[] lines = { "hotkey=" + hotkey }; // Hotkey'i dosyaya yaz
                File.WriteAllLines(ConfigFilePath, lines);
            }
            catch (Exception ex)
            {
            }
        }

        // Başlat butonuna tıklayınca çalışacak metod
        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (IsHotkeyFormOpen()) return; // Eğer hotkey ayarları formu açıksa işlem yapma

            click = true; // Otomatik tıklamayı başlat
            buttonStop.Enabled = true; // Durdur butonunu aktif et
            buttonStart.Enabled = false; // Başlat butonunu devre dışı bırak

            if (radioButton1.Checked) // Eğer ilk radio buton seçiliyse
            {
                startautoclick(); // Sürekli tıklama modunu başlat
            }
            else if (radioButton2.Checked) // Eğer ikinci radio buton seçiliyse
            {
                startautoclickfor(); // Belirli sayıda tıklama modunu başlat
            }
        }

        // Durdur butonuna tıklayınca çalışacak metod
        private void buttonStop_Click(object sender, EventArgs e)
        {
            click = false; // Otomatik tıklamayı durdur
            buttonStop.Enabled = false; // Durdur butonunu devre dışı bırak
            buttonStart.Enabled = true; // Başlat butonunu aktif et
            stopautoclick(); // Otomatik tıklamayı durdur
        }

        // Sürekli tıklama modunu başlatan metod
        private void startautoclick()
        {
            int delay = 0;
            if (string.IsNullOrEmpty(textbox1.Text))
            {
                stopautoclick();
                MessageBox.Show("Gecikme boş olamaz.");
                return;
            }
            else if (textbox1.Text == "0")
            {
                stopautoclick();
                MessageBox.Show("Gecikme 0 olamaz.");
                return;
            }
            delay = Convert.ToInt32(textbox1.Text);
            timer1.Interval = delay; // Timer'ın aralığını ayarla
            timer1.Start(); // Timer'ı başlat
        }

        // Otomatik tıklamayı durduran metod
        private void stopautoclick()
        {
            buttonStop.Enabled = false; // Durdur butonunu devre dışı bırak
            buttonStart.Enabled = true; // Başlat butonunu aktif et
            timer1.Stop(); // Timer'ı durdur
            timer2.Stop(); // İkinci timer'ı durdur
        }

        // Timer'ın her tikinde çalışacak metod
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsHotkeyFormOpen() || !click) return; // Eğer hotkey ayarları formu açıksa veya tıklama durdurulmuşsa işlem yapma

            if (comboBox1.SelectedIndex == 0) // Sol tıklama
            {
                if (comboBox2.SelectedIndex == 0) // Tek tıklama
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, IntPtr.Zero); // Sol tık bas
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, IntPtr.Zero); // Sol tık bırak
                }
                else // Çift tıklama
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, IntPtr.Zero); // Sol tık bas
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, IntPtr.Zero); // Sol tık bırak
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, IntPtr.Zero); // Sol tık bas
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, IntPtr.Zero); // Sol tık bırak
                }
            }
            else if (comboBox1.SelectedIndex == 1) // Sağ tıklama
            {
                if (comboBox2.SelectedIndex == 0) // Tek tıklama
                {
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, IntPtr.Zero); // Sağ tık bas
                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, IntPtr.Zero); // Sağ tık bırak
                }
                else // Çift tıklama
                {
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, IntPtr.Zero); // Sağ tık bas
                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, IntPtr.Zero); // Sağ tık bırak
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, IntPtr.Zero); // Sağ tık bas
                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, IntPtr.Zero); // Sağ tık bırak
                }
            }
        }

        // Belirli sayıda tıklama modunu başlatan metod
        private void startautoclickfor()
        {
            decimal clickCount = numericUpDown1.Value; // Tıklama sayısını al
            int delay = 0;
            if (string.IsNullOrEmpty(textbox1.Text))
            {
                stopautoclick();
                MessageBox.Show("Gecikme boş olamaz.");
                return;
            }
            else if (textbox1.Text == "0")
            {
                stopautoclick();
                MessageBox.Show("Gecikme 0 olamaz.");
                return;
            }
            delay = Convert.ToInt32(textbox1.Text);
            if (clickCount <= 0) // Eğer tıklama sayısı 0 veya daha azsa
            {
                MessageBox.Show("0 kere tıklanamaz.");
                stopautoclick(); // Otomatik tıklamayı durdur
                return;
            }

            Task.Run(() => // Yeni bir thread'de çalıştır
            {
                for (int i = 0; i < clickCount && click; i++) // Belirtilen sayıda tıklama yap
                {
                    if (IsHotkeyFormOpen()) break; // Eğer hotkey ayarları formu açıksa döngüyü durdur

                    if (comboBox1.SelectedIndex == 0) // Sol tıklama
                    {
                        if (comboBox2.SelectedIndex == 0) // Tek tıklama
                        {
                            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, IntPtr.Zero); // Sol tık bas
                            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, IntPtr.Zero); // Sol tık bırak
                        }
                        else // Çift tıklama
                        {
                            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, IntPtr.Zero); // Sol tık bas
                            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, IntPtr.Zero); // Sol tık bırak
                            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, IntPtr.Zero); // Sol tık bas
                            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, IntPtr.Zero); // Sol tık bırak
                        }
                    }
                    else if (comboBox1.SelectedIndex == 1) // Sağ tıklama
                    {
                        if (comboBox2.SelectedIndex == 0) // Tek tıklama
                        {
                            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, IntPtr.Zero); // Sağ tık bas
                            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, IntPtr.Zero); // Sağ tık bırak
                        }
                        else // Çift tıklama
                        {
                            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, IntPtr.Zero); // Sağ tık bas
                            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, IntPtr.Zero); // Sağ tık bırak
                            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, IntPtr.Zero); // Sağ tık bas
                            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, IntPtr.Zero); // Sağ tık bırak
                        }
                    }

                    Thread.Sleep(delay); // Belirtilen gecikme süresi kadar bekle
                }
                Invoke((MethodInvoker)(() => buttonStop_Click(null, null))); // Durdur butonuna tıkla
            });
        }

        // Hotkey ayarları formunun açık olup olmadığını kontrol eden metod
        private bool IsHotkeyFormOpen()
        {
            return Application.OpenForms["HotkeySettingsForm"] != null;
        }

        // Hotkey kontrol timer'ının her tikinde çalışacak metod
        private void TimerHotkeyTick_Tick(object sender, EventArgs e)
        {
            if (IsHotkeyFormOpen()) return; // Eğer hotkey ayarları formu açıksa işlem yapma

            // Hotkey ataması yapıldıktan sonra 1 saniye boyunca hotkey kontrolünü devre dışı bırak
            if ((DateTime.Now - lastHotkeySetTime).TotalSeconds < 1) return;

            // Hotkey'i kontrol et
            if (!string.IsNullOrEmpty(hotkey))
            {
                try
                {
                    Keys key = (Keys)Enum.Parse(typeof(Keys), hotkey); // Hotkey'i Keys enum'ına çevir
                    if ((GetAsyncKeyState((int)key) & 0x8000) != 0) // Tuşa basıldığını kontrol et
                    {
                        // Hotkey'e basıldığında, durumu kontrol et
                        if (!click)  // Eğer otomatik tıklama başlamamışsa, başlat
                        {
                            buttonStart_Click(null, null);
                        }
                        else  // Eğer tıklama zaten başlatılmışsa, durdur
                        {
                            buttonStop_Click(null, null);
                        }

                        // Hotkey'in tekrar tetiklenmemesi için bir süre bekleyelim
                        Thread.Sleep(300); // 300 ms bekle
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hotkey kontrolü sırasında bir hata oluştu: {ex.Message}");
                }
            }
        }

        // Form kapanırken çalışacak metod
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopautoclick(); // Otomatik tıklamayı durdur
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            string git = "https://github.com/rruzgaae/rrautoclick/tree/main";

            // Varsayılan tarayıcıda URL'yi aç
            Process.Start(new ProcessStartInfo(git) { UseShellExecute = true });
        }
    }
}