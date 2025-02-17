﻿using BankAccounts.Database.Tables;
using SQLite;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankAccounts.Pages
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Page
    {
        public Reports()
        {
            InitializeComponent();
            Reload();
        }
        private void Reload()
        {



            DatabaseHandler data = new DatabaseHandler("BankAccounts");

            using (SQLiteConnection connection = new SQLiteConnection(data.Cesta))
            {
                //var x = connection.Query<Person>("SELECT *\r\nFROM Clients c \r\ninner join Transactions t on IdClient = c.id\r\nwhere c.Id = '1'",null);

                SQLiteCommand komand = new SQLiteCommand(connection);
                komand.CommandText = $"SELECT *\r\nFROM Clients c \r\ninner join Transactions t on IdClient = c.id\r\nwhere c.Id = '{MainWindow.Chosen.Id}'";

                var x = komand.ExecuteQuery<Transaction>();

                
                DataShower.ItemsSource = x;
            }
        }
    }
}
