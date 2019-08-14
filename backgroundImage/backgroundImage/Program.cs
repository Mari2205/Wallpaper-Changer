
using System;
using System.Threading;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;



namespace backgroundImage
{
    class Program
    {

        [DllImport("User32", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uiAction, int uiParam,
        string pvParam, uint fWinIni);

        static void Main(string[] args)
        {
            string startUpFile = Environment.SpecialFolder.Startup + "wallpaper.exe";
            while (true)
            {
                if (!File.Exists(startUpFile))
                {
                    startUp();
                }

                changeWallpaper();
                Thread.Sleep(1800000);
            }
        }
        public static void changeWallpaper()
        {
            string downloadedFileName = Path.GetTempPath() + "img.png";
            string url = "https://picsum.photos/1920/1080";
            using (WebClient webC = new WebClient())
            {
                webC.DownloadFile(new Uri(url), downloadedFileName);

            }
            SystemParametersInfo(0x0014, 0, downloadedFileName , 0x0001);
            
        }
        public static void startUp()
        {
            string currentPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string startUpFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string path = Path.Combine(startUpFolder, "wallpaper.exe");
            if (!File.Exists(path))
            {
                try
                {
                    File.Copy(currentPath, path);
                }
                catch{}
            }  
        }
    }
}
