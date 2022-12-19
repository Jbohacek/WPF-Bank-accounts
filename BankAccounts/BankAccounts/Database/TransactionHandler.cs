using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccounts.Database.Tables;

namespace BankAccounts.Database
{
    internal class TransactionHandler
    {
        public static void AddTransaction(Person Chosen, Transaction ToAdd)
        {
            DatabaseHandler handler = new DatabaseHandler("BankAccounts");

            Chosen.Balance += ToAdd.Payment;

            handler.UpdateByObject<Person>(Chosen);

            handler.InsertValue<Transaction>(ToAdd);
        }
    }
}
