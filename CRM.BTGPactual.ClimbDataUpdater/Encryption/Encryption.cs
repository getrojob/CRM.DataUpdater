using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.IO;

namespace Pactual.CRM.Encryption
{
   public class Encryption
    {
        private byte[] key;
        private byte[] iv;
        private char separator = "-".ToCharArray()[0];
        /// <summary>
        /// Enviar uma string de 32 caracteres para Key32Bytes e uma string de 16 caracteres para IV16Bytes.
        /// </summary>
        /// <param name="Key32bits"></param>
        /// <param name="IV16bits"></param>
        public Encryption(string Key32Bytes, string IV16Bytes, char o_separator)
        {
            this.key = Encoding.Default.GetBytes(Key32Bytes);
            this.iv = Encoding.Default.GetBytes(IV16Bytes);

            this.separator = o_separator;
        }

        public string EncryptString(string str)
        {

            byte[] resutArray = this.encryptStringToBytes_AES(str, key, iv);
            string r = "";
            foreach (byte b in resutArray)
            {
                r += b.ToString() + separator.ToString();
            }
            return r.Remove(r.Length - 1);
        }

        public string DecryptString(string str)
        {                        
            string[] arraystr = str.Split(this.separator.ToString().ToCharArray());

            List<byte> arrayByte = new List<byte>();

            foreach (string s in arraystr)
            {
                if (s != string.Empty)
                {
                    byte b = Convert.ToByte(s);
                    arrayByte.Add(b);
                }
            }


            byte[] senha = arrayByte.ToArray();

            return this.decryptStringFromBytes_AES(senha, key, iv);

        }

        private byte[] encryptStringToBytes_AES(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the stream used to encrypt to an in memory
            // array of bytes.
            MemoryStream msEncrypt = null;

            // Declare the RijndaelManaged object
            // used to encrypt the data.
            RijndaelManaged aesAlg = null;

            try
            {
                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                msEncrypt = new MemoryStream();
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {

                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                }

            }
            finally
            {

                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            return msEncrypt.ToArray();

        }

        private string decryptStringFromBytes_AES(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            try
            {
                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }

            }
            finally
            {

                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;

        }




    }
}
