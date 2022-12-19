using BankAccounts.Database.Tables;
using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BankAccounts.Classes
{
    public class Sloupec
    {
        public Rectangle BoxIncome { get; set; }
        public TextBlock Amount { get; set; }
        public TextBlock Date { get; set; }


        public enum TypeOfIncome { Income, DeIncome }
        public TypeOfIncome ofIncome { get; set; }


        public Transaction? MyTransaction { get; set; }


        public Sloupec(Rectangle _Box, TextBlock _Amount, TextBlock _Date, TypeOfIncome Typ) 
        { 
            BoxIncome= _Box;
            Amount= _Amount;
            Date= _Date;
            ofIncome= Typ;
        }
    }
}
