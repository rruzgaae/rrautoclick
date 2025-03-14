namespace AutoClicker
{
    partial class Form1
    {
        /// <summary>
        /// Gerekli designer değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        /// <param name="disposing">true, eğer yönetilen kaynaklar atılacaksa true; aksi halde false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Designer desteği için gerekli metottur.
        /// İçeriğini kod düzenleyiciyle değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            buttonStart = new Button();
            buttonStop = new Button();
            buttonHotkeySetting = new Button();
            buttonHelp = new Button();
            textbox1 = new TextBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            comboBox2 = new ComboBox();
            label3 = new Label();
            comboBox1 = new ComboBox();
            label2 = new Label();
            groupBox2 = new GroupBox();
            numericUpDown1 = new NumericUpDown();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            timer1 = new System.Windows.Forms.Timer(components);
            timer2 = new System.Windows.Forms.Timer(components);
            TimerHotkeyTick = new System.Windows.Forms.Timer(components);
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(12, 145);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(102, 45);
            buttonStart.TabIndex = 4;
            buttonStart.Text = "Başlat ()";
            buttonStart.Click += buttonStart_Click;
            // 
            // buttonStop
            // 
            buttonStop.Enabled = false;
            buttonStop.Location = new Point(120, 145);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(102, 45);
            buttonStop.TabIndex = 5;
            buttonStop.Text = "Durdur ()";
            buttonStop.Click += buttonStop_Click;
            // 
            // buttonHotkeySetting
            // 
            buttonHotkeySetting.Location = new Point(120, 196);
            buttonHotkeySetting.Name = "buttonHotkeySetting";
            buttonHotkeySetting.Size = new Size(102, 23);
            buttonHotkeySetting.TabIndex = 6;
            buttonHotkeySetting.Text = "Kısayol Ayarı";
            buttonHotkeySetting.Click += buttonHotkeySetting_Click;
            // 
            // buttonHelp
            // 
            buttonHelp.Location = new Point(12, 196);
            buttonHelp.Name = "buttonHelp";
            buttonHelp.Size = new Size(102, 23);
            buttonHelp.TabIndex = 7;
            buttonHelp.Text = "Yardım >>";
            buttonHelp.Click += buttonHelp_Click;
            // 
            // textbox1
            // 
            textbox1.Location = new Point(107, 17);
            textbox1.Name = "textbox1";
            textbox1.Size = new Size(115, 23);
            textbox1.TabIndex = 8;
            textbox1.Text = "100";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(89, 21);
            label1.TabIndex = 9;
            label1.Text = "Milisaniye";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBox2);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(12, 46);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(210, 93);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tıklama Türü";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Tek", "Çift" });
            comboBox2.Location = new Point(117, 52);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(87, 23);
            comboBox2.TabIndex = 13;
            comboBox2.Text = "Tek";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Underline);
            label3.Location = new Point(6, 50);
            label3.Name = "label3";
            label3.Size = new Size(93, 21);
            label3.TabIndex = 12;
            label3.Text = "Tıklama tipi:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Sol", "Sağ" });
            comboBox1.Location = new Point(117, 17);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(87, 23);
            comboBox1.TabIndex = 11;
            comboBox1.Text = "Sol";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Underline);
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Size = new Size(108, 21);
            label2.TabIndex = 10;
            label2.Text = "Fare Düğmesi:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(numericUpDown1);
            groupBox2.Controls.Add(radioButton2);
            groupBox2.Controls.Add(radioButton1);
            groupBox2.Location = new Point(228, 46);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(188, 93);
            groupBox2.TabIndex = 14;
            groupBox2.TabStop = false;
            groupBox2.Text = "Tıklama Ayarı";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(98, 24);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(60, 23);
            numericUpDown1.TabIndex = 14;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Font = new Font("Segoe UI", 12F);
            radioButton2.Location = new Point(6, 22);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(86, 25);
            radioButton2.TabIndex = 13;
            radioButton2.TabStop = true;
            radioButton2.Text = "Kez çalış";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Font = new Font("Segoe UI", 12F);
            radioButton1.Location = new Point(6, 56);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(180, 25);
            radioButton1.TabIndex = 12;
            radioButton1.TabStop = true;
            radioButton1.Text = "Durdurana kadar çalış";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // TimerHotkeyTick
            // 
            TimerHotkeyTick.Tick += TimerHotkeyTick_Tick;
            // 
            // Form1
            // 
            ClientSize = new Size(424, 226);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Controls.Add(textbox1);
            Controls.Add(buttonStart);
            Controls.Add(buttonStop);
            Controls.Add(buttonHotkeySetting);
            Controls.Add(buttonHelp);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "rruzgaae's Auto Clicker 1.0";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonHotkeySetting;
        private System.Windows.Forms.Button buttonHelp;
        private TextBox textbox1;
        private Label label1;
        private GroupBox groupBox1;
        private Label label2;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private Label label3;
        private GroupBox groupBox2;
        private NumericUpDown numericUpDown1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer TimerHotkeyTick;
    }
}
