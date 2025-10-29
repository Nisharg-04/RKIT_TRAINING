using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secure_Notes_Vault.Model
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;

        // This 'Body' will contain the ENCRYPTED Base64 string when serialized to disk
        // and the DECRYPTED plaintext when loaded into memory for viewing.
        public string Body { get; set; } = string.Empty;

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
