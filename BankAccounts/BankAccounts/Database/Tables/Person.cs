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
        public string UserName { get { return (LastName.ToLower() + FirstName.ToLower()); } }

        [Ignore]
        public string GetBalance { get 
            {

                var pok = Balance.ToString().ToCharArray();
                Array.Reverse(pok);

                string konec = "";
                for (int i = 0; i < Balance.ToString().Length; i++)
                {
                    konec += pok[i];
                    if (i % 3 == 2 && i != Balance.ToString().Length)
                    {
                        konec += " ";
                    }
                }



                var kon = konec.ToCharArray();
                Array.Reverse(kon);

                string Finalni = new string(kon);

                if (Finalni.Substring(0, 1) == " ")
                {
                    Finalni = Finalni.Substring(1);
                }

                if (Finalni.Substring(0, 1) == "-" && Finalni.Substring(1, 1) != " ")
                {
                    Finalni = Finalni.Insert(1, " ");
                }

                return Finalni + " CZK"; 
            } 
        }

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
