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
    /// Interaction logic for RegisterControl.xaml
    /// </summary>
    public partial class RegisterControl : UserControl
    {
        public RegisterControl()
        {
            InitializeComponent();

        }

        private void BirthDate_Loaded(object sender, RoutedEventArgs e)
        {
            var textbox1 = (TextBox)BirthDate.Template.FindName("PART_TextBox", BirthDate);
            textbox1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00ADB5"));
            textbox1.CaretBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EEEEEE"));

        }
    }
}
