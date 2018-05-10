using DistributedPasswordsWPF.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DistributedPasswordsWPF
{
    class ViewModel
    {
        public enum Pages { Main, Unlock, EditNew, ChangePW, PathSettings };
        private Dictionary<Pages, Page> pagesLUT = new Dictionary<Pages, Page>();
        
        private MainWindow window;
        private readonly Page mainView;
        private readonly Page unlock;
        private readonly Page editnew;
        private readonly Page changepw;
        private readonly Page pathsettings;


        private ViewModel()
        {
            mainView    = new MainView();
            unlock      = new Unlock();
            editnew     = new EditNew();
            pathsettings = new PathSettings();

            pagesLUT.Add(Pages.Main, mainView);
            pagesLUT.Add(Pages.Unlock, unlock);
            pagesLUT.Add(Pages.EditNew, editnew);
            pagesLUT.Add(Pages.ChangePW, changepw);
            pagesLUT.Add(Pages.PathSettings, pathsettings);


        }

        public void init(MainWindow window)
        {
            this.window = window;
        }

        public void DisplayPage(Pages page)
        {
            window.Main.Content = pagesLUT[page];
        }

        public static ViewModel instance = new ViewModel();
    }
}
