using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace CryptoCloudApp.Encryption
{
    public static class RsaKeyService
    {
        private const int KeySize = 2048;

        public static (string publicKey, string privateKey) GenerateKeyPair()
        {
            try
            {
                using (RSA rsa = RSA.Create(KeySize))
                {
                    string publicKey = rsa.ToXmlString(false);
                    string privateKey = rsa.ToXmlString(true);
                    return (publicKey, privateKey);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка генерации ключей RSA: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public static byte[] EncryptSymmetricKey(string publicKey, string symmetricKey)
        {
            if (string.IsNullOrEmpty(publicKey))
            {
                throw new ArgumentException("Публичный ключ не может быть пустым!");
            }

            if (string.IsNullOrEmpty(symmetricKey))
            {
                throw new ArgumentException("Симметричный ключ не может быть пустым!");
            }

            try
            {
                using (RSA rsa = RSA.Create())
                {
                    rsa.FromXmlString(publicKey);
                    byte[] symmetricKeyBytes = Encoding.UTF8.GetBytes(symmetricKey);
                    return rsa.Encrypt(symmetricKeyBytes, RSAEncryptionPadding.Pkcs1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка шифрования ключа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public static string DecryptSymmetricKey(string privateKey, byte[] encryptedSymmetricKey)
        {
            if (string.IsNullOrEmpty(privateKey))
            {
                throw new ArgumentException("Приватный ключ не может быть пустым!");
            }

            if (encryptedSymmetricKey == null || encryptedSymmetricKey.Length == 0)
            {
                throw new ArgumentException("Зашифрованный ключ не может быть пустым!");
            }

            try
            {
                using (RSA rsa = RSA.Create())
                {
                    rsa.FromXmlString(privateKey);
                    byte[] decryptedBytes = rsa.Decrypt(encryptedSymmetricKey, RSAEncryptionPadding.Pkcs1);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка расшифровки ключа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public static void SaveKeyToFile(string key, string filePath)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Ключ не может быть пустым!");
            }

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("Путь к файлу не может быть пустым!");
            }

            try
            {
                File.WriteAllText(filePath, key);
                MessageBox.Show($"Ключ сохранен в файл: {filePath}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения ключа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public static string LoadKeyFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл с ключом не найден!", filePath);
            }

            try
            {
                return File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки ключа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}