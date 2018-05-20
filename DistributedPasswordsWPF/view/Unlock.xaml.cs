using DistributedPasswordsWPF.model;
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

namespace DistributedPasswordsWPF
{
    /// <summary>
    /// Interaktionslogik für Unlock.xaml
    /// </summary>
    public partial class Unlock : Page
    {
        private int wrongpasswordcounter = 0;
        public Unlock()
        {
            InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            wrongpasswordcounter = 0;
            if (!PasswordSystem.Instance.IsHeaderFilePresent())
            {
                Info.Content = "Header file not present, type in a new password to create one";
            }
            else
            {
                Info.Content = "";
            }
                        
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.PathSettings);
        }

        private void PasswordBoxUp(object sender, KeyEventArgs e)
        {
            Info.Content = "";
            if (e.Key == Key.Enter)
            {
                if (!PasswordSystem.Instance.IsHeaderFilePresent())
                {
                    PasswordSystem.Instance.CreateHeader(PasswordBox.Password);
                    Info.Content = "Header created, retype your password to unlock";
                }
                else
                {
                    bool op = PasswordSystem.Instance.Unlock(PasswordBox.Password);
                    if (!op)
                    {
                        Info.Content = "Wrong password" + String.Concat(Enumerable.Repeat(".", wrongpasswordcounter));
                    }

                }
                //TODO check password, if correct, route
                //Router.instance.DisplayPage(Router.Pages.Main);
            }
        }
    }
}
