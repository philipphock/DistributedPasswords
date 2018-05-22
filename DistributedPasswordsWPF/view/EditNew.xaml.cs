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
        private enum Mode
        {
            EDIT,NEW
        }

        private Mode _mode;


        private PasswordEntry entry;
        public PasswordEntry Entry { get => entry; set => entry = value; }

        private Username selectedUsername;
        public Username SelectedUsername { get => selectedUsername; set => selectedUsername = value; }


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
            BindingExpression be = EmailBox.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            DEBUG.Print(this.GetType(), SelectedUsername.Email);
            _reset();
            Router.instance.DisplayPage(Router.Pages.Main);
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.GenPW);
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            if (Router.instance.Payload == null)
            {
                //abort password manager:
                _comboboxChanged();
                
                return;
            }
            if (Router.instance.Payload.GetType() == typeof(string)){

                if (Router.instance.Payload as string == "new")
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
                PasswordEntry entry = Router.instance.Payload as PasswordEntry;
                this.entry = entry;
                
                
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

                //enable fields
            }
            else
            {
                //disable fields
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

      

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            _comboboxChanged();
        }
      
        private void _comboboxChanged()
        {
            PasswordBox1.Password = SelectedUsername?.Password;
            PasswordBox2.Password = SelectedUsername?.Password;
        }
    }
}
