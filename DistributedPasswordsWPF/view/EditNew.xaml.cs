using DstPasswordsCore.model;
using DstPasswordsCore.model.dataobjects;
using DstPasswordsCore.model.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private EncryptedEntry ee;

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
            
            User.SelectedIndex = -1;
            //User.SelectedItem = null;
            //User.ItemsSource = null;
            entry = null;
            SelectedUsername = null;
            PasswordBox1.Password = "";
            PasswordBox2.Password = "";
            NotesBox.Text = "";
            EmailBox.Text = "";
            
            IdBox.Text = "";
            _pwvisible = false;
            ee = null;

            

            OnPropertyChanged();
        }

        protected void OnPropertyChanged(string name="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            
        }

        


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (_checkDataChanged())
            {
                MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Leave anyway?", "Data modified", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    _reset();
                    Router.instance.DisplayPage(Router.Pages.Main);
                }

            }
            else
            {
                _reset();
                Router.instance.DisplayPage(Router.Pages.Main);
            }

            
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_isPasswordValid())
            {

                if (ee == null)
                {
                    //new
                    var ee_local = ee;
                    do
                    {
                        ee_local = EncryptedEntry.FromDecrypted(this.Entry);
                    } while (!ee_local.Save());
                    ee = ee_local;
                }
                else
                {
                    ee.Update(Entry);
                }
                
                Router.instance.DisplayPage(Router.Pages.Main);
            }
            else
            {
                _pw();
            }
            _checkDataChanged();

        }


        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.GenPW);
        }

        private List<EncryptedEntry> entries;
        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            entries = PasswordSystem.Instance.ReadDatabase();
            _pwvisible = false;
            TFAContent.Visibility = Visibility.Hidden;

            if (Router.instance.Payload == null)
            {
                //abort password manager:
                _comboboxChanged();
                return;
            }
            if (Router.instance.Payload.GetType() == typeof(Mode)){

                if ((Mode)Router.instance.Payload  == Mode.NEW)
                {
                    //new
                    ee = null;

                    _mode = Mode.NEW;
                    this.entry = new PasswordEntry(null);
                }
                else
                {

                    //back from password generator
                    _comboboxChanged();
                }

            }
            else if (Router.instance.Payload.GetType() == typeof(EncryptedEntry))
            {

                _mode = Mode.EDIT;
                ee = Router.instance.Payload as EncryptedEntry;
                this.entry = ee.Decrypt;

            }
            else if (Router.instance.Payload.GetType() == typeof(string))
            {
                //password generator
                PasswordBox1.Password = Router.instance.Payload as string;
                PasswordBox2.Password = Router.instance.Payload as string;
                SelectedUsername.Password = Router.instance.Payload as string;
            }

            PasswordBoxVisible.Visibility = Visibility.Collapsed;
            PasswordBox1.Visibility = Visibility.Visible;
            PasswordBox2.Visibility = Visibility.Visible;
            PasswordBoxVisible.Text = "";
            ShowHidePwdBtn.Content = "Show";

            _checkUsernameSize();
            this.DataContext = this;

            PropertyChanged += (o, ea) =>
            {
                //Debug.WriteLine("check");
            };

            OnPropertyChanged();

            //Debug.WriteLine("page load");


        }

        private  bool _checkDataChanged()
        {

            if (ee == null)
            {
                Save.Content = "Save";
                return false;
            }
            PasswordEntry tmp = ee?.Decrypt;
            
            //Debug.WriteLine(tmp.Id);
            //Debug.WriteLine(entry.Id);
            if (!tmp.Equals(entry))
            {
                Save.Content = "Save*";
                return true;
            }
            else
            {
                Save.Content = "Save";
                return false;
            }


        }

        private void _checkUsernameSize()
        {
            if (entry?.Usernames?.Count > 0)
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
            IdBox.Text = URL.URLize(IdBox.Text);
            Entry.Id = IdBox.Text;
            OnPropertyChanged("Id");
            
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
            _checkDataChanged();


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
            MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Delete "+SelectedUsername.Name+"?", "Delete", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                entry.Remove(SelectedUsername);
                _checkUsernameSize();
                _comboboxChanged();
                OnPropertyChanged();
            }
            
            

        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            string ret = Microsoft.VisualBasic.Interaction.InputBox("Username:", "Username", "");
            if (!string.IsNullOrEmpty(ret))
            {
                SelectedUsername.Name = ret;
                OnPropertyChanged();

            }

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
            _pw();
        }

        private void PasswordBox1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _pw();
            _checkDataChanged();

        }

        private void PasswordBox2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _pw();
            _checkDataChanged();

        }

        private void PasswordBoxVisible_TextChanged(object sender, TextChangedEventArgs e)
        {
            _pw();
            _checkDataChanged();

        }

        private void _pw()
        {
            if (!_isPasswordValid())
            {
                PasswordInfo.Content = "passwords are different";
            }
            else
            {
                PasswordInfo.Content = "";

            }
        }
        
        private bool _isPasswordValid()
        {
            if (_pwvisible)
            {
                return true;
            }

            return PasswordBox1.Password == PasswordBox2.Password;
        }

        private void PasswordBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (_isPasswordValid())
            {
                this.SelectedUsername.Password = PasswordBox1.Password;
            }
            

        }

        private void PasswordBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (_isPasswordValid())
            {
                this.SelectedUsername.Password = PasswordBox2.Password;
            }

        }

        private void PasswordBoxVisible_KeyUp(object sender, KeyEventArgs e)
        {
            this.SelectedUsername.Password = PasswordBoxVisible.Text;
    

        }

        private void QR_Click(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.QR, selectedUsername);
        }

        private void CPY_Usrname(object sender, RoutedEventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, SelectedUsername.Name);

            
        }

        private string tfaotp;
        public string TFAOTPValue
        {
            get
            {
                return tfaotp;
            }

            set
            {

                tfaotp = TFAHelper.AddPadding(value);
                    
            }
        }
        private Auth2FAUpdateEvent tfaEvent;
        private void TFAContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            string content = TFAContent.Text;
            TFAOTPValue = "";
            
            tfaEvent?.UnsubscribeAll();

            if (string.IsNullOrEmpty(content))
            {
                //Debug.WriteLine("null");
                return;
            }
            string param = TFAHelper.TryParseUrl(content);
            

            try
            {
                int i = Auth2FA.GenerateOTP(param);
                TFAOTPValue = ""+i;
                tfaEvent = Auth2FA.GenerateRenewableOTP(param);
                tfaEvent.Subscribe((src, otp) =>
                {
                    TFAOTPValue = "" + otp;
                    OnPropertyChanged("TFAOTPValue");
                    //Debug.WriteLine("+++++++++update++++++++++");

                });
            }
            catch (Exception)
            {
                //pass
                //Debug.WriteLine("err");
            }
            _checkDataChanged();

        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            tfaEvent?.UnsubscribeAll();
            tfaEvent = null;

        }

        private void TfaLockBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TFAContent.Visibility == Visibility.Visible)
            {
                TFAContent.Visibility = Visibility.Hidden;

            }
            else
            {
                TFAContent.Visibility = Visibility.Visible;
            }
            
        }

        private void IdBox_TextChanged(object sender, TextChangedEventArgs targs)
        {
            EncryptedEntry entry = entries.Find(e => e.Id == IdBox.Text);
            PasswordInfo.Foreground = System.Windows.Media.Brushes.Black;
            if (entry != null && entry.Id != ee?.Id)
            {
                if (_mode == Mode.EDIT)
                {
                    PasswordInfo.Foreground = System.Windows.Media.Brushes.Red ;

                }
                
                PasswordInfo.Content = "WARNING: This ID already exists!!!";

            }
            else
            {
                PasswordInfo.Content = "";

            }
            _checkDataChanged();

        }

        private void KeyDownAll(object sender, KeyEventArgs e)
        {
            
       
        }

        private void EmailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _checkDataChanged();

        }

        private void NotesBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _checkDataChanged();

        }

        private void CPY_Password(object sender, RoutedEventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, SelectedUsername.Password);
        }
    }
}
