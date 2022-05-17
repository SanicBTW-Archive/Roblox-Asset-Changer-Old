using System;
using System.Collections.Generic;
using System.Linq;
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

using Roblox_Asset_Changer.Assets;

namespace Roblox_Asset_Changer.Pages
{
    /// <summary>
    /// Lógica de interacción para CursorPage.xaml
    /// </summary>
    public partial class CursorPage : Page
    {
        public CursorPage()
        {
            InitializeComponent();
        }

        #region Load Page / Pagina cargada
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            #region Load Images

            #region Arrow Cursor
            if(System.IO.Directory.Exists(ChangerClass.robpath))
            {
                BitmapImage ArrowCursorImageWorker = new BitmapImage();
                ArrowCursorImageWorker.BeginInit();
                ArrowCursorImageWorker.CacheOption = BitmapCacheOption.OnLoad;
                ArrowCursorImageWorker.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                ArrowCursorImageWorker.UriSource = new Uri(ChangerClass.acdir);
                ArrowCursorImageWorker.EndInit();
                //preview with skybox
                ArrowCursorPrevwSky.Source = ArrowCursorImageWorker;

                //preview without skybox
                ArrowCursorPrev.Source = ArrowCursorImageWorker;

                //preview with custom color
                ArrowCursorPrevCustColor.Source = ArrowCursorImageWorker;
            }
            #endregion

            #endregion
        }
        #endregion

        #region Eventos de click / Click btn events
        private void ChangeACBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangerClass.currentlyChanging = "Arrow Cursor";
            ChangerClass.MainChanger("Cursor");
        }
        #endregion
    }
}
