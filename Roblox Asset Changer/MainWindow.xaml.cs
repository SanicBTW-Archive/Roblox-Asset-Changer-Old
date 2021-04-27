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
using System.Configuration;
using Roblox_Asset_Changer.Pages;
using Roblox_Asset_Changer.Logging;
using Pixelmaniac.Notifications;
using System.Runtime.InteropServices;
using System.Threading;

namespace Roblox_Asset_Changer
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Window events
        private void MainAppWindow_ContentRendered(object sender, EventArgs e)
        {
            Title = "Roblox Asset Changer - " + Properties.Settings.Default.AppVersion + ", " + Properties.Settings.Default.BuildNumber;

            #region Logging stuff same with console logging stuff not necessary or somethin
            /*
            string UserConf = GetDefaultExeConfigPath(ConfigurationUserLevel.PerUserRoamingAndLocal);
            string registros = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), UserConf.Replace("user.config", Properties.Settings.Default.AppVersion));
            Log oLog = new Log(registros);

            oLog.Add("Application started! Roblox Asset Changer made by Sanic, any Roblox assets belongs to its trademark, you are currently using: " + Properties.Settings.Default.AppVersion + " currently trying to do an updater");
            oLog.Add("Log viewer currently in progress, its currently unavailable");
            */

            #endregion
            #region Notifications and the notification manager thingy or placeholder idk

            var notificationManager = new NotificationManager();
            notificationManager.Options.InAppNotificationPlacement = true;

            if (Properties.Settings.Default.languageCode.Contains("en-US"))
            {
                var TestingNotification = new NotificationContent
                {
                    Title = "Notification",
                    Message = "Yeah placeholder notification, just for the future and check if this thing really works",
                    AppIdentity = "Roblox Asset Changer",
                    AttributionText = Properties.Settings.Default.AppVersion
                };

                notificationManager.Notify(TestingNotification);

                //oLog.Add("TestingNotification has been notified in the application");
            }
            else if (Properties.Settings.Default.languageCode.Contains("es-ES"))
            {
                var TestingNotification = new NotificationContent
                {
                    Title = "Notificación",
                    Message = "Si una notificación placeholder, solo para el futuro y para ver si esto de verdad funciona",
                    AppIdentity = "Roblox Asset Changer",
                    AttributionText = Properties.Settings.Default.AppVersion
                };

                notificationManager.Notify(TestingNotification);

                //oLog.Add("TestingNotification has been notified in the application");
            }
            #endregion
        }
        #endregion

        #region Window Behaviour
        private void AppMenuNav_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AppMenuNav.SelectedItem == HomePagelvi)
            {
                appcontentFrame.Navigate(typeof(HomePage));
                AppMenuDrawer.IsLeftDrawerOpen = false;
            }
            else if (AppMenuNav.SelectedItem == CursorPagelvi)
            {
                appcontentFrame.Navigate(typeof(CursorPage));
                AppMenuDrawer.IsLeftDrawerOpen = false;
            }
            else if (AppMenuNav.SelectedItem == FacePagelvi)
            {
                appcontentFrame.Navigate(typeof(FacePage));
                AppMenuDrawer.IsLeftDrawerOpen = false;
            }
            else if (AppMenuNav.SelectedItem == LoadingGamePagelvi)
            {
                appcontentFrame.Navigate(typeof(LoadingGamePage));
                AppMenuDrawer.IsLeftDrawerOpen = false;
            }
            else if (AppMenuNav.SelectedItem == RobloxMenuPagelvi)
            {
                //Roblox Menuy Page aqui
                appcontentFrame.Navigate(typeof(RobloxMenuPage));
                AppMenuDrawer.IsLeftDrawerOpen = false;
            }
            else if (AppMenuNav.SelectedItem == TopBarPagelvi)
            {
                //Top Bar Page aqui
                appcontentFrame.Navigate(typeof(TopBarPage));
                AppMenuDrawer.IsLeftDrawerOpen = false;
            }
            else if (AppMenuNav.SelectedItem == SoundsPagelvi)
            {
                //Sounds Page aqui
                appcontentFrame.Navigate(typeof(SoundsPage));
                AppMenuDrawer.IsLeftDrawerOpen = false;
            }
            else if (AppMenuNav.SelectedItem == OptionsPagelvi)
            {
                appcontentFrame.Navigate(typeof(OptionsPage));
                AppMenuDrawer.IsLeftDrawerOpen = false;
            }
            else if (AppMenuNav.SelectedItem == UpdatePagelvi)
            {
                appcontentFrame.Navigate(typeof(UpdatePage));
                AppMenuDrawer.IsLeftDrawerOpen = false;
            }
            else if (AppMenuNav.SelectedItem == AboutPagelvi)
            {
                appcontentFrame.Navigate(typeof(AboutPage));
                AppMenuDrawer.IsLeftDrawerOpen = false;
            }
        }
        #endregion

        #region Helper
        public string GetDefaultExeConfigPath(ConfigurationUserLevel userLevel)
        {
            try
            {
                var UserConfig = ConfigurationManager.OpenExeConfiguration(userLevel);
                return UserConfig.FilePath;
            }
            catch (ConfigurationException e)
            {
                return e.Filename;
            }
        }
        #endregion
    }
}
