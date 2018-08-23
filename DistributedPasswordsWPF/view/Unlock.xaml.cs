using DistributedPasswordsWPF.model;
using DstPasswordsCore.model;
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
            PasswordBox.Password = "";
            if (!PasswordSystem.Instance.IsHeaderFilePresent())
            {
                Info.Content = "Header file not present, type in a new password to create one";
            }
            else
            {
                Info.Content = "";
            }

            FocusManager.SetFocusedElement(this, PasswordBox);


        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.PathSettings);
        }

        private async void PasswordBoxUp(object sender, KeyEventArgs e)
        {
            string inp = PasswordBox.Password;
            
            Info.Content = "";
            if (e.Key == Key.Enter)
            {
                PasswordBox.Password = "";

                if (string.IsNullOrEmpty(inp))
                {
                    return;
                }
                if (!PasswordSystem.Instance.IsHeaderFilePresent())
                {
                    PasswordSystem.Instance.CreateHeader(inp);
                    Info.Content = "Header created, retype your password to unlock";
                }
                else
                {
                    PasswordBox.IsEnabled = false;
                    bool op = await PasswordSystem.Instance.Unlock(inp);
                    PasswordBox.IsEnabled = true;
                    if (!op)
                    {
                        Info.Content = "Wrong password" + String.Concat(Enumerable.Repeat(".", wrongpasswordcounter));
                        wrongpasswordcounter++;
                    }
                    else
                    {
                        Router.instance.DisplayPage(Router.Pages.Main);
                    }
                }
                //TODO check password, if correct, route
                //Router.instance.DisplayPage(Router.Pages.Main);
            }
        }
    }
}
