using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernWpf.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using ModernWpf.Media.Animation;

namespace Roblox_Asset_Changer.Assets
{
    public class ChangerClass
    {
        public static string robpath = Roblox_Asset_Changer.Properties.Settings.Default.robloxlocationFolder;
        public static string acdir = System.IO.Path.Combine(robpath, "content", "textures", "ArrowCursor.png");
        public static string afcdir = System.IO.Path.Combine(robpath, "content", "textures", "ArrowFarCursor.png");
        public static string currentlyChanging = "";

        public static void MainChanger(string AssetToChange)
        {
            if (System.IO.Directory.Exists(robpath))
            {
                if (AssetToChange.Equals("Cursor"))
                {
                    PromptChangeCursor();
                }
                else if (AssetToChange.Equals("Face"))
                {

                }
            }
            else
            {
                WarnRobloxFolderNotDetected();
            }

        }

        #region Change Stuff 
        private static void ChangeCursor(string CursorType, string ChangeAssetPath)
        {
            if (CursorType.Equals("Arrow Cursor"))
            {
                jajadeubg("Currently changing " + CursorType + "\nMain dir " + acdir + "\nNew file dir " + ChangeAssetPath);
            }
            else if (CursorType.Equals("Arrow Far Cursor"))
            {
                jajadeubg("Currently changing " + CursorType + "\nMain dir " + afcdir + "\nNew file dir " + ChangeAssetPath);
            }
        }

        private static void ChangeFace(string chpath)
        {
            //In development / En desarrollo
        }
        #endregion

        #region Prompts 

        private static async void PromptChangeCursor()
        {
            var shelldlg = new CommonOpenFileDialog();
            shelldlg.Title = "Roblox Asset Changer - New " + currentlyChanging + " Selection";
            shelldlg.InitialDirectory = robpath;
            shelldlg.DefaultDirectory = robpath;
            shelldlg.AddToMostRecentlyUsedList = false;
            shelldlg.AllowNonFileSystemItems = false;
            shelldlg.EnsureFileExists = true;
            shelldlg.EnsurePathExists = true;
            shelldlg.EnsureReadOnly = false;
            shelldlg.EnsureValidNames = true;
            shelldlg.Multiselect = false;
            shelldlg.ShowPlacesList = true;
            shelldlg.RestoreDirectory = true;

            ModernWpf.Controls.ContentDialog ChCur = new ModernWpf.Controls.ContentDialog
            {
                Title = "Placeholder (Change Cursor Prompt Title)",
                Content = "Placeholder (Change Cursor Prompt Content)",
                PrimaryButtonText = "Placeholder (Change Cursor Prompt PrimBtn)",
                CloseButtonText = "Placeholder (Change Cursor Prompt ClsBtn)",
                DefaultButton = ContentDialogButton.Primary
            };
            ModernWpf.Controls.ContentDialogResult result = await ChCur.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if(shelldlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    ChangeCursor(currentlyChanging, shelldlg.FileName);
                }
            }
        }

        private static async void WarnRobloxFolderNotDetected()
        {
            ModernWpf.Controls.ContentDialog WarningRobloxFolder = new ModernWpf.Controls.ContentDialog
            {
                Title = Properties.LanguageResources.ShellResources.Language.behindCodeAssetsPromptTitle,
                Content = Properties.LanguageResources.ShellResources.Language.behindCodeAssetsPromptContent,
                PrimaryButtonText = Properties.LanguageResources.ShellResources.Language.behindCodeAssetsPromptPrimBtn,
                CloseButtonText = Properties.LanguageResources.ShellResources.Language.behindCodeAssetsPromptCloseBtn,
                DefaultButton = ContentDialogButton.Primary
            };
            ModernWpf.Controls.ContentDialogResult result = await WarningRobloxFolder.ShowAsync();

            if(result == ContentDialogResult.Primary)
            {
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                mw.AppMenuDrawer.IsLeftDrawerOpen = true;
                mw.appcontentFrame.Navigate(typeof(Pages.OptionsPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
                mw.CursorPagelvi.IsSelected = false;
                mw.OptionsPagelvi.IsSelected = true;
                mw.AppMenuDrawer.IsLeftDrawerOpen = false;
            }
        }

        private static async void jajadeubg(string content)
        {
            ModernWpf.Controls.ContentDialog debu = new ModernWpf.Controls.ContentDialog
            {
                Title = "debug",
                Content = content
            };
            ModernWpf.Controls.ContentDialogResult result = await debu.ShowAsync();
        }
        #endregion
    }
}
