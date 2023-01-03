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
using System.Windows.Shapes;
using BankAccounts.Database.Tables;

namespace BankAccounts.Views
{
    /// <summary>
    /// Interaction logic for ShowTransaction.xaml
    /// </summary>
    public partial class ShowTransaction : Window
    {
        public ShowTransaction(Transaction localTransaction)
        {
            InitializeComponent();

            IdBlock.Text += localTransaction.id;
            AmountBlock.Text += localTransaction.Payment;
            DateBlock.Text += localTransaction.GetDate + localTransaction.PaymentDay.Year;
            ToPersonBlock.Text += localTransaction.IdClientTO;
            ImagePathBlock.Text += localTransaction.GetCatergorySourceImage;

            this.Topmost = true;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
