using MaterialDesignThemes.Wpf;
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

        #region Comportamiento de la página al cargar / Page loading behaviour
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            #region Cargar los controles / Load controls
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
            if (System.IO.Directory.Exists(Properties.Settings.Default.robloxlocationFolder))
            {
                openrblxfolderlocation.Content = Properties.LanguageResources.OptionsPageResources.Language.RobloxDirectoryOpenFolderSelection;
            }
            else
            {
                openrblxfolderlocation.Content = Properties.LanguageResources.OptionsPageResources.Language.RobloxDirectorySelectFolder;
            }
            #endregion
            #region Modificar el ajuste del directorio si no esta configurado aun / Modifiy directory settings if not defined
            if (Properties.Settings.Default.robloxlocationFolder == "nul")
            {
                Properties.Settings.Default.robloxlocationFolder = "aún no se ha definido el directorio.";
                if (Properties.Settings.Default.languageCode == "es-ES" || Properties.Settings.Default.robloxlocationFolder == "directory isn't defined.")
                {
                    Properties.Settings.Default.robloxlocationFolder = "aún no se ha definido el directorio.";
                }
                else if (Properties.Settings.Default.languageCode == "en-US" || Properties.Settings.Default.robloxlocationFolder == "aún no se ha definido el directorio.")
                {
                    Properties.Settings.Default.robloxlocationFolder = "directory isn't defined.";
                }
            }
            Properties.Settings.Default.Save();
            #endregion
            #region AccentCB
            var paletteHelper = new MaterialDesignThemes.Wpf.PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            switch (Properties.Settings.Default.CurrentAccent)
            {
                case "Default":
                    ChangePAccent.SelectedIndex = 0;
                    theme.SetPrimaryColor(Colors.SlateGray);
                    paletteHelper.SetTheme(theme);
                    break;
                case "Red":
                    ChangePAccent.SelectedIndex = 1;
                    theme.SetPrimaryColor(Colors.Red);
                    paletteHelper.SetTheme(theme);
                    break;
                case "Blue":
                    ChangePAccent.SelectedIndex = 2;
                    theme.SetPrimaryColor(Colors.Blue);
                    paletteHelper.SetTheme(theme);
                    break;
                case "Yellow":
                    ChangePAccent.SelectedIndex = 3;
                    theme.SetPrimaryColor(Colors.Yellow);
                    paletteHelper.SetTheme(theme);
                    break;
                case "Green":
                    ChangePAccent.SelectedIndex = 4;
                    theme.SetPrimaryColor(Colors.Green);
                    paletteHelper.SetTheme(theme);
                    break;
            }
            #endregion
            #region Theme swetich lol
            if (Properties.Settings.Default.CurrentTheme.Contains("Dark"))
            {
                ModernWpf.ThemeManager.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Dark;
                theme.SetBaseTheme(Theme.Dark);
                paletteHelper.SetTheme(theme);
                ThemeChangerTS.IsOn = true;
            }
            else if (Properties.Settings.Default.CurrentTheme.Contains("Light"))
            {
                ModernWpf.ThemeManager.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Light;
                theme.SetBaseTheme(Theme.Light);
                paletteHelper.SetTheme(theme);
                ThemeChangerTS.IsOn = false;
            }
            #endregion
            #endregion
        }
        #endregion

        #region Comportamiento de los controles / Control behaviour
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
            if (System.IO.Directory.Exists(Properties.Settings.Default.robloxlocationFolder))
            {
                System.Diagnostics.Process.Start("explorer.exe", Properties.Settings.Default.robloxlocationFolder);
                openrblxfolderlocation.Content = Properties.LanguageResources.OptionsPageResources.Language.RobloxDirectoryOpenFolderSelection;
            }
            else
            {
                RobloxFolderSelectPrompt();
            }
        }
        private void resetrlbxfolderlocation_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.robloxlocationFolder = "nul";
            Properties.Settings.Default.Save();
            rblxDirectory.Text = Properties.LanguageResources.OptionsPageResources.Language.RobloxDirectoryCurrentRobloxDirectory + " " + Properties.Settings.Default.robloxlocationFolder;
        }

        #endregion
        #region Página de interfaz de usuario
        private void ChangePAccent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var paletteHelper = new MaterialDesignThemes.Wpf.PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            switch (ChangePAccent.SelectedIndex)
            {
                case 0:
                    theme.SetPrimaryColor(Colors.SlateGray);
                    paletteHelper.SetTheme(theme);
                    Properties.Settings.Default.CurrentAccent = "Default";
                    break;
                case 1:
                    theme.SetPrimaryColor(Colors.Red);
                    paletteHelper.SetTheme(theme);
                    Properties.Settings.Default.CurrentAccent = "Red";
                    break;
                case 2:
                    theme.SetPrimaryColor(Colors.Blue);
                    paletteHelper.SetTheme(theme);
                    Properties.Settings.Default.CurrentAccent = "Blue";
                    break;
                case 3:
                    theme.SetPrimaryColor(Colors.Yellow);
                    paletteHelper.SetTheme(theme);
                    Properties.Settings.Default.CurrentAccent = "Yellow";
                    break;
                case 4:
                    theme.SetPrimaryColor(Colors.Green);
                    paletteHelper.SetTheme(theme);
                    Properties.Settings.Default.CurrentAccent = "Green";
                    break;
            }
            Properties.Settings.Default.Save();
        }

        private void ThemeChangerTS_Toggled(object sender, RoutedEventArgs e)
        {
            var paletteHelper = new MaterialDesignThemes.Wpf.PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            ToggleSwitch darkswitch = sender as ToggleSwitch;
            if (darkswitch != null)
            {
                if(darkswitch.IsOn == true)
                {
                    ModernWpf.ThemeManager.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Dark;
                    theme.SetBaseTheme(Theme.Dark);
                    paletteHelper.SetTheme(theme);
                    Properties.Settings.Default.CurrentTheme = "Dark";
                }
                else
                {
                    ModernWpf.ThemeManager.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Light;
                    theme.SetBaseTheme(Theme.Light);
                    paletteHelper.SetTheme(theme);
                    Properties.Settings.Default.CurrentTheme = "Light";
                }
            }
            Properties.Settings.Default.Save();
        }


        /* disabled cuz it doesnt really work with ui, secondary accent color its just not useful in app lol
        private void ChangeSAccent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var paletteHelper = new MaterialDesignThemes.Wpf.PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            if (ChangeSAccent.SelectedIndex == 0)
            {
                theme.SetSecondaryColor(Colors.SlateGray);
                paletteHelper.SetTheme(theme);
                Properties.Settings.Default.CurrentSecondaryAccent = "Default";
            }
            else if (ChangeSAccent.SelectedIndex == 1)
            {
                theme.SetSecondaryColor(Colors.Red);
                paletteHelper.SetTheme(theme);
                Properties.Settings.Default.CurrentSecondaryAccent = "Red";
            }
            else if (ChangeSAccent.SelectedIndex == 2)
            {
                theme.SetSecondaryColor(Colors.Blue);
                paletteHelper.SetTheme(theme);
                Properties.Settings.Default.CurrentSecondaryAccent = "Blue";
            }
            else if (ChangeSAccent.SelectedIndex == 3)
            {
                theme.SetSecondaryColor(Colors.Yellow);
                paletteHelper.SetTheme(theme);
                Properties.Settings.Default.CurrentSecondaryAccent = "Yellow";
            }
            else if (ChangeSAccent.SelectedIndex == 4)
            {
                theme.SetSecondaryColor(Colors.Green);
                paletteHelper.SetTheme(theme);
                Properties.Settings.Default.CurrentSecondaryAccent = "Green";
            }
        }
        */
        #endregion
        #endregion

        #region Asyncs para los prompts / Prompts async something idk
        #region Prompt para reiniciar la aplicacion / Prompt for restarting the application
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
        #region Prompt para elegir la carpeta de roblox / Prompt to choose roblox folder
        private async void RobloxFolderSelectPrompt()
        {
            string RobloxStarterDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Roblox", "Versions");

            var oldshelldlg = new CommonOpenFileDialog();

            oldshelldlg.Title = "Roblox Asset Changer - Roblox folder selection";
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
                Title = Properties.LanguageResources.OptionsPageResources.Language.behindCodePromptFolderSelecTitle,
                Content = Properties.LanguageResources.OptionsPageResources.Language.behindCodePromptFolderSelecContent,
                PrimaryButtonText = Properties.LanguageResources.OptionsPageResources.Language.behindCodePromptFolderSelecPrimBtn,
                CloseButtonText = Properties.LanguageResources.OptionsPageResources.Language.behindCodePromptFolderSelecCloseBtn,
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

        #region Ayudante nosexd / Helper
        #region Conseguir el directorio de los ajustes / Get settings directory
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