using System.Security.Cryptography;
using System.Text;
namespace CryptographyDemo
{
    internal class Program
    {
        public static string SHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                return Convert.ToHexString(bytes);
            }
        }
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            RandomNumberGenerator.Fill(salt);

            int iterations = 100000;
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            byte[] key = pbkdf2.GetBytes(32); // 256-bit key

            byte[] hashedBytes = new byte[16 + 32];
            Array.Copy(salt, 0, hashedBytes, 0, 16);
            Array.Copy(key, 0, hashedBytes, 16, 32);

            // Return as base64 string
            return Convert.ToBase64String(hashedBytes);
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
        
            byte[] hashedBytes = Convert.FromBase64String(storedHash);

          
            byte[] salt = new byte[16];
            Array.Copy(hashedBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            // Compare the results
            // Use a time-constant comparison to prevent timing attacks
            return CryptographicOperations.FixedTimeEquals(hash, hashedBytes.AsSpan(16));
        }

        public static void AESDEMO()
        {
            byte[] key = new byte[32]; // 256-bit key
            RandomNumberGenerator.Fill(key);

            byte[] nonce = new byte[12]; // 96-bit nonce
            RandomNumberGenerator.Fill(nonce);

            string text = "Confidential data!";
            byte[] plaintext = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] ciphertext = new byte[plaintext.Length];
            byte[] tag = new byte[16]; // authentication tag
            using (AesGcm aes = new AesGcm(key))
            {
                aes.Encrypt(nonce, plaintext, ciphertext, tag);
            }

            Console.WriteLine("Encrypted: " + Convert.ToBase64String(ciphertext));

            // Decrypt
            byte[] decrypted = new byte[ciphertext.Length];
            using (AesGcm aes = new AesGcm(key))
            {
                aes.Decrypt(nonce, ciphertext, tag, decrypted);
            }

            Console.WriteLine("Decrypted: " + System.Text.Encoding.UTF8.GetString(decrypted));
        }
        public static void DSDEMO()
        {
            string original = "RKIT Software";

            using (RSA rsa = RSA.Create(2048))
            {
                byte[] data = Encoding.UTF8.GetBytes(original);
                byte[] sign = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                Console.WriteLine($"Sign: {Convert.ToBase64String(sign)}");

                bool isVerified = rsa.VerifyData(data, sign, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                Console.WriteLine($"Signature verification status : {isVerified}");
            }

        }
        public static void RSADEMO()
        {
            using var rsa = RSA.Create();

            // Export keys
            RSAParameters pub = rsa.ExportParameters(false);
            RSAParameters priv = rsa.ExportParameters(true);

            byte[] data = Encoding.UTF8.GetBytes("Secret message");

            byte[] encrypted = rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
            Console.WriteLine("Encrypted: " + Convert.ToBase64String(encrypted));

            byte[] decrypted = rsa.Decrypt(encrypted, RSAEncryptionPadding.OaepSHA256);
            Console.WriteLine("Decrypted: " + Encoding.UTF8.GetString(decrypted));
        }
        static void Main(string[] args)
        {
        //    string input = "Nisharg";
        //    string hash = SHA256Hash(input);
        //    Console.WriteLine($"Input: {input}");
        //    Console.WriteLine($"SHA-256 Hash: {hash}");



        //    string password = "MY_SECURE_PASSWORD";

        //    string hashedPassword =HashPassword(password);
        //    Console.WriteLine($"Hashed Password: {hashedPassword}");

        //    bool isVerified = VerifyPassword("MY_SECURE_PASSWORD", hashedPassword);
        //    Console.WriteLine(isVerified ? "Verified" : "Not verfied");
        
            //AESDEMO();
        
            //DSDEMO();

            //RSADEMO();
        }
    }
}
