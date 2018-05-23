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
    /// Interaktionslogik für Page1.xaml
    /// </summary>
    public partial class ChangePW : Page
    {
        public ChangePW()
        {
            InitializeComponent();
        }

        

        public bool IsPasswordValid
        {
            get
            {
                return NewPW1.Password == NewPW1.Password;

            }
        }
        

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsPasswordValid)
            {
                if (PasswordSystem.Instance.ChangePassword(OldPW.Password, NewPW1.Password))
                {
                    Router.instance.DisplayPage(Router.Pages.Main);
                }
                //TODO handle wrong old pw
            }
            else
            {
                //TODO
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.Main);

        }
    }
}
