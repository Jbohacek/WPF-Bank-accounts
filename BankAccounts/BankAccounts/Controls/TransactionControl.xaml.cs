using BankAccounts.Database.Tables;
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

namespace BankAccounts.Controls
{
    /// <summary>
    /// Interakční logika pro TransactionControl.xaml
    /// </summary>
    public partial class TransactionControl : UserControl
    {


        public Transaction Transakce
        {
            get { return (Transaction)GetValue(MyTransakceProperty); }
            set { SetValue(MyTransakceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyTransakceProperty =
            DependencyProperty.Register("Transakce", typeof(Transaction), typeof(TransactionControl), new PropertyMetadata(new Transaction(),SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TransactionControl? transaction = d as TransactionControl;
            if (transaction != null)
            {
                Transaction transakce = (Transaction)e.NewValue;
                switch (transakce.Payment)
                {
                    case > 0:
                        transaction.BoxBalance.Text = "+ " + transakce.GetPayment;
                        transaction.BoxBalance.Foreground = new SolidColorBrush(Colors.Green);
                        break;
                    case < 0:
                        transaction.BoxBalance.Text = "" + transakce.GetPayment;
                        transaction.BoxBalance.Foreground = new SolidColorBrush(Colors.Red);
                        break;
                    default:
                        transaction.BoxBalance.Text = "" + transakce.GetPayment;
                        transaction.BoxBalance.Foreground = new SolidColorBrush(Colors.White);
                        break;
                }

                transaction.BoxDate.Text = transakce.GetDate;
                transaction.BoxID.Text = "#" + transakce.id.ToString();
                transaction.BoxTypeOfPayment.Text = transakce.TypePayment;

                transaction.BoxPicture.Source = new BitmapImage(new Uri(transakce.GetCatergorySourceImage, UriKind.Relative));
            }

        }

        public TransactionControl()
        {
            InitializeComponent();
        }
    }
}
