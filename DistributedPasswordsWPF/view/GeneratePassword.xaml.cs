using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace DistributedPasswordsWPF.view
{
    /// <summary>
    /// Interaktionslogik für GeneratePassword.xaml
    /// </summary>
    public partial class GeneratePassword : Page
    {
        public GeneratePassword()
        {
            InitializeComponent();
        }
        readonly Random random = new Random();
        const string UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string LOWER = "abcdefghijklmnopqrstuvwxyz";
        const string DIGITS = "0123456789";
        const string SPECIAL = "!\"§$%&/()=?ß\\}][{+*~#-_|<>,.";
       

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.EditNew, Pw.Text);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.EditNew);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string generator = CustomChars.Text;

            if (UpperChk.IsChecked ?? true)
            {
                generator = string.Concat(generator, UPPER);
            }
            if (LowerChk.IsChecked ?? true)
            {
                generator = string.Concat(generator, LOWER);
            }
            if (DigitsChk.IsChecked ?? true)
            {
                generator = string.Concat(generator, DIGITS);
            }
            if (SpecialChk.IsChecked ?? true)
            {
                generator = string.Concat(generator, SPECIAL);
            }
            
            Pw.Text = new string(Enumerable.Repeat(generator, int.Parse(Len.Text))
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
