using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Database.Tables
{
    [Table("Clients")]
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string IdAccount { get; set; }
        [NotNull]
        public string FirstName { get; set; }
        [NotNull]
        public string LastName { get; set; }

        [Ignore]
        public string FullName { get { return FirstName + " " + LastName; } }
        [Ignore]
        public string UserName { get { return LastName.ToLower() + LastName.ToLower(); } }

        [NotNull]
        public double Balance { get; set; }

        [NotNull]
        public string? Email { get; set; }

        [NotNull]
        public DateTime? BirthDate { get; set; }

        [PasswordPropertyText]
        public string? Password { get; set; }
    }
}
