namespace CryptographyWF01
{
    partial class Form1
    {
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.TextBox IDtxt;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.ComboBox cmbEncryptionAlgorithm;
        private System.Windows.Forms.Label lblAlgorithm;
        private System.Windows.Forms.Button btnGenerateKey;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Button btnUploadToDrive;
        private System.Windows.Forms.Button btnDownloadFromDrive;

        private void InitializeComponent()
        {
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.IDtxt = new System.Windows.Forms.TextBox();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.cmbEncryptionAlgorithm = new System.Windows.Forms.ComboBox();
            this.lblAlgorithm = new System.Windows.Forms.Label();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.lblKey = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.btnGenerateKey = new System.Windows.Forms.Button();
            this.btnUploadToDrive = new System.Windows.Forms.Button();
            this.btnDownloadFromDrive = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(15, 30);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(300, 20);
            this.txtFilePath.TabIndex = 4;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(330, 28);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "Обзор...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(15, 76);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(300, 20);
            this.txtKey.TabIndex = 2;
            this.txtKey.Text = "Введите ключ...";
            this.txtKey.Enter += new System.EventHandler(this.txtKey_Enter);
            this.txtKey.Leave += new System.EventHandler(this.txtKey_Leave);
            // 
            // IDtxt
            // 
            this.IDtxt.Location = new System.Drawing.Point(15, 235);
            this.IDtxt.Name = "IDtxt";
            this.IDtxt.Size = new System.Drawing.Size(300, 20);
            this.IDtxt.TabIndex = 6;
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(15, 170);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(150, 23);
            this.btnEncrypt.TabIndex = 1;
            this.btnEncrypt.Text = "Зашифровать";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(180, 170);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(150, 23);
            this.btnDecrypt.TabIndex = 0;
            this.btnDecrypt.Text = "Расшифровать";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // cmbEncryptionAlgorithm
            // 
            this.cmbEncryptionAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEncryptionAlgorithm.FormattingEnabled = true;
            this.cmbEncryptionAlgorithm.Location = new System.Drawing.Point(15, 126);
            this.cmbEncryptionAlgorithm.Name = "cmbEncryptionAlgorithm";
            this.cmbEncryptionAlgorithm.Size = new System.Drawing.Size(300, 21);
            this.cmbEncryptionAlgorithm.TabIndex = 5;
            // 
            // lblAlgorithm
            // 
            this.lblAlgorithm.AutoSize = true;
            this.lblAlgorithm.Location = new System.Drawing.Point(12, 110);
            this.lblAlgorithm.Name = "lblAlgorithm";
            this.lblAlgorithm.Size = new System.Drawing.Size(111, 13);
            this.lblAlgorithm.TabIndex = 6;
            this.lblAlgorithm.Text = "Выберите алгоритм:";
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(12, 12);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(77, 13);
            this.lblFilePath.TabIndex = 7;
            this.lblFilePath.Text = "Путь к файлу:";
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(12, 60);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(36, 13);
            this.lblKey.TabIndex = 8;
            this.lblKey.Text = "Ключ:";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(12, 225);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(36, 13);
            this.lblID.TabIndex = 8;
            this.lblID.Text = "ID:";
            // 
            // btnGenerateKey
            // 
            this.btnGenerateKey.Location = new System.Drawing.Point(330, 74);
            this.btnGenerateKey.Name = "btnGenerateKey";
            this.btnGenerateKey.Size = new System.Drawing.Size(75, 23);
            this.btnGenerateKey.TabIndex = 9;
            this.btnGenerateKey.Text = "Генерация";
            this.btnGenerateKey.UseVisualStyleBackColor = true;
            this.btnGenerateKey.Click += new System.EventHandler(this.btnGenerateKey_Click);
            // 
            // btnUploadToDrive
            // 
            this.btnUploadToDrive.Location = new System.Drawing.Point(15, 200);
            this.btnUploadToDrive.Name = "btnUploadToDrive";
            this.btnUploadToDrive.Size = new System.Drawing.Size(150, 23);
            this.btnUploadToDrive.TabIndex = 10;
            this.btnUploadToDrive.Text = "Загрузить на Google Drive";
            this.btnUploadToDrive.UseVisualStyleBackColor = true;
            this.btnUploadToDrive.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnDownloadFromDrive
            // 
            this.btnDownloadFromDrive.Location = new System.Drawing.Point(180, 200);
            this.btnDownloadFromDrive.Name = "btnDownloadFromDrive";
            this.btnDownloadFromDrive.Size = new System.Drawing.Size(150, 23);
            this.btnDownloadFromDrive.TabIndex = 11;
            this.btnDownloadFromDrive.Text = "Скачать с Google Drive";
            this.btnDownloadFromDrive.UseVisualStyleBackColor = true;
            this.btnDownloadFromDrive.Click += new System.EventHandler(this.btnDownloadFromDrive_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(420, 260);
            this.Controls.Add(this.btnDownloadFromDrive);
            this.Controls.Add(this.btnUploadToDrive);
            this.Controls.Add(this.btnGenerateKey);
            this.Controls.Add(this.lblKey);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.lblAlgorithm);
            this.Controls.Add(this.cmbEncryptionAlgorithm);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.IDtxt);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFilePath);
            this.Name = "Form1";
            this.Text = "CryptographyWF01";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
