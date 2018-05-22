using DistributedPasswordsWPF.model.dataobjects;
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
    /// Interaktionslogik für EditNew.xaml
    /// </summary>
    public partial class EditNew : Page
    {
        private enum Mode
        {
            EDIT,NEW
        }

        private Mode _mode;

        public EditNew()
        {
            InitializeComponent();
        }

        
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.Main);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.Main);
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.GenPW);
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            if (Router.instance.Payload.GetType() == typeof(string)){
                //new
                _mode = Mode.NEW;
            }
            else
            {
                _mode = Mode.EDIT;
                PasswordEntry entry = Router.instance.Payload as PasswordEntry;
            }
        }

        private void UrlizeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowHidePwdBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
