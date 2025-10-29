using Secure_Notes_Vault.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Secure_Notes_Vault.CryptoServices;
using System.Security.Cryptography;
using Secure_Notes_Vault.Utils;
using DateTimeOffsetConverter = Secure_Notes_Vault.Utils.DateTimeOffsetConverter;
using System.Runtime.CompilerServices;

namespace Secure_Notes_Vault.VaultServices
{
    public class VaultService : IDisposable
    {
        private readonly CryptoService _cryptoService;
        private readonly string _vaultPath = Path.Combine(AppContext.BaseDirectory, "SecureVault");
        private readonly string _saltPath;
        private readonly string _checkPath;
        private byte[]? _derivedKey; 
        private readonly JsonSerializerOptions _jsonOptions;

        public VaultService(CryptoService cryptoService)
        {
            _cryptoService = cryptoService;
         
            Directory.CreateDirectory(_vaultPath); 
            _saltPath = Path.Combine(_vaultPath, "vault.salt");
            _checkPath = Path.Combine(_vaultPath, "vault.check");

         
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new DateTimeOffsetConverter() }
            };
        }

        public void Initialize(string passphrase)
        {
            byte[] salt;
            if (File.Exists(_saltPath))
            {
       
                salt = Convert.FromBase64String(File.ReadAllText(_saltPath));
            }
            else
            {
              
                salt = RandomNumberGenerator.GetBytes(16); // 16-byte salt
                File.WriteAllText(_saltPath, Convert.ToBase64String(salt));
            }

            // Derive the key
            _derivedKey = _cryptoService.DeriveKey(passphrase, salt);

            // Verify the key
            if (File.Exists(_checkPath))
            {
              
                string checkData = File.ReadAllText(_checkPath);
                _cryptoService.Decrypt(checkData, _derivedKey); 
            }
            else
            {
                string checkData = _cryptoService.Encrypt("VAULT_OK", _derivedKey);
                File.WriteAllText(_checkPath, checkData);
            }
        }

        private void CheckInitialized()
        {
            if (_derivedKey == null)
                throw new InvalidOperationException("Vault is not initialized or is locked.");
        }

        private string GetNotePath(Guid id) => Path.Combine(_vaultPath, $"{id}.json");

        public List<Note> ListNotes()
        {
            CheckInitialized();
            var notes = new List<Note>();
         
            foreach (var filePath in Directory.EnumerateFiles(_vaultPath, "*.json"))
            {
              
                string json = File.ReadAllText(filePath);
                var note = JsonSerializer.Deserialize<Note>(json, _jsonOptions);
                if (note != null)
                {
                    notes.Add(note);
                }
            }
            return notes;
        }

        public Note CreateNote(string title, string plainTextBody)
        {
            CheckInitialized();
            string encryptedBody = _cryptoService.Encrypt(plainTextBody, _derivedKey!);

            var note = new Note
            {
                Id = Guid.NewGuid(),
                Title = title,
                Body = encryptedBody,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now
            };

            string json = JsonSerializer.Serialize(note, _jsonOptions);
            File.WriteAllText(GetNotePath(note.Id), json);
            return note;
        }

        public Note GetNoteForViewing(Guid id)
        {
            CheckInitialized();
            string json = File.ReadAllText(GetNotePath(id));
            var note = JsonSerializer.Deserialize<Note>(json, _jsonOptions);

            if (note == null)
                throw new FileNotFoundException("Note data is corrupt.", GetNotePath(id));

            // Decrypt the body for viewing
            note.Body = _cryptoService.Decrypt(note.Body, _derivedKey!);
            return note;
        }

        public void UpdateNote(Guid id, string newTitle, string newPlainTextBody)
        {
            CheckInitialized();
            string json = File.ReadAllText(GetNotePath(id));
            var note = JsonSerializer.Deserialize<Note>(json, _jsonOptions);

            if (note == null)
                throw new FileNotFoundException("Note data is corrupt.", GetNotePath(id));

            note.Title = newTitle;
            note.Body = _cryptoService.Encrypt(newPlainTextBody, _derivedKey!); 
            note.UpdatedAt = DateTimeOffset.Now;

            string updatedJson = JsonSerializer.Serialize(note, _jsonOptions);
            File.WriteAllText(GetNotePath(id), updatedJson);
        }

        public void DeleteNote(Guid id)
        {
            CheckInitialized();
            File.Delete(GetNotePath(id));
        }


        public void Dispose()
        {
            if (_derivedKey != null)
            {
                Array.Clear(_derivedKey, 0, _derivedKey.Length);
                _derivedKey = null;
            }
 
        }
    }
}
