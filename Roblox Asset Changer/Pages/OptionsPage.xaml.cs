using Microsoft.WindowsAPICodePack.Dialogs;
using ModernWpf.Controls;
using Pixelmaniac.Notifications;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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

namespace Roblox_Asset_Changer.Pages
{
    /// <summary>
    /// Lógica de interacción para OptionsPage.xaml
    /// </summary>
    public partial class OptionsPage
    {
        public OptionsPage()
        {
            InitializeComponent();
        }

        #region Comportamiento de la pagina al cargar
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            #region Cargar controles
            #region LanguageCB 
            if (Properties.Settings.Default.languageCode == "en-US")
            {
                LanguageCB.SelectedIndex = 0;
            }
            else if (Properties.Settings.Default.languageCode == "es-ES")
            {
                LanguageCB.SelectedIndex = 1;
            }

            #endregion
            #region rblxDirectory
            rblxDirectory.Text = Properties.LanguageResources.OptionsPageResources.Language.RobloxDirectoryCurrentRobloxDirectory + " " + Properties.Settings.Default.robloxlocationFolder;
            #endregion
            #region openrblxfolderlocation
            if (Properties.Settings.Default.robloxlocationFolder.Contains("C:"))
            {
                openrblxfolderlocation.Content = Properties.LanguageResources.OptionsPageResources.Language.RobloxDirectoryOpenFolderSelection;
            }
            else
            {
                openrblxfolderlocation.Content = Properties.LanguageResources.OptionsPageResources.Language.RobloxDirectorySelectFolder;
            }
            #endregion
            #endregion
        }
        #endregion

