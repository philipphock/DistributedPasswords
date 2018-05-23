using DistributedPasswordsWPF.model;

using System.Windows;


namespace DistributedPasswordsWPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Router.instance.init(this);
            PasswordSystem.Instance.Init();

            System.Windows.Forms.NotifyIcon notifyIcon = null;

            

            notifyIcon = new System.Windows.Forms.NotifyIcon();


            notifyIcon.Icon = new System.Drawing.Icon("assets/img/tray.ico");
            notifyIcon.Visible = true;

            notifyIcon.Click += new System.EventHandler(_notifyIcon_Click);


        }

        void _notifyIcon_Click(object sender, System.EventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {

                Router.instance.HideMain();
            }
            else
            {
                Router.instance.ShowMain();
            }
        }
    }
}
