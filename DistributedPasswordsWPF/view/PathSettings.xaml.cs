using DistributedPasswordsWPF.model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
        
        public PathSettings()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        private void PageLoaded(object sender, RoutedEventArgs e)
        {

            try
            {
                string dbp = Settings.DB_PATH;
                DBPath.Content = dbp;

            }
            catch (ArgumentException)
            {
                DBPath.Content = "PATH/TO/DB";
            }


            try
            {
                string keys = Settings.KEYS_PATH;
                KeysPath.Content = keys;

            }
            catch (ArgumentException)
            {
                KeysPath.Content = "PATH/TO/KEYS";
            }

        }

        private bool isOKFolder(string s)
        {
            if (!Directory.Exists(s))
            {
                return false;
            }
            FileAttributes attr = File.GetAttributes(s);
            return attr.HasFlag(FileAttributes.Directory);                
            
        }
        private void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            string dbString = DBPath.Content.ToString();
            string keysString = DBPath.Content.ToString();
            bool dbOk = isOKFolder(dbString);
            bool keysOk = isOKFolder(keysString);

            if (dbOk && keysOk)
            {
                Settings.DB_PATH = dbString;
                Settings.KEYS_PATH = keysString;
                Router.instance.DisplayPage(Router.Pages.Unlock);

            }
            else
            {
                System.Windows.MessageBox.Show("The path is not an existing folder");
            }

        }

        private void Backbutton_Click(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.Unlock);

        }

        private void BrowseDB_Click(object sender, RoutedEventArgs e)
        {
            labelFileChooser(DBPath);

        }

        private void labelFileChooser(System.Windows.Controls.Label lbl)
        {


            using (var fbd = new FolderBrowserDialog())
            {
                if (isOKFolder(lbl.Content.ToString()))
                {
                    fbd.SelectedPath = lbl.Content.ToString();
                }
                else
                {
                    fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                }
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    lbl.Content = fbd.SelectedPath.ToString();
                }
            }
        }

        private void BrowseKeys_Click(object sender, RoutedEventArgs e)
        {
            labelFileChooser(KeysPath);
        }
    }
}
