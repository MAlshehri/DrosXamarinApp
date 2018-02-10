using System;
using System.IO;
using System.IO.Compression;

namespace Dros
{
    public static class Helper
    {

        public static string DatabaseFilePath => GetLocalFilePath("dros.db");
        public static string ZippedDbPath => GetLocalFilePath("dros.zip");
        public static string LibraryPath 
        {
            get
            {
                #if __ANDROID__
                string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                #else
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                string libraryPath = Path.Combine(documentsPath, "..", "Library");
                #endif

                return libraryPath;
            }
        }

        public static void UnzipFile(string zipFilePath, string saveFilePath)
        {
            var zip = ZipStorer.Open(zipFilePath, FileAccess.Read); 
            var dir = zip.ReadCentralDir();

            foreach (ZipStorer.ZipFileEntry entry in dir)
            {
                if (Path.GetFileName(entry.FilenameInZip) == "dros.db")
                {
                    if (zip.ExtractFile(entry, saveFilePath))
                        File.Delete(zipFilePath);
                    break;
                }
            }
            zip.Close();
        }

        public static string GetLocalFilePath(string fileName)
        {
            var path = Path.Combine(LibraryPath, fileName);
            return path;
        }
    }
}
