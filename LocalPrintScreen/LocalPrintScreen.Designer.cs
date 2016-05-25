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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocalPrintScreen));
            this.pictureBoxForReceiving = new System.Windows.Forms.PictureBox();
            this.buttonStartTranslation = new System.Windows.Forms.Button();
            this.buttonFinishTranslation = new System.Windows.Forms.Button();
            this.textBoxServerIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonConnectToServer = new System.Windows.Forms.Button();
            this.numericUpDownForQuality = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownForCadrsPerSecond = new System.Windows.Forms.NumericUpDown();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxForName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSendMessage = new System.Windows.Forms.Button();
            this.textBoxForMessage = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForReceiving)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownForQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownForCadrsPerSecond)).BeginInit();
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
            this.buttonConnectToServer.Location = new System.Drawing.Point(746, 123);
            this.buttonConnectToServer.Name = "buttonConnectToServer";
            this.buttonConnectToServer.Size = new System.Drawing.Size(119, 37);
            this.buttonConnectToServer.TabIndex = 5;
            this.buttonConnectToServer.Text = "Connect to server";
            this.buttonConnectToServer.UseVisualStyleBackColor = true;
            this.buttonConnectToServer.Click += new System.EventHandler(this.buttonConnectToServer_Click);
            // 
            // numericUpDownForQuality
            // 
            this.numericUpDownForQuality.Location = new System.Drawing.Point(767, 235);
            this.numericUpDownForQuality.Name = "numericUpDownForQuality";
            this.numericUpDownForQuality.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownForQuality.TabIndex = 6;
            this.numericUpDownForQuality.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // numericUpDownForCadrsPerSecond
            // 
            this.numericUpDownForCadrsPerSecond.Location = new System.Drawing.Point(767, 187);
            this.numericUpDownForCadrsPerSecond.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDownForCadrsPerSecond.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownForCadrsPerSecond.Name = "numericUpDownForCadrsPerSecond";
            this.numericUpDownForCadrsPerSecond.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownForCadrsPerSecond.TabIndex = 7;
            this.numericUpDownForCadrsPerSecond.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(900, 36);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(270, 533);
            this.textBox2.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(777, 392);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 43);
            this.button1.TabIndex = 9;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(734, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Name:";
            // 
            // textBoxForName
            // 
            this.textBoxForName.Location = new System.Drawing.Point(734, 94);
            this.textBoxForName.Name = "textBoxForName";
            this.textBoxForName.Size = new System.Drawing.Size(145, 20);
            this.textBoxForName.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(900, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Chat:";
            // 
            // buttonSendMessage
            // 
            this.buttonSendMessage.Location = new System.Drawing.Point(746, 493);
            this.buttonSendMessage.Name = "buttonSendMessage";
            this.buttonSendMessage.Size = new System.Drawing.Size(133, 37);
            this.buttonSendMessage.TabIndex = 13;
            this.buttonSendMessage.Text = "Send message";
            this.buttonSendMessage.UseVisualStyleBackColor = true;
            this.buttonSendMessage.Click += new System.EventHandler(this.buttonSendMessage_Click);
            // 
            // textBoxForMessage
            // 
            this.textBoxForMessage.Location = new System.Drawing.Point(22, 469);
            this.textBoxForMessage.Multiline = true;
            this.textBoxForMessage.Name = "textBoxForMessage";
            this.textBoxForMessage.Size = new System.Drawing.Size(689, 100);
            this.textBoxForMessage.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 453);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Message:";
            // 
            // LocalPrintScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 593);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxForMessage);
            this.Controls.Add(this.buttonSendMessage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxForName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.numericUpDownForCadrsPerSecond);
            this.Controls.Add(this.numericUpDownForQuality);
            this.Controls.Add(this.buttonConnectToServer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxServerIP);
            this.Controls.Add(this.buttonFinishTranslation);
            this.Controls.Add(this.buttonStartTranslation);
            this.Controls.Add(this.pictureBoxForReceiving);
            this.Name = "LocalPrintScreen";
            this.Text = "ScreenTranslator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LocalPrintScreen_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForReceiving)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownForQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownForCadrsPerSecond)).EndInit();
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
        private System.Windows.Forms.NumericUpDown numericUpDownForQuality;
        private System.Windows.Forms.NumericUpDown numericUpDownForCadrsPerSecond;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxForName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSendMessage;
        private System.Windows.Forms.TextBox textBoxForMessage;
        private System.Windows.Forms.Label label4;
    }
}

