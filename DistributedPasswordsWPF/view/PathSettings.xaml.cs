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

            
            string dbp = Settings.DB_PATH;
            DBPath.Content = dbp;

            

            
            string keys = Settings.KEYS_PATH;
            KeysPath.Content = keys;

            

        }

        private bool _isOKFolder(string s)
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
            
            _pathsOK(() =>
            {
                string dbString = DBPath.Content.ToString();
                string keysString = KeysPath.Content.ToString();

                Settings.DB_PATH = dbString;
                Settings.KEYS_PATH = keysString;
            });
                
            

        }

        private void _pathsOK(Action success = null, Router.Pages page = Router.Pages.Unlock, object payload = null)
        {
            string dbString = DBPath.Content.ToString();
            string keysString = KeysPath.Content.ToString();
            bool dbOk = _isOKFolder(dbString);
            bool keysOk = _isOKFolder(keysString);

            if (dbOk && keysOk)
            {

                success?.Invoke();
                Router.instance.DisplayPage(page, payload);

            }
            else
            {
                String s = (string)FindResource("PathSettings_InvalidFolder");
                System.Windows.MessageBox.Show(s);
            }

            
            
        }

      

        private void BrowseDB_Click(object sender, RoutedEventArgs e)
        {
            _labelFileChooser(DBPath);

        }

        private void _labelFileChooser(System.Windows.Controls.Label lbl)
        {


            using (var fbd = new FolderBrowserDialog())
            {
                if (_isOKFolder(lbl.Content.ToString()))
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
            _labelFileChooser(KeysPath);
        }

        private void Backbutton_Click(object sender, RoutedEventArgs e)
        {
            _pathsOK();
        }
    }
}
