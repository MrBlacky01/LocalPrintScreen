namespace LocalPrintScreen
{
    partial class LocalPrintScreen
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxForReceiving = new System.Windows.Forms.PictureBox();
            this.buttonStartTranslation = new System.Windows.Forms.Button();
            this.buttonFinishTranslation = new System.Windows.Forms.Button();
            this.textBoxServerIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonConnectToServer = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.textBox2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForReceiving)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxForReceiving
            // 
            this.pictureBoxForReceiving.Location = new System.Drawing.Point(22, 12);
            this.pictureBoxForReceiving.Name = "pictureBoxForReceiving";
            this.pictureBoxForReceiving.Size = new System.Drawing.Size(689, 423);
            this.pictureBoxForReceiving.TabIndex = 0;
            this.pictureBoxForReceiving.TabStop = false;
            // 
            // buttonStartTranslation
            // 
            this.buttonStartTranslation.Location = new System.Drawing.Point(746, 273);
            this.buttonStartTranslation.Name = "buttonStartTranslation";
            this.buttonStartTranslation.Size = new System.Drawing.Size(133, 40);
            this.buttonStartTranslation.TabIndex = 1;
            this.buttonStartTranslation.Text = "Start translation";
            this.buttonStartTranslation.UseVisualStyleBackColor = true;
            this.buttonStartTranslation.Click += new System.EventHandler(this.buttonStartTranslation_Click);
            // 
            // buttonFinishTranslation
            // 
            this.buttonFinishTranslation.Location = new System.Drawing.Point(746, 338);
            this.buttonFinishTranslation.Name = "buttonFinishTranslation";
            this.buttonFinishTranslation.Size = new System.Drawing.Size(133, 43);
            this.buttonFinishTranslation.TabIndex = 2;
            this.buttonFinishTranslation.Text = "Finish translation";
            this.buttonFinishTranslation.UseVisualStyleBackColor = true;
            this.buttonFinishTranslation.Click += new System.EventHandler(this.buttonFinishTranslation_Click);
            // 
            // textBoxServerIP
            // 
            this.textBoxServerIP.Location = new System.Drawing.Point(734, 50);
            this.textBoxServerIP.Name = "textBoxServerIP";
            this.textBoxServerIP.Size = new System.Drawing.Size(145, 20);
            this.textBoxServerIP.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(731, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter server IP:";
            // 
            // buttonConnectToServer
            // 
            this.buttonConnectToServer.Location = new System.Drawing.Point(746, 76);
            this.buttonConnectToServer.Name = "buttonConnectToServer";
            this.buttonConnectToServer.Size = new System.Drawing.Size(119, 37);
            this.buttonConnectToServer.TabIndex = 5;
            this.buttonConnectToServer.Text = "Connect to server";
            this.buttonConnectToServer.UseVisualStyleBackColor = true;
            this.buttonConnectToServer.Click += new System.EventHandler(this.buttonConnectToServer_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(777, 195);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(64, 20);
            this.numericUpDown1.TabIndex = 6;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(777, 146);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            36,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(64, 20);
            this.numericUpDown2.TabIndex = 7;
            this.numericUpDown2.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(900, 12);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(186, 422);
            this.textBox2.TabIndex = 8;
            // 
            // LocalPrintScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 465);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.buttonConnectToServer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxServerIP);
            this.Controls.Add(this.buttonFinishTranslation);
            this.Controls.Add(this.buttonStartTranslation);
            this.Controls.Add(this.pictureBoxForReceiving);
            this.Name = "LocalPrintScreen";
            this.Text = "ScreenTranslator";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForReceiving)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxForReceiving;
        private System.Windows.Forms.Button buttonStartTranslation;
        private System.Windows.Forms.Button buttonFinishTranslation;
        private System.Windows.Forms.TextBox textBoxServerIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonConnectToServer;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.TextBox textBox2;
    }
}

