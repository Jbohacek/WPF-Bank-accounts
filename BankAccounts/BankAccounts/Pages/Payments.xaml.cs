using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BankAccounts.Database.Tables;

namespace BankAccounts.Pages
{
    /// <summary>
    /// Interakční logika pro Payments.xaml
    /// </summary>
    public partial class Payments : Page
    {
        public Payments()
        {
            InitializeComponent();
        }

        private void GetPayed_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHandler handler = new DatabaseHandler("BankAccounts");

            Transaction novy = new Transaction()
            {
                IdClient = 1,
                IdClientTO = null,
                Payment = Convert.ToDouble(PayAmount.Text),
                PaymentDay = DateTime.Now,

            };

            handler.InsertValue<Transaction>(novy);
        }
    }
}
