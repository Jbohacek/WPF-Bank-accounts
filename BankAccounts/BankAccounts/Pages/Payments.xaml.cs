﻿using System;
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
using BankAccounts.Database;
using BankAccounts.Database.Tables;

namespace BankAccounts.Pages
{
    /// <summary>
    /// Interakční logika pro Payments.xaml
    /// </summary>
    public partial class Payments : Page
    {
        public MainWindow ParentOf { get; set; }
        public Payments(MainWindow _ParentOf)
        {
            InitializeComponent();
            ParentOf = _ParentOf;
        }

        private void GetPayed_Click(object sender, RoutedEventArgs e)
        {

            Transaction novy = new Transaction()
            {
                IdClient = MainWindow.Chosen.Id,
                IdClientTO = null,
                Payment = Convert.ToDouble(PayAmount.Text),
                PaymentDay = DateTime.Now,
                CategoryPayment = "Food",
                TypePayment = "Card payment"
            };

            TransactionHandler.AddTransaction(MainWindow.Chosen, novy);

            ParentOf.Reload();
        }
    }
}
