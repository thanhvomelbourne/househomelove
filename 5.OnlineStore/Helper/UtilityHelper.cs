using CoreLibrary.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace OnlineStore.Helper
{
    public static class UtilityHelper
    {
        public static string SHOPPING_CART_ID = "SHOPPING_CART_ID";
        public static string ORDER_ID = "ORDER_ID";
        public static string ITEMS_PER_PAGE = "ITEMS_PER_PAGE";
        public static string SORT_BY = "SORT_BY";
        public static string USER_ID = "USER_ID";
        public static string CURRENT_PRODUCT_PAGE_FOR_ADMIN = "CURRENT_PRODUCT_PAGE_FOR_ADMIN";
        public static string CURRENT_PRODUCT_SEARCH_FOR_ADMIN = "CURRENT_PRODUCT_SEARCH_FOR_ADMIN";
        public static string CURRENT_PRODUCT_SEARCH_ISLIVE_FOR_ADMIN = "CURRENT_PRODUCT_SEARCH_ISLIVE_FOR_ADMIN";
        public static string CURRENT_PRODUCT_SEARCH_INSTOCK_FOR_ADMIN = "CURRENT_PRODUCT_SEARCH_INSTOCK_FOR_ADMIN";
        public static string CURRENT_ORDER_PAGE_FOR_ADMIN = "CURRENT_ORDER_PAGE_FOR_ADMIN";
        public static string CURRENT_ORDER_SEARCH_FOR_ADMIN = "CURRENT_ORDER_SEARCH_FOR_ADMIN";
        public static string CURRENT_ORDER_SEARCH_CONFIRMED_STATUS_FOR_ADMIN = "CURRENT_ORDER_SEARCH_CONFIRMED_STATUS_FOR_ADMIN";
        public static string CURRENT_ORDER_SEARCH_SHIPPING_METHOD_FOR_ADMIN = "CURRENT_ORDER_SEARCH_SHIPPING_METHOD_FOR_ADMIN";
        public static string CURRENT_CATEGORY_SEARCH_ISLIVE_FOR_ADMIN = "CURRENT_CATEGORY_SEARCH_ISLIVE_FOR_ADMIN";

        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(Constant.CommonValue.EncryptKey, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(Constant.CommonValue.EncryptKey, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }

            string result = sb.ToString();

            return new string(result.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        public static void SetCookie(string key, string value, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(key, value) {Expires = expires};
            HttpContext.Current.Response.SetCookie(cookie);
        }

        public static string GetCookie(string key, bool isNumber)
        {
            HttpCookie cook = HttpContext.Current.Request.Cookies[key];
            if (cook != null)
            {
                return cook.Value;
            }
            else
            {
                return isNumber ? "0" : string.Empty;
            }
        }

        /// <summary>
        /// Determines if GZip is supported
        /// </summary>
        /// <returns></returns>
        public static bool IsGZipSupported()
        {
            string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
            if (!string.IsNullOrEmpty(AcceptEncoding) &&
                    (AcceptEncoding.Contains("gzip") || AcceptEncoding.Contains("deflate")))
                return true;
            return false;
        }

        /// <summary>
        /// Sets up the current page or handler to use GZip through a Response.Filter
        /// IMPORTANT:  
        /// You have to call this method before any output is generated!
        /// </summary>
        public static void GZipEncodePage()
        {
            HttpResponse response = HttpContext.Current.Response;

            if (IsGZipSupported())
            {
                string acceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];

                if (acceptEncoding.Contains("gzip"))
                {
                    response.Filter = new System.IO.Compression.GZipStream(response.Filter,
                                                System.IO.Compression.CompressionMode.Compress);
                    response.Headers.Remove("Content-Encoding");
                    response.AppendHeader("Content-Encoding", "gzip");
                }
                else
                {
                    response.Filter = new System.IO.Compression.DeflateStream(response.Filter,
                                                System.IO.Compression.CompressionMode.Compress);
                    response.Headers.Remove("Content-Encoding");
                    response.AppendHeader("Content-Encoding", "deflate");
                }
            }

            // Allow proxy servers to cache encoded and unencoded versions separately
            response.AppendHeader("Vary", "Content-Encoding");
        }


    }
}