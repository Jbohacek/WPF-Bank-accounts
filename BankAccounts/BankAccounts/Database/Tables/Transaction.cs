using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Database.Tables
{
    [Table("Transactions")]
    internal class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        
        public int IdClient { get; set; }

        [AllowNull]
        public int? IdClientTO { get; set; }

        
        public double Payment { get; set; }

        public DateTime PaymentDay { get; set; }


        public override string ToString()
        {
            return $"{id} - user : {IdClient} - {Payment} - {PaymentDay.Day}";
        }
    }
}
