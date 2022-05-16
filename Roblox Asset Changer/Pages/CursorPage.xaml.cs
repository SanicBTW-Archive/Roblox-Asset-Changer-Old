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

        #region Eventos de click / Click btn events
        private void ChangeACBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangerClass.currentlyChanging = "Arrow Cursor";
            ChangerClass.MainChanger("Cursor");
        }
        #endregion
    }
}
