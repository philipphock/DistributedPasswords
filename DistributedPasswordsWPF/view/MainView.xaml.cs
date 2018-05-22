using DistributedPasswordsWPF.model;
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
    /// Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : Page
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void _getData()
        {
            List<PasswordEntry> data = PasswordSystem.Instance.ReadDatabase();
            listView.ItemsSource = data;

        }

        private void Lock_Click(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.Unlock);
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            _getData();
        }
        private void ChgPW_Click(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.ChangePW);

        }

        //new button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.EditNew, "new");
        }

        //select button
        private void Button_Item_Select(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            PasswordEntry entry = button.DataContext as PasswordEntry;

        }

        //ei´dit button
        private void Button_Item_Edit(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            PasswordEntry entry = button.DataContext as PasswordEntry;
            Router.instance.DisplayPage(Router.Pages.EditNew, entry);


        }

        //delete button
        private void Button_Item_Delete(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            PasswordEntry entry = button.DataContext as PasswordEntry;
        }
    }
}
