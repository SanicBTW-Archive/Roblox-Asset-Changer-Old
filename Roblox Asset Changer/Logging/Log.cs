using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roblox_Asset_Changer.Logging
{
    public class Log
    {
        #region Main
        private string Path = "";

        public Log(string Path)
        {
            this.Path = Path;
        }

        public void Add(string sLog)
        {
            CreateDirectory();
            string nombre = GetNameFile();
            string cadena = "";

            cadena += DateTime.Now + " - " + sLog + Environment.NewLine;

            System.IO.StreamWriter sw = new System.IO.StreamWriter(Path + "/" + nombre, false);
            sw.Write(cadena);
            sw.Close();
        }
        #endregion
        #region Helper

        private string GetNameFile()
        {
            string nombre = "";

            nombre = "RACLog_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + " " + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + ".log";

            return nombre;
        }

        private void CreateDirectory()
        {
            try
            {
                if (!System.IO.Directory.Exists(Path))
                    System.IO.Directory.CreateDirectory(Path);
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
