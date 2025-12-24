using System.Security.Cryptography;
using System.Text;
using Secure_Notes_Vault.VaultServices;
using Secure_Notes_Vault.CryptoServices;

namespace Secure_Notes_Vault
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to SecureNotes CLI");

            string passphrase = GetPassphrase();

            using (VaultService vault = new VaultService(new CryptoService()))
            {
                try
                {
                    // Try to initialize the vault. This will create a new one
                    // or unlock an existing one.
                    vault.Initialize(passphrase);
                }
                catch (CryptographicException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Access DENIED. Invalid passphrase or corrupted vault.");
                    Console.ResetColor();
                    return;
                }
                finally
                {
                    passphrase = string.Empty;
                    GC.Collect();
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Vault unlocked successfully.");
                Console.ResetColor();

                RunMenu(vault);
            }

            Console.WriteLine("Vault locked. Goodbye.");
        }

        private static void RunMenu(VaultService vault)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n SecureNotes Menu ");
                Console.WriteLine("[1] List all notes");
                Console.WriteLine("[2] Create new note");
                Console.WriteLine("[3] View note");
                Console.WriteLine("[4] Update note");
                Console.WriteLine("[5] Delete note");
                Console.WriteLine("[6] Exit");
                Console.Write("Select an option: ");

                switch (Console.ReadLine())
                {
                    case "1": ListNotes(vault); break;
                    case "2": CreateNote(vault); break;
                    case "3": ViewNote(vault); break;
                    case "4": UpdateNote(vault); break;
                    case "5": DeleteNote(vault); break;
                    case "6": running = false; break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static void ListNotes(VaultService vault)
        {
            Console.WriteLine("\nYour Notes");
            var notes = vault.ListNotes();
            if (!notes.Any())
            {
                Console.WriteLine("No notes found.");
                return;
            }

            foreach (var note in notes)
            {
                Console.WriteLine($"ID: {note.Id} | Title: {note.Title} | Updated: {note.UpdatedAt:g}");
            }
        }

        private static void CreateNote(VaultService vault)
        {
            Console.WriteLine("\nCreate New Note");
            Console.Write("Title: ");
            string title = Console.ReadLine() ?? "Untitled";
            Console.WriteLine("Body (Press  Ctrl+Z on a new line to finish):");
            string body = Console.In.ReadToEnd(); // for multi line it closes when eof occurs in windows ctrl + Z in linux ctrl + D

            var note = vault.CreateNote(title, body);
            Console.WriteLine($"Note '{note.Title}' created successfully with ID: {note.Id}");
        }

        private static Guid? GetNoteIdFromUser()
        {
            Console.Write("Enter Note ID: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                return id;
            }
            Console.WriteLine("Invalid ID format.");
            return null;
        }

        private static void ViewNote(VaultService vault)
        {
            Console.WriteLine("\nView Note");
            Guid? id = GetNoteIdFromUser();
            if (!id.HasValue) return;

            try
            {
                var note = vault.GetNoteForViewing(id.Value);
                Console.WriteLine("---------------------------------");
                Console.WriteLine($"ID: {note.Id}");
                Console.WriteLine($"Title: {note.Title}");
                Console.WriteLine($"Created: {note.CreatedAt}");
                Console.WriteLine($"Updated: {note.UpdatedAt}");
                Console.WriteLine("Body");
                Console.WriteLine(note.Body);
                Console.WriteLine("---------------------------------");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: Note not found.");
            }
        }

        private static void UpdateNote(VaultService vault)
        {
            Console.WriteLine("\nUpdate Note");
            Guid? id = GetNoteIdFromUser();
            if (!id.HasValue) return;

            try
            {
                // First, get the existing note
                var oldNote = vault.GetNoteForViewing(id.Value);

                Console.WriteLine($"Current Title: {oldNote.Title}");
                Console.Write("New Title (press Enter to keep old): ");
                string newTitle = Console.ReadLine();
                if (string.IsNullOrEmpty(newTitle))
                {
                    newTitle = oldNote.Title;
                }

                Console.WriteLine("Enter new body (Press Ctrl+D or Ctrl+Z on a new line to finish):");
                Console.WriteLine($" Current Body \n{oldNote.Body}\n End of Body ");
                string newBody = Console.In.ReadToEnd();
                if (string.IsNullOrEmpty(newBody))
                {
                    newBody = oldNote.Body;
                }

                vault.UpdateNote(id.Value, newTitle, newBody);
                Console.WriteLine("Note updated successfully.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: Note not found.");
            }
        }

        private static void DeleteNote(VaultService vault)
        {
            Console.WriteLine("\n Delete Note ");
            Guid? id = GetNoteIdFromUser();
            if (!id.HasValue) return;

            Console.Write("Are you sure you want to delete this note? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                try
                {
                    vault.DeleteNote(id.Value);
                    Console.WriteLine("Note deleted.");
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Error: Note not found.");
                }
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
            }
        }

         private static string GetPassphrase()
        {
            Console.Write("Enter your vault passphrase: ");
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && sb.Length > 0)
                {
                    sb.Length--;
                    Console.Write("\b \b"); // Erase character from console
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    sb.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
            Console.WriteLine();
            return sb.ToString();
        }
    }
}
