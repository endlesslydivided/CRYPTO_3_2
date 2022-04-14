using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;

namespace Lab7
{
    internal class Program
    {
        private static void Main()
        {
            while (true)
            { 
                Stopwatch time = new Stopwatch();

                Console.WriteLine("Введите текст:");
                var str = Console.ReadLine();
                if (int.TryParse(str, out int res))
                {
                    if (res == 0) break;
                }
                time.Start();
                var crypted = Des.Crypt(str);
                time.Stop();
                Console.WriteLine($"Сrypted: {crypted} | {(float)time.ElapsedMilliseconds / 1000} sec");

                time.Reset();
                time.Start();
                var decrypted = Des.Decrypt(crypted);
                time.Stop();
                Console.WriteLine($"Decrypted: {decrypted} | {(float)time.ElapsedMilliseconds / 1000} sec");

                Console.ReadKey();
            }
        }
    }

    public class Des
    {
        public static string Crypt(string text)
        {
            var key = new byte[] { 0xea, 0xee, 0xe2, 0xe0, 0xeb, 0xe5, 0xe2, 0xe0 };
            var iv = new byte[] { 0xa1, 0xa2, 0xa3, 0xa4, 0xa5, 0xa6, 0xa7, 0xa8 };

            var bytes = Encoding.Default.GetBytes(text);
            var data = Encrypt(bytes, key, iv);
            return Encoding.Default.GetString(data);
        }

        public static string Decrypt(string text)
        {
            var key = new byte[] { 0xea, 0xee, 0xe2, 0xe0, 0xeb, 0xe5, 0xe2, 0xe0 };
            var iv = new byte[] { 0xa1, 0xa2, 0xa3, 0xa4, 0xa5, 0xa6, 0xa7, 0xa8 };

            var bytes = Encoding.Default.GetBytes(text);
            var data = Decrypt(bytes, key, iv);
            return Encoding.Default.GetString(data);
        }

        public static byte[] Encrypt(byte[] inputBytes, byte[] key, byte[] iv)
        {
            using (var des = new DESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CFB;
                des.Padding = PaddingMode.Zeros;

                var encryptor = des.CreateEncryptor(key, iv);

                var stream = new MemoryStream();
                using (var cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                }

                return stream.ToArray().Take(inputBytes.Length).ToArray();
            }
        }

        public static byte[] Decrypt(byte[] inputBytes, byte[] key, byte[] iv)
        {
            using (var des = new DESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CFB;
                des.Padding = PaddingMode.Zeros;

                var decryptor = des.CreateDecryptor(key, iv);
                var input = new List<byte>(inputBytes);
                if (inputBytes.Length % 8 != 0)
                {
                    input.AddRange(new byte[8 - inputBytes.Length % 8]);
                }

                using (var result = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(new MemoryStream(input.ToArray()), decryptor,
                        CryptoStreamMode.Read))
                    {
                        cryptoStream.CopyTo(result);
                    }

                    return result.ToArray().Take(inputBytes.Length).ToArray();
                }
            }
        }
    }
}
