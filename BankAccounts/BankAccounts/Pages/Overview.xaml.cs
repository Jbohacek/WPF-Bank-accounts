using BankAccounts.Database.Tables;
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
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BankAccounts.Classes;
using System.Windows.Markup;

namespace BankAccounts.Pages
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Overview : Page
    {
        public Overview()
        {
            InitializeComponent();
            this.DataContext = MainWindow.Chosen;

            ReloadData();
           
            BalanceView.Text = MainWindow.Chosen.GetBalance;


            if (BalanceView.Text.Length > 12)
            {
                BalanceView.FontSize = 35;
            }
            if (BalanceView.Text.Length > 20)
            {
                BalanceView.FontSize = 24;
            }
            if (BalanceView.Text.Length > 30)
            {
                BalanceView.FontSize = 12;
            }

            LoadGraph();

        }
        private void ReloadData()
        {
            DatabaseHandler data = new DatabaseHandler("BankAccounts");

            using (SQLiteConnection connection = new SQLiteConnection(data.Cesta))
            {
                //var x = connection.Query<Person>("SELECT *\r\nFROM Clients c \r\ninner join Transactions t on IdClient = c.id\r\nwhere c.Id = '1'",null);

                SQLiteCommand komand = new SQLiteCommand(connection);
                komand.CommandText = $"SELECT *\r\nFROM Clients c \r\ninner join Transactions t on IdClient = c.id\r\nwhere c.Id = '{MainWindow.Chosen.Id}'";

                var x = komand.ExecuteQuery<Transaction>();

                switch (SortByType)
                {
                    case 0:
                        x = x.OrderBy(x => x.id).ToList();
                        break;
                    case 1:
                        x = x.OrderBy(x => x.Payment).ToList();
                        break;
                    case 2:
                        x = x.OrderBy(x => x.PaymentDay).ToList();
                        break;
                    default:

                        break;
                }
                bool? sortIt = SortBy.IsChecked;
                if (sortIt != null)
                {
                    if (sortIt == false)
                    {
                        x.Reverse();
                    }
                }

                DoubleAnimation OpacityItems = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.2)))
                {From = 0 };
                LastPurchesList.BeginAnimation(OpacityProperty, OpacityItems);

                LastPurchesList.ItemsSource = x;

               
                
            }
        }

        int SortByType = 0;
        /*
         * 0 Id
         * 1 Amount
         * 2 Date
         */

        private void ResetColorsSort()
        {
            SortDate.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00ADB5"));
            SortID.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00ADB5"));
            SortAmount.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00ADB5"));
        }
        private void SortDate_Click(object sender, RoutedEventArgs e)
        {
            ResetColorsSort();
            SortDate.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#006368"));
            SortByType = 2;
            ReloadData();
        }

        private void SortAmount_Click(object sender, RoutedEventArgs e)
        {
            ResetColorsSort();
            SortAmount.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#006368"));
            SortByType = 1;
            ReloadData();
        }

        private void SortID_Click(object sender, RoutedEventArgs e)
        {
            ResetColorsSort();
            SortID.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#006368"));
            SortByType = 0;
            ReloadData();
        }

        private void SortBy_Checked(object sender, RoutedEventArgs e)
        {
            ReloadData();
        }

        public List<Sloupec> IncomeSloupce = new List<Sloupec>();
        public List<Sloupec> DeIncomeSloupce = new List<Sloupec>();
        public MoneyTable Table { get; set; }
        private void LoadGraph()
        {
            IncomeSloupce = new List<Sloupec>()
            {
                new Sloupec(BoxIncome1,TextIncome1,Datum1, Sloupec.TypeOfIncome.Income),
                new Sloupec(BoxIncome2,TextIncome2,Datum2, Sloupec.TypeOfIncome.Income),
                new Sloupec(BoxIncome3,TextIncome3,Datum3, Sloupec.TypeOfIncome.Income),
                new Sloupec(BoxIncome4,TextIncome4,Datum4, Sloupec.TypeOfIncome.Income),
                new Sloupec(BoxIncome5,TextIncome5,Datum5, Sloupec.TypeOfIncome.Income),
                new Sloupec(BoxIncome6,TextIncome6,Datum6, Sloupec.TypeOfIncome.Income),
                new Sloupec(BoxIncome7,TextIncome7,Datum7, Sloupec.TypeOfIncome.Income),
            };
            DeIncomeSloupce = new List<Sloupec>()
            {
                new Sloupec(TextDeIncome1,BoxDeIncome1,Datum1, Sloupec.TypeOfIncome.DeIncome),
                new Sloupec(TextDeIncome2,BoxDeIncome2,Datum2, Sloupec.TypeOfIncome.DeIncome),
                new Sloupec(TextDeIncome3,BoxDeIncome3,Datum3, Sloupec.TypeOfIncome.DeIncome),
                new Sloupec(TextDeIncome4,BoxDeIncome4,Datum4, Sloupec.TypeOfIncome.DeIncome),
                new Sloupec(TextDeIncome5,BoxDeIncome5,Datum5, Sloupec.TypeOfIncome.DeIncome),
                new Sloupec(TextDeIncome6,BoxDeIncome6,Datum6, Sloupec.TypeOfIncome.DeIncome),
                new Sloupec(TextDeIncome7,BoxDeIncome7,Datum7, Sloupec.TypeOfIncome.DeIncome),
            };

            Table = new MoneyTable(IncomeSloupce, DeIncomeSloupce);

            DatabaseHandler data = new DatabaseHandler("BankAccounts");

            using (SQLiteConnection connection = new SQLiteConnection(data.Cesta))
            {
                //var x = connection.Query<Person>("SELECT *\r\nFROM Clients c \r\ninner join Transactions t on IdClient = c.id\r\nwhere c.Id = '1'",null);

                SQLiteCommand komand = new SQLiteCommand(connection);
                komand.CommandText = $"SELECT *\r\nFROM Clients c \r\ninner join Transactions t on IdClient = c.id\r\nwhere c.Id = '{MainWindow.Chosen.Id}'";

                var x = komand.ExecuteQuery<Transaction>();

                Table.OrderByID(x.OrderBy(x => x.id).Reverse().ToList());
                //Table.OrderByDate(x.OrderBy(x => x.id).Reverse().ToList());
            }


            
        }

        string TableSorting = "ID";

        private void SortTableByDate_Click(object sender, RoutedEventArgs e)
        {
            if(TableSorting== "ID") 
            {
                SortTableByDate.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#006368"));
                SortTableByDate.Cursor = Cursors.Arrow;
                SortTableByID.Cursor = Cursors.Hand;
                SortTableByID.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00ADB5"));
                TableSorting = "DATE";

                DatabaseHandler data = new DatabaseHandler("BankAccounts");

                using (SQLiteConnection connection = new SQLiteConnection(data.Cesta))
                {
                    //var x = connection.Query<Person>("SELECT *\r\nFROM Clients c \r\ninner join Transactions t on IdClient = c.id\r\nwhere c.Id = '1'",null);

                    SQLiteCommand komand = new SQLiteCommand(connection);
                    komand.CommandText = $"SELECT *\r\nFROM Clients c \r\ninner join Transactions t on IdClient = c.id\r\nwhere c.Id = '{MainWindow.Chosen.Id}'";

                    var x = komand.ExecuteQuery<Transaction>();

                    //Table.OrderByID(x.OrderBy(x => x.id).Reverse().ToList());
                    Table.OrderByDate(x.OrderBy(x => x.id).Reverse().ToList());
                }
            }
        }

        private void SortTableByID_Click(object sender, RoutedEventArgs e)
        {
            if (TableSorting == "DATE")
            {
                SortTableByDate.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00ADB5"));
                SortTableByDate.Cursor = Cursors.Hand;
                SortTableByID.Cursor = Cursors.Arrow;
                SortTableByID.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#006368"));
                TableSorting = "ID";

                DatabaseHandler data = new DatabaseHandler("BankAccounts");

                using (SQLiteConnection connection = new SQLiteConnection(data.Cesta))
                {
                    //var x = connection.Query<Person>("SELECT *\r\nFROM Clients c \r\ninner join Transactions t on IdClient = c.id\r\nwhere c.Id = '1'",null);

                    SQLiteCommand komand = new SQLiteCommand(connection);
                    komand.CommandText = $"SELECT *\r\nFROM Clients c \r\ninner join Transactions t on IdClient = c.id\r\nwhere c.Id = '{MainWindow.Chosen.Id}'";

                    var x = komand.ExecuteQuery<Transaction>();

                    Table.OrderByID(x.OrderBy(x => x.id).Reverse().ToList());
                    //Table.OrderByDate(x.OrderBy(x => x.id).Reverse().ToList());
                }
            }


        }
    }
}
