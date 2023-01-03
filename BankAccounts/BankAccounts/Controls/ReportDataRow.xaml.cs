using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
using Transaction = BankAccounts.Database.Tables.Transaction;

namespace BankAccounts.Controls
{
    /// <summary>
    /// Interaction logic for ReportDataRow.xaml
    /// </summary>
    public partial class ReportDataRow : UserControl
    {
        public Transaction LokalTransaction { get; set; }

        public Database.Tables.Transaction Transakce
        {
            get { return (Database.Tables.Transaction)GetValue(MyTransakceProperty); }
            set { SetValue(MyTransakceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyTransakceProperty =
            DependencyProperty.Register("Transakce", typeof(Database.Tables.Transaction), typeof(ReportDataRow), new PropertyMetadata(new Database.Tables.Transaction(), SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ReportDataRow? row = d as ReportDataRow;
            if (row != null)
            {
                Database.Tables.Transaction transakce = (Database.Tables.Transaction)e.NewValue;

                switch (transakce.Payment)
                {
                    case > 0:
                        row.AmountTrans.Text = "+" + transakce.GetPayment;
                        row.AmountTrans.Foreground = new SolidColorBrush(Colors.Green);
                        break;
                    case < 0:
                        row.AmountTrans.Text = "" + transakce.GetPayment;
                        row.AmountTrans.Foreground = new SolidColorBrush(Colors.Red);
                        break;
                    default:
                        row.AmountTrans.Text = "" + transakce.GetPayment;
                        row.AmountTrans.Foreground = new SolidColorBrush(Colors.White);
                        break;
                }

                row.DateTrans.Text = transakce.GetDate;
                row.IDTrans.Text = transakce.GetID;

                row.ImageTransaction.Source = new BitmapImage(new Uri(transakce.GetCatergorySourceImage, UriKind.Relative));

                row.LokalTransaction = transakce;

            }
        }

        public ReportDataRow()
        {
            InitializeComponent();
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LokalTransaction.OpenTransaction();
        }
    }
}
