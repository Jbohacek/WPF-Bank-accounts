using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccounts.Views;

namespace BankAccounts.Database.Tables
{
    [Table("Transactions")]
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [Ignore]
        public string GetID { get { return "#" + id; } }
        
        public int IdClient { get; set; }

        [AllowNull]
        public int? IdClientTO { get; set; }

        
        public double Payment { get; set; }

        public string TypePayment { get; set; }

        public string CategoryPayment { get; set; }

        [Ignore]
        public string GetCatergorySourceImage { get
            {
                switch(CategoryPayment)
                {
                    case "Clothing":
                        return @"\Pictures\Category\Clothing.png";
                    case "Electronics":
                        return @"\Pictures\Category\Electronics.png";
                    case "Food":
                        return @"\Pictures\Category\Food.png";
                    case "Games":
                        return @"\Pictures\Category\Games.png";
                    case "Medical":
                        return @"\Pictures\Category\Medical.png";
                    case "Toys":
                        return @"\Pictures\Category\Toys.png";
                    default:
                        return @"\Pictures\Category\Other.png";
                }
            } }


        [Ignore]
        public string GetPayment {
            get {
                var pok = Payment.ToString().ToCharArray();
                Array.Reverse(pok);

                string konec = "";
                for (int i = 0; i < Payment.ToString().Length; i++)
                {
                    konec += pok[i];
                    if (i % 3 == 2 && i != Payment.ToString().Length)
                    {
                        konec += " ";
                    }
                }

                var kon = konec.ToCharArray();
                Array.Reverse(kon);


                string Finalni = new(kon);

                if (Finalni.Substring(0, 1) == " ")
                {
                    Finalni = Finalni.Substring(1);
                }

                if (Finalni.Substring(0,1) == "-" && Finalni.Substring(1,1) != " ")
                {
                    Finalni = Finalni.Insert(1, " ");
                }


                return Finalni + " CZK";

            }
        }

        public DateTime PaymentDay { get; set; }


        [Ignore]
        public string GetDate
        { get {

                return PaymentDay.Day + ". " + PaymentDay.ToString("MMMM").Substring(0, 3);
            
            } }
        public void OpenTransaction()
        {
            ShowTransaction Shower = new ShowTransaction(this);
            Shower.ShowDialog();
        }

        public override string ToString()
        {
            return $"{id} - user : {IdClient} - {Payment} - {PaymentDay.Day}";
        }
    }
}
