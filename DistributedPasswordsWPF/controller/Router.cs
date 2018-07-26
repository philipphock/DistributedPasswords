using DistributedPasswordsWPF.view;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DistributedPasswordsWPF
{
    public class Router
    {
        public enum Pages { Main, Unlock, EditNew, ChangePW, PathSettings,GenPW, QR };
        private Dictionary<Pages, Page> pagesLUT = new Dictionary<Pages, Page>();
        
        private MainWindow window;
        private readonly MainView mainView;
        private readonly Page unlock;
        private readonly Page editnew;
        private readonly Page changepw;
        private readonly Page pathsettings;
        private readonly Page generatePW;
        private readonly Page qr;

        public void HideMain()
        {
            window.Visibility = System.Windows.Visibility.Hidden;
        }

        public async void ShowMain()
        {
            window.Visibility = System.Windows.Visibility.Visible;
            await Task.Delay(100);
            window.WindowState = System.Windows.WindowState.Normal;
            window.Activate();
            //window.Topmost = true;

        }

        private Router()
        {
            mainView    = new MainView();
            unlock      = new Unlock();
            editnew     = new EditNew();
            pathsettings = new PathSettings();
            changepw = new ChangePW();
            generatePW = new GeneratePassword();
            qr = new QR();

            pagesLUT.Add(Pages.Main, mainView);
            pagesLUT.Add(Pages.Unlock, unlock);
            pagesLUT.Add(Pages.EditNew, editnew);
            pagesLUT.Add(Pages.ChangePW, changepw);
            pagesLUT.Add(Pages.PathSettings, pathsettings);
            pagesLUT.Add(Pages.GenPW, generatePW);
            pagesLUT.Add(Pages.QR, qr);

            
                



        }

        public void Init(MainWindow window)
        {
            this.window = window;
            window.WinHandler += mainView.OnMainWindowChanged;

        }

        public void DisplayPage(Pages page)
        {
            _payload = null;
            window.Main.Content = pagesLUT[page];
        }

        public void DisplayPage(Pages page, object payload)
        {
            _payload = payload;
            window.Main.Content = pagesLUT[page];
        }

        private object _payload;
        public object Payload
        {
            get
            {
                return _payload;
            }
        }


        public static Router instance = new Router();
    }
}
