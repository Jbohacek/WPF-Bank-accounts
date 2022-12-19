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
    /// Interaction logic for Personal.xaml
    /// </summary>
    public partial class Personal : Page
    {
        public MainWindow ParentOf { get; set; }

        public Personal(MainWindow _ParentOf)
        {
            InitializeComponent();
            this.DataContext = MainWindow.Chosen;
            ParentOf = _ParentOf;
        }

        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            var textbox1 = (TextBox)DatePicker.Template.FindName("PART_TextBox", DatePicker);
            textbox1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00ADB5"));
            textbox1.CaretBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EEEEEE"));
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHandler NewDatabaseHandled = new DatabaseHandler("BankAccounts");

            NewDatabaseHandled.UpdateByObject<Person>(MainWindow.Chosen);
            ParentOf.ShowSave();
            ParentOf.Reload();
        }
    }
}
