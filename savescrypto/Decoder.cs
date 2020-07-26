using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace savescrypto
{
    class Decoder
    {
        private string raw_data = "";

        private const string passwd = "WeDidntSecureThisVeryWell!!1";

        private string json = "";

        public bool SaveFileToJson(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }

            string save_dir = Path.GetDirectoryName(path);

            raw_data = File.ReadAllText(path);

            json = decrypt(raw_data, passwd);

            File.WriteAllText(Path.Combine(save_dir, "savegame.json"), json);

            return true;
        }

        private string decrypt(string text, string passwd = null)
        {
            byte[] bytes = Encoding.UTF8.GetBytes("pemgail9uzpgzl88");
            byte[] array = Convert.FromBase64String(text);
            byte[] bytes2 = new PasswordDeriveBytes(passwd, null).GetBytes(32);

            ICryptoTransform transform = new RijndaelManaged
            {
                Mode = CipherMode.CBC
            }.CreateDecryptor(bytes2, bytes);
            MemoryStream memoryStream = new MemoryStream(array);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
            byte[] array2 = new byte[array.Length];
            int count = cryptoStream.Read(array2, 0, array2.Length);
            memoryStream.Close();
            cryptoStream.Close();

            return Encoding.UTF8.GetString(array2, 0, count);
        }               
    }
}
