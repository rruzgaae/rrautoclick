using System;
using System.IO;
using System.Windows.Forms;

namespace AutoClicker
{
    public partial class HotkeySettingsForm : Form
    {
        // Seçilen hotkey'i tutan özellik
        public string SelectedKey { get; private set; }

        // Config dosyasının yolu
        private const string ConfigFilePath = "config.ini";

        // HotkeySettingsForm constructor'ı
        public HotkeySettingsForm()
        {
            InitializeComponent();
            this.KeyPreview = true; // Formun tuş olaylarını yakalamasını sağlar
        }

        // Form yüklendiğinde çalışacak metod
        private void HotkeySettingsForm_Load(object sender, EventArgs e)
        {
            LoadHotkeyFromConfig(); // Config dosyasından hotkey'i yükle
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
                            SelectedKey = line.Substring(7); // "hotkey=" kısmını atla ve hotkey'i al
                            textBox1.Text = SelectedKey; // TextBox'a hotkey'i yaz
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Config dosyasını okurken bir hata oluştu: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Config dosyası bulunamadı, yeni dosya oluşturulacak.");
            }
        }

        // Hotkey'i config dosyasına kaydeden metod
        private void SaveHotkeyToConfig()
        {
            try
            {
                string[] lines = { "hotkey=" + SelectedKey }; // "hotkey=" ile başlayan satır oluştur
                File.WriteAllLines(ConfigFilePath, lines); // Dosyaya yaz
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Config dosyasına yazarken bir hata oluştu: {ex.Message}");
            }
        }

        // Formda bir tuşa basıldığında çalışacak metod
        private void HotkeySettingsForm_KeyDown(object sender, KeyEventArgs e)
        {
            SelectedKey = e.KeyCode.ToString(); // Basılan tuşu SelectedKey'e kaydet
            textBox1.Text = SelectedKey; // TextBox'a basılan tuşu yaz
        }

        // Kapanma butonuna tıklayınca hotkey'i kaydedip formu kapat
        private void button2_Click(object sender, EventArgs e)
        {
            SaveHotkeyToConfig(); // Hotkey'i config dosyasına kaydet
            this.Close(); // Formu kapat
        }
    }
}