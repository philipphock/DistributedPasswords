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
        public enum Pages { Main, Unlock, EditNew };
        private Dictionary<Pages, Page> pagesLUT = new Dictionary<Pages, Page>();
        
        private MainWindow window;
        private readonly Page mainView;
        private readonly Page unlock;
        private readonly Page editnew;


        private ViewModel()
        {
            mainView    = new MainView();
            unlock      = new Unlock();
            editnew     = new EditNew();

            pagesLUT.Add(Pages.Main, mainView);
            pagesLUT.Add(Pages.Unlock, unlock);
            pagesLUT.Add(Pages.EditNew, editnew);

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
