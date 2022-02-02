using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AES
    {
        /// <summary>
        /// Function that encrypts the plaintext from inFile and stores cipher text to outFile
        /// </summary>
        /// <param name="inFile"> filepath where plaintext is stored </param>
        /// <param name="outFile"> filepath where cipher text is expected to be stored </param>
        /// <param name="secretKey"> symmetric encryption key </param>
        public static void EncryptFile(string inFile, string outFile, string secretKey)
        {
            byte[] header = null;   //image header (54 byte) should not be encrypted
            byte[] body = null;     //image body to be encrypted
            byte[] encrypted_body = null;

            //byte[] text = Encoding.ASCII.GetBytes(inFile);

            Manager.Formatter.Decompose(File.ReadAllBytes(inFile), out header, out body);

            AesCryptoServiceProvider aesCrypto = new AesCryptoServiceProvider();
            aesCrypto.Key = Encoding.ASCII.GetBytes(secretKey);
            aesCrypto.Mode = CipherMode.ECB;
            aesCrypto.Padding = PaddingMode.None;

            ICryptoTransform desEncrypt = aesCrypto.CreateEncryptor();
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, desEncrypt, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(body, 0, body.Length);
                }
                encrypted_body = msEncrypt.ToArray();
            }

            Manager.Formatter.Compose(header, encrypted_body, header.Length + encrypted_body.Length, outFile);
        }


        /// <summary>
        /// Function that decrypts the cipher text from inFile and stores as plaintext to outFile
        /// </summary>
        /// <param name="inFile"> filepath where cipher text is stored </param>
        /// <param name="outFile"> filepath where plain text is expected to be stored </param>
        /// <param name="secretKey"> symmetric encryption key </param>
        public static void DecryptFile(string inFile, string outFile, string secretKey)
        {
            byte[] header = null;       //image header (54 byte) should not be decrypted
            byte[] body = null;         //image body to be decrypted

            Manager.Formatter.Decompose(File.ReadAllBytes(inFile), out header, out body);
            byte[] decrypted_body = new byte[body.Length];

            AesCryptoServiceProvider aesCrypto = new AesCryptoServiceProvider();
            aesCrypto.Key = Encoding.ASCII.GetBytes(secretKey);
            aesCrypto.Mode = CipherMode.ECB;
            aesCrypto.Padding = PaddingMode.None;
            ICryptoTransform desDecrypt = aesCrypto.CreateDecryptor();

            using (MemoryStream msDecrypt = new MemoryStream(body))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, desDecrypt, CryptoStreamMode.Read))
                {
                    csDecrypt.Read(decrypted_body, 0, decrypted_body.Length);
                }
            }

            Manager.Formatter.Compose(header, decrypted_body, header.Length + decrypted_body.Length, outFile);
        }
    }
}
