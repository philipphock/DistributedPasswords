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

namespace DistributedPasswordsWPF.view
{
    /// <summary>
    /// Interaktionslogik für Page1.xaml
    /// </summary>
    public partial class PathSettings : Page
    {
        private string _InfoDBText = "";

        public PathSettings()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.Unlock);
            //TODO Save
        }

        private void Backbutton_Click(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.Unlock);

        }
    }
}
