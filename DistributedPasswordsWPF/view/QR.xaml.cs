using DstPasswordsCore.model.dataobjects;
using QRCoder;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DistributedPasswordsWPF.view
{
    /// <summary>
    /// Interaktionslogik für QR.xaml
    /// </summary>
    public partial class QR : Page
    {
        private readonly QRCodeGenerator qrGenerator;
        private Username username;
        public QR()
        {
            InitializeComponent();

            qrGenerator = new QRCodeGenerator();
        }
        



        private void back_Click(object sender, RoutedEventArgs e)
        {
            Router.instance.DisplayPage(Router.Pages.EditNew);
        }

        //password
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ToBitmap(username.Password);
        }

        //username
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ToBitmap(username.Name);

        }

        //email
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ToBitmap(username.Email);

        }

        //notes
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ToBitmap(username.Notes);

        }

        //Custom
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string ret = Microsoft.VisualBasic.Interaction.InputBox("Text:", "Any text", "");
            if (!string.IsNullOrEmpty(ret))
            {
                ToBitmap(ret);


            }
        }

        private void ToBitmap(string s)
        {
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(s, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            System.Drawing.Bitmap bitmap = qrCode.GetGraphic(20);
            
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                QRImage.Source = bitmapImage;

            }

        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            username = Router.instance.Payload as Username;
            Id.Content = username.Name;
            QRImage.Source = null;

        }
    }
}
