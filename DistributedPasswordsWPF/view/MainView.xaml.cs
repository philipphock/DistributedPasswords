using DistributedPasswordsWPF.model;
using DstPasswordsCore.model;
using DstPasswordsCore.model.dataobjects;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            PasswordSystem.Instance.LockedHandler += (o, a) =>
            {
                listView.ItemsSource = null;
            };
        }

        private void _getData()
        {
            List<EncryptedEntry> data = PasswordSystem.Instance.ReadDatabase().OrderBy(s => s.Id).ToList();
            

            listView.ItemsSource = data;
            Debug.WriteLine("getdata");

        }

        private void Lock_Click(object sender, RoutedEventArgs e)
        {
            PasswordSystem.Instance.Lock();
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

        public void UpdateData()
        {
            _getData();
        }

        //new button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.EditNew, EditNew.Mode.NEW);
        }

        //select button
        private void Button_Item_Select(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            EncryptedEntry entry = button.DataContext as EncryptedEntry;
            Clipboard.SetData(DataFormats.Text, entry.Id);
        }

        //edit button
        private void Button_Item_Edit(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            EncryptedEntry entry = button.DataContext as EncryptedEntry;
            Router.instance.DisplayPage(Router.Pages.EditNew, entry);


        }

        //delete button
        private void Button_Item_Delete(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            EncryptedEntry entry = button.DataContext as EncryptedEntry;

            MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Delete " + entry.Id + "?", "Delete", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                File.Delete(System.IO.Path.Combine(PasswordSystem.Instance.Settings.DB_PATH, entry.Filename));
                _getData();
            }
        }

        private void Button_Item_CPYUN(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            EncryptedEntry entry = button.DataContext as EncryptedEntry;
                
            Clipboard.SetData(DataFormats.Text, entry.Decrypt.Usernames.ElementAtOrDefault(0)?.Name);
        }

        private void Button_Item_CPYPW(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            EncryptedEntry entry = button.DataContext as EncryptedEntry;

            Clipboard.SetData(DataFormats.Text, entry.Decrypt.Usernames.ElementAtOrDefault(0)?.Password);
        }
        //search button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _filter();
        }

        private void _filter()
        {
            listView.ItemsSource = PasswordSystem.Instance.Filter(SearchBox.Text);
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _filter();
            }
            if (e.Key == Key.Escape)
            {
                SearchBox.Text = "";
                _filter();
            }
        }

        
        public void OnMainWindowChanged(object sender, MainWindow.WinState s)
        {
            if (s.WindowState == MainWindow.WinState.State.Hide)
            {
                SearchBox.Text = "";
                _filter();
            }

            if (s.WindowState == MainWindow.WinState.State.Show)
            {
                _getData();
            }
            
        }
    }
}
