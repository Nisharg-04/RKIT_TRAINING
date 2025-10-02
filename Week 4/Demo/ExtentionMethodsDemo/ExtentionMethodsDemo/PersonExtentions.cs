using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtentionMethodsDemo
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    public static class PersonExtensions
    {
        /// <summary>
        /// Combines the first and last name of a person.
        /// </summary>
        public static string GetFullName(this Person person)
        {
            return $"{person.FirstName} {person.LastName}";
        }
    }
}