        #region Comportamiento de los controles
        #region Página de idioma
        private void LanguageCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageCB.SelectedIndex == 0)
            {
                Properties.Settings.Default.languageCode = "en-US";
                if (Properties.LanguageResources.OptionsPageResources.Language.PageHeaderText.Contains("Ajustes"))
                {
                    RestartPromptEN();
                }
            }
            else if (LanguageCB.SelectedIndex == 1)
            {
                Properties.Settings.Default.languageCode = "es-ES";
                if (Properties.LanguageResources.OptionsPageResources.Language.PageHeaderText.Contains("Options"))
                {
                    RestartPromptESP();
                }
            }
            Properties.Settings.Default.Save();
        }
        #endregion
        #region Página del directorio de Roblox
        private void openrblxfolderlocation_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.robloxlocationFolder.Contains("C:"))
            {
                System.Diagnostics.Process.Start("explorer.exe", Properties.Settings.Default.robloxlocationFolder);
                openrblxfolderlocation.Content = Properties.LanguageResources.OptionsPageResources.Language.RobloxDirectoryOpenFolderSelection;
            }

            else if (Properties.Settings.Default.languageCode.Contains("en-US"))
            {
                RobloxFolderSelectPromptEN();
            }
            else if (Properties.Settings.Default.languageCode.Contains("es-ES"))
            {
                RobloxFolderSelectPromptES();
            }
        }
        #endregion
        #endregion

        #region Asyncs para los prompts
        #region Prompt para reiniciar la aplicacion
        private async void RestartPromptEN()
        {
            ContentDialog restart = new ContentDialog
            {
                Title = "The application has to restart",
                Content = "To apply the selected language you have to restart the application for it to take effects",
                PrimaryButtonText = "Restart the application now",
                SecondaryButtonText = "Restart the application later",
                DefaultButton = ContentDialogButton.Primary
            };

            ContentDialogResult result = await restart.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
            if (result == ContentDialogResult.Secondary)
            {
                Application.Current.MainWindow.Title = "Roblox Asset Changer - " + Properties.Settings.Default.AppVersion + " | PLEASE RESTART THE APPLICATION TO APPLY CHANGES";
            }
        }

        private async void RestartPromptESP()
        {
            ContentDialog reinicio = new ContentDialog
            {
                Title = "La aplicación necesita reiniciarse",
                Content = "Para aplicar el idioma seleccionado se tiene que reiniciar la aplicación para que surja efecto",
                PrimaryButtonText = "Reiniciarla ahora",
                SecondaryButtonText = "Reiniciarla después",
                DefaultButton = ContentDialogButton.Primary
            };

            ContentDialogResult result = await reinicio.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
            if (result == ContentDialogResult.Secondary)
            {
                Application.Current.MainWindow.Title = "Roblox Asset Changer - " + Properties.Settings.Default.AppVersion + " | POR FAVOR REINICIA LA APLICACIÓN PARA QUE SE APLIQUEN LOS CAMBIOS";
            }
        }
        #endregion
        #region Prompt para elegir la carpeta de roblox
        private async void RobloxFolderSelectPromptEN()
        {
            string RobloxStarterDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Roblox", "Versions");

            var oldshelldlg = new CommonOpenFileDialog();

            oldshelldlg.Title = "Roblox Asset Changer " + Properties.Settings.Default.AppVersion + " - Roblox folder selection";
            oldshelldlg.IsFolderPicker = true;

            oldshelldlg.InitialDirectory = RobloxStarterDirectory;
            oldshelldlg.DefaultDirectory = RobloxStarterDirectory;

            oldshelldlg.AddToMostRecentlyUsedList = false;
            oldshelldlg.AllowNonFileSystemItems = false;
            oldshelldlg.EnsureFileExists = true;
            oldshelldlg.EnsurePathExists = true;
            oldshelldlg.EnsureReadOnly = false;
            oldshelldlg.EnsureValidNames = true;
            oldshelldlg.Multiselect = false;
            oldshelldlg.ShowPlacesList = true;

            ModernWpf.Controls.ContentDialog FolderSelect = new ModernWpf.Controls.ContentDialog
            {
                Title = "Roblox Folder Selection",
                Content = "A new window will now open to select the Roblox Folder, please keep in mind that it has to be the latest version of Roblox or else the program won't work",
                PrimaryButtonText = "Let me select the folder!",
                CloseButtonText = "I'm not ready",
                DefaultButton = ModernWpf.Controls.ContentDialogButton.Primary
            };

            ModernWpf.Controls.ContentDialogResult oidude = await FolderSelect.ShowAsync();

            if (oidude == ModernWpf.Controls.ContentDialogResult.Primary)
            {
                if (oldshelldlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    var SelectedFolder = oldshelldlg.FileName;

                    Properties.Settings.Default.robloxlocationFolder = SelectedFolder;

                    rblxDirectory.Text = Properties.LanguageResources.OptionsPageResources.Language.RobloxDirectoryCurrentRobloxDirectory + " " + Properties.Settings.Default.robloxlocationFolder;

                    Properties.Settings.Default.Save();

                    openrblxfolderlocation.Content = Properties.LanguageResources.OptionsPageResources.Language.RobloxDirectoryOpenFolderSelection;
                }
            }
        }
        private async void RobloxFolderSelectPromptES()
        {
            string RobloxStarterDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Roblox", "Versions");

            var oldshelldlg = new CommonOpenFileDialog();

            oldshelldlg.Title = "Roblox Asset Changer " + Properties.Settings.Default.AppVersion + " - Selección de carpeta de Roblox";
            oldshelldlg.IsFolderPicker = true;

            oldshelldlg.InitialDirectory = RobloxStarterDirectory;
            oldshelldlg.DefaultDirectory = RobloxStarterDirectory;

            oldshelldlg.AddToMostRecentlyUsedList = false;
            oldshelldlg.AllowNonFileSystemItems = false;
            oldshelldlg.EnsureFileExists = true;
            oldshelldlg.EnsurePathExists = true;
            oldshelldlg.EnsureReadOnly = false;
            oldshelldlg.EnsureValidNames = true;
            oldshelldlg.Multiselect = false;
            oldshelldlg.ShowPlacesList = true;

            ModernWpf.Controls.ContentDialog FolderSelect = new ModernWpf.Controls.ContentDialog
            {
                Title = "Selección de carpeta de Roblox",
                Content = "Una nueva ventana se abrira ahora para seleccionar la carpeta de Roblox, por favor ten en cuenta de que tiene que ser la última versión de Roblox o si no el programa no funcionara",
                PrimaryButtonText = "¡Dejame seleccionar la carpeta!",
                CloseButtonText = "No estoy preparado.",
                DefaultButton = ModernWpf.Controls.ContentDialogButton.Primary
            };

            ModernWpf.Controls.ContentDialogResult oidude = await FolderSelect.ShowAsync();

            if (oidude == ModernWpf.Controls.ContentDialogResult.Primary)
            {
                if (oldshelldlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    var SelectedFolder = oldshelldlg.FileName;

                    Properties.Settings.Default.robloxlocationFolder = SelectedFolder;

                    rblxDirectory.Text = Properties.LanguageResources.OptionsPageResources.Language.RobloxDirectoryCurrentRobloxDirectory + " " + Properties.Settings.Default.robloxlocationFolder;

                    Properties.Settings.Default.Save();

                    openrblxfolderlocation.Content = Properties.LanguageResources.OptionsPageResources.Language.RobloxDirectoryOpenFolderSelection;
                }
            }
        }
        #endregion
        #endregion

        #region Helper
        #region Conseguir el directorio de los ajustes
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
        #endregion
    }
}