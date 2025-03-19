using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace CryptographyWF01
{
    public partial class Form1 : Form
    {
        private DriveService driveService;

        public Form1()
        {
            InitializeComponent();
            InitializeGoogleDrive();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtKey.Text = "Введите ключ...";
            cmbEncryptionAlgorithm.Items.AddRange(new string[] { "AES", "RC4", "Twofish" });
            cmbEncryptionAlgorithm.SelectedIndex = 0;
        }

        private void InitializeGoogleDrive()
        {
            string[] scopes = { DriveService.Scope.DriveFile };
            string applicationName = "CryptographyWF01";

            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore("Drive.Auth.Store")).Result;

                driveService = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = applicationName,
                });
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Все файлы (*.*)|*.*";
                openFileDialog.Title = "Выберите файл";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnGenerateKey_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int keyLength = random.Next(16, 32);
            string generatedKey = GenerateRandomKey(keyLength);
            txtKey.Text = generatedKey;
        }

        private string GenerateRandomKey(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] key = new char[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] data = new byte[length];
                rng.GetBytes(data);

                for (int i = 0; i < key.Length; i++)
                {
                    key[i] = chars[data[i] % chars.Length];
                }
            }
            return new string(key);
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                string filePath = txtFilePath.Text;
                string key = txtKey.Text;

                try
                {
                    string algorithm = cmbEncryptionAlgorithm.SelectedItem.ToString();
                    string encryptedFilePath = EncryptFile(filePath, key, algorithm);
                    UploadToDrive(encryptedFilePath);


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при шифровании: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
            private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                string filePath = txtFilePath.Text;
                string key = txtKey.Text;

                try
                {
                    string algorithm = cmbEncryptionAlgorithm.SelectedItem.ToString();
                    string encryptedFilePath = EncryptFile(filePath, key, algorithm);

                    MessageBox.Show($"Файл успешно зашифрован:\n{encryptedFilePath}",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при шифровании: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDownloadFromDrive_Click(object sender, EventArgs e)
        {
            string fileId = Microsoft.VisualBasic.Interaction.InputBox("Введите ID файла с Google Drive:", "ID файла");

            if (string.IsNullOrEmpty(fileId))
            {
                MessageBox.Show("ID файла не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                DownloadFromDrive(fileId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при скачивании: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                string filePath = txtFilePath.Text;
                string key = txtKey.Text;

                try
                {
                    string algorithm = cmbEncryptionAlgorithm.SelectedItem.ToString();
                    string decryptedFilePath = DecryptFile(filePath, key, algorithm);

                    MessageBox.Show($"Файл успешно расшифрован:\n{decryptedFilePath}",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при расшифровке: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtFilePath.Text) || !File.Exists(txtFilePath.Text))
            {
                MessageBox.Show("Пожалуйста, выберите существующий файл.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(txtKey.Text) || txtKey.Text == "Введите ключ...")
            {
                MessageBox.Show("Пожалуйста, введите ключ для шифрования/расшифровки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtKey.Text.Length < 16)
            {
                MessageBox.Show("Ключ должен быть длиной минимум 16 символов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private string EncryptFile(string filePath, string key, string algorithm)
        {
            byte[] data = File.ReadAllBytes(filePath);

            byte[] encryptedData;

            switch (algorithm)
            {
                case "AES":
                    encryptedData = EncryptAES(data, key);
                    break;
                case "RC4":
                    encryptedData = EncryptRC4(data, key);
                    break;
                case "Twofish":
                    encryptedData = EncryptTwofish(data, key);
                    break;
                default:
                    throw new NotSupportedException($"Алгоритм {algorithm} не поддерживается.");
            }
            string encryptedFilePath = filePath + $".{algorithm}";
            File.WriteAllBytes(encryptedFilePath, encryptedData);
            return encryptedFilePath;
        }


        private string DecryptFile(string filePath, string key, string algorithm)
        {
            byte[] encryptedData = File.ReadAllBytes(filePath);

            byte[] decryptedData;

            switch (algorithm)
            {
                case "AES":
                    decryptedData = DecryptAES(encryptedData, key);
                    break;
                case "RC4":
                    decryptedData = DecryptRC4(encryptedData, key);
                    break;
                case "Twofish":
                    decryptedData = DecryptTwofish(encryptedData, key);
                    break;
                default:
                    throw new NotSupportedException($"Алгоритм {algorithm} не поддерживается.");
            }
            string decryptedFilePath = filePath.Replace(".enc", "_decrypted");
            File.WriteAllBytes(decryptedFilePath, decryptedData);
            return decryptedFilePath;
        }


        private byte[] EncryptAES(byte[] data, string key)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));

                aes.IV = new byte[aes.BlockSize / 8];
                using (var encryptor = aes.CreateEncryptor())
                {
                    return PerformCryptography(data, encryptor);
                }
            }
        }


        private byte[] DecryptAES(byte[] encryptedData, string key)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
                aes.IV = new byte[aes.BlockSize / 8];
                using (var decryptor = aes.CreateDecryptor())
                {
                    return PerformCryptography(encryptedData, decryptor);
                }
            }
        }


        private byte[] EncryptRC4(byte[] data, string key)
        {
            var engine = new RC4Engine();
            var keyParam = new KeyParameter(Encoding.UTF8.GetBytes(key));
            engine.Init(true, keyParam);
            byte[] output = new byte[data.Length];
            return output;
        }


        private byte[] DecryptRC4(byte[] encryptedData, string key)
        {
            var engine = new RC4Engine();
            var keyParam = new KeyParameter(Encoding.UTF8.GetBytes(key));
            engine.Init(false, keyParam);
            byte[] output = new byte[encryptedData.Length];
            engine.ProcessBytes(encryptedData, 0, encryptedData.Length, output, 0);
            return output;
        }


        private byte[] EncryptTwofish(byte[] data, string key)
        {
            var engine = new TwofishEngine();
            var cipher = new BufferedBlockCipher(engine);
            var keyParam = new KeyParameter(Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32)));
            cipher.Init(true, keyParam);
            return cipher.DoFinal(data);
        }


        private byte[] DecryptTwofish(byte[] encryptedData, string key)
        {
            var engine = new TwofishEngine();
            var cipher = new BufferedBlockCipher(engine);
            var keyParam = new KeyParameter(Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32)));

            cipher.Init(false, keyParam);

            return cipher.DoFinal(encryptedData);
        }

        private byte[] PerformCryptography(byte[] data, ICryptoTransform transform)
        {
            using (var ms = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                }
                return ms.ToArray();
            }
        }
        private string UploadToDrive(string filePath)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(filePath)
            };

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var request = driveService.Files.Create(fileMetadata, stream, "application/octet-stream");
                request.Fields = "id";
                var file = request.Upload();

                if (file.Status == Google.Apis.Upload.UploadStatus.Failed)
                {
                    throw new Exception($"Ошибка загрузки файла: {file.Exception}");
                }
                string fileId = request.ResponseBody.Id;
                Clipboard.SetText(fileId);
                IDtxt.Text = fileId;

                MessageBox.Show($"Файл загружен на Google Drive.",
                                "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return fileId;
            }
        }
        private void DownloadFromDrive(string fileId)
        {
            if (driveService == null)
            {
                throw new Exception("Google Drive API не инициализирован.");
            }

            var request = driveService.Files.Get(fileId);
            request.Fields = "name";
            var file = request.Execute();

            if (file == null || string.IsNullOrEmpty(file.Name))
            {
                throw new Exception("Файл не найден или отсутствует имя.");
            }

            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string key = txtKey.Text;
            string algorithm = cmbEncryptionAlgorithm.SelectedItem.ToString();
            string decryptedFilePath = DecryptFile(downloadsPath, key, algorithm);
            if (string.IsNullOrWhiteSpace(decryptedFilePath) || !Directory.Exists(decryptedFilePath))
            {
                throw new Exception($"Некорректный путь для расшифровки: {decryptedFilePath}");
            }
            string destinationPath = Path.Combine(decryptedFilePath, file.Name);
            using (var stream = new MemoryStream())
            {
                request.Download(stream);

                using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                {
                    stream.Position = 0;
                    stream.CopyTo(fileStream);
                }
            }

            MessageBox.Show($"Файл успешно скачан и сохранён в папке 'Загрузки':\n{destinationPath}",
                "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        

        private void txtKey_Enter(object sender, EventArgs e)
        {
            if (txtKey.Text == "Введите ключ...")
            {
                txtKey.Text = "";
            }
        }

        private void txtKey_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKey.Text))
            {
                txtKey.Text = "Введите ключ...";
            }
        }
    }
}

