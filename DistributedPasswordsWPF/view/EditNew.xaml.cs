using DistributedPasswordsWPF.debug;
using DistributedPasswordsWPF.model.dataobjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class EditNew : Page, INotifyPropertyChanged
    {
        public enum Mode
        {
            EDIT,NEW
        }

        private Mode _mode;

        private bool _pwvisible;

        private PasswordEntry entryToOverride;

        private PasswordEntry entry;
        public PasswordEntry Entry { get => entry; set => entry = value; }

        private Username selectedUsername;
        public Username SelectedUsername { get => selectedUsername; set => selectedUsername = value; }

        public bool UserActive
        {
            get
            {
                return SelectedUsername != null;
            }
        }

        public EditNew()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void _reset()
        {
            entry = null;
            SelectedUsername = null;
            PasswordBox1.Password = "";
            PasswordBox2.Password = "";
            NotesBox.Text = "";
            EmailBox.Text = "";
            IdBox.Text = "";
            _pwvisible = false;
            OnPropertyChanged();
        }

        protected void OnPropertyChanged(string name="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _reset();
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
            _pwvisible = false;
            if (Router.instance.Payload == null)
            {
                //abort password manager:
                //_comboboxChanged();
                
                return;
            }
            if (Router.instance.Payload.GetType() == typeof(Mode)){

                if ((Mode)Router.instance.Payload  == Mode.NEW)
                {
                    //new

                    _mode = Mode.NEW;
                    this.entry = new PasswordEntry();
                }
                else
                {
                    //back from password generator
                    _comboboxChanged();
                }
                
                
            }
            else
            {
                _mode = Mode.EDIT;
                entryToOverride = Router.instance.Payload as PasswordEntry;
                this.entry = entryToOverride.DeepClone();
                
                
            }

            _checkUsernameSize();
            this.DataContext = this;
            OnPropertyChanged();



        }

        private void _checkUsernameSize()
        {
            if (entry.Usernames.Count > 0)
            {

                SelectedUsername = entry.Usernames[0];
                OnPropertyChanged("SelectedUsername");
                OnPropertyChanged("UserActive");
                
                //enable fields
            }
            else
            {
                //disable fields
            }
        }

        private void UrlizeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var url = new Uri(IdBox.Text);
                var fragments = url.Authority.Split('.');
                if (fragments.Length > 2)
                {
                    IdBox.Text = fragments[fragments.Length - 2] + "." + fragments[fragments.Length - 1];
                }
                else
                {
                    IdBox.Text = url.Authority;
                }
            }
            catch (UriFormatException)
            {
                //no uri
            }
            
            
            
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

      

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            _comboboxChanged();
            
        }
      
        private void _comboboxChanged()
        {

            SelectedUsername = User.SelectedItem as Username;
            OnPropertyChanged("SelectedUsername");
            OnPropertyChanged("UserActive");
            PasswordBox1.Password = SelectedUsername?.Password;
            PasswordBox2.Password = SelectedUsername?.Password;
            

        }

        private void AddUserBtn_Click_1(object sender, RoutedEventArgs e)
        {
            string ret = Microsoft.VisualBasic.Interaction.InputBox("Username:", "Username", "");
            if (!string.IsNullOrEmpty(ret))
            {
                Username n = new Username
                {
                    Name = ret
                };

                entry.Add(n);
            }
            _checkUsernameSize();
        }

        private void RemoveBtn_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {

        }
        

        private void ShowHidePwdBtn_Click_1(object sender, RoutedEventArgs e)
        {
            _pwvisible = !_pwvisible;
            if (_pwvisible)
            {
                PasswordBoxVisible.Visibility = Visibility.Visible;
                PasswordBox1.Visibility = Visibility.Collapsed;
                PasswordBox2.Visibility = Visibility.Hidden;
                PasswordBoxVisible.Text = PasswordBox1.Password;
                ShowHidePwdBtn.Content= "Hide";
            }
            else
            {
                
                PasswordBoxVisible.Visibility = Visibility.Collapsed;
                PasswordBox1.Visibility = Visibility.Visible;
                PasswordBox2.Visibility = Visibility.Visible;
                PasswordBox1.Password = PasswordBoxVisible.Text;
                PasswordBox2.Password = PasswordBoxVisible.Text;
                PasswordBoxVisible.Text = "";
                ShowHidePwdBtn.Content = "Show";
            }
            
        }
    }
}
