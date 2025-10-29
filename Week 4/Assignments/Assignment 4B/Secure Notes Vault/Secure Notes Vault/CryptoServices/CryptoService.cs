using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Secure_Notes_Vault.CryptoServices
{
    public class CryptoService
    {
        //  PBKDF2 Settings
        private const int Iterations = 500000;
        private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

        private const int KeySizeBits = 256;
        private const int KeySizeBytes = KeySizeBits / 8; // 32 bytes
        private const int NonceSize = 12; // 12 bytes 
        private const int TagSize = 16; // 16 bytes 

        public byte[] DeriveKey(string passphrase, byte[] salt)
        {
           
            return Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(passphrase),
                salt,
                Iterations,
                _hashAlgorithm,
                KeySizeBytes 
            );
        }

        public string Encrypt(string plainText, byte[] key)
        {
            
            using AesGcm aesGcm = new AesGcm(key);

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            
            byte[] nonce = new byte[NonceSize];
            byte[] tag = new byte[TagSize];
            byte[] cipherTextBytes = new byte[plainTextBytes.Length];

            
            RandomNumberGenerator.Fill(nonce);

            
            aesGcm.Encrypt(nonce, plainTextBytes, cipherTextBytes, tag);

           
            byte[] payload = new byte[NonceSize + TagSize + cipherTextBytes.Length];
            Buffer.BlockCopy(nonce, 0, payload, 0, NonceSize);
            Buffer.BlockCopy(tag, 0, payload, NonceSize, TagSize);
            Buffer.BlockCopy(cipherTextBytes, 0, payload, NonceSize + TagSize, cipherTextBytes.Length);

           
            return Convert.ToBase64String(payload);
        }

        public string Decrypt(string cipherText, byte[] key)
        {
            
            byte[] payload = Convert.FromBase64String(cipherText);

            
            if (payload.Length < NonceSize + TagSize)
            {
                throw new CryptographicException("Invalid ciphertext payload.");
            }

            using AesGcm aesGcm = new AesGcm(key);

           
            byte[] nonce = new byte[NonceSize];
            Buffer.BlockCopy(payload, 0, nonce, 0, NonceSize);

            byte[] tag = new byte[TagSize];
            Buffer.BlockCopy(payload, NonceSize, tag, 0, TagSize);

            int cipherTextLength = payload.Length - NonceSize - TagSize;
            byte[] cipherTextBytes = new byte[cipherTextLength];
            Buffer.BlockCopy(payload, NonceSize + TagSize, cipherTextBytes, 0, cipherTextLength);

          
            byte[] plainTextBytes = new byte[cipherTextLength];

            aesGcm.Decrypt(nonce, cipherTextBytes, tag, plainTextBytes);

      
            return Encoding.UTF8.GetString(plainTextBytes);
        }
    }
    }

