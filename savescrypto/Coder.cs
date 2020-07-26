using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace savescrypto
{
    class Coder
    {
        private const string passwd = "WeDidntSecureThisVeryWell!!1";

        public bool JsonToSaveFile(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }

            string save_dir = Path.GetDirectoryName(path);

            string text = File.ReadAllText(path);

            string raw_data = encrypt(text, passwd);

            File.WriteAllText(Path.Combine(save_dir, "savegame"), raw_data);

            return true;
        }

        private string encrypt(string text, string passwd = null)
        {
            byte[] bytes = Encoding.UTF8.GetBytes("pemgail9uzpgzl88");
            byte[] bytes2 = Encoding.UTF8.GetBytes(text);
            byte[] bytes3 = new PasswordDeriveBytes(passwd, null).GetBytes(32);

            ICryptoTransform transform = new RijndaelManaged
            {
                Mode = CipherMode.CBC
            }.CreateEncryptor(bytes3, bytes);

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
            cryptoStream.Write(bytes2, 0, bytes2.Length);
            cryptoStream.FlushFinalBlock();
            byte[] inArray = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();

            return Convert.ToBase64String(inArray);
        }
    }
}
