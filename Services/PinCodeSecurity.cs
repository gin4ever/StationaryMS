using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text;
using System.Security;
using System.Security.Cryptography;
namespace eProject.Services
{
    public class PinCodeSecurity
    {
        private static string key = "@5@#$$$%#^&@##%";
        public static string pinEncrypt(string pincode)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var td = new TripleDESCryptoServiceProvider())
                {
                    td.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    td.Mode = CipherMode.ECB;
                    td.Padding = PaddingMode.PKCS7;
                    using (var tranform = td.CreateEncryptor())
                    {
                        byte[] textByte = UTF8Encoding.UTF8.GetBytes(pincode);
                        byte[] bytes = tranform.TransformFinalBlock(textByte, 0, textByte.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public static string pinDecrypt(string pincode)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var td = new TripleDESCryptoServiceProvider())
                {
                    td.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    td.Mode = CipherMode.ECB;
                    td.Padding = PaddingMode.PKCS7;
                    using (var tranform = td.CreateDecryptor())
                    {
                        byte[] cipherText = Convert.FromBase64String(pincode);
                        byte[] bytes = tranform.TransformFinalBlock(cipherText, 0, cipherText.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }
    }
}
