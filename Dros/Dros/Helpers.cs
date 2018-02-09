using System;
using System.IO;
using System.IO.Compression;

namespace Dros
{
    public static class Helpers
    {
        public static void UnzipFile(string zipFilePath, string saveFilePath)
        {
            var zipFile = new FileInfo(zipFilePath);

            try
            {
                using (FileStream zipStream = zipFile.OpenRead())
                {
                    using (FileStream extracted = File.Create(saveFilePath))
                    {
                        using (GZipStream extraction = new GZipStream(zipStream, CompressionMode.Decompress))
                        {
                            extraction.CopyTo(extracted);
                            Console.WriteLine("Decompressed: {0}", zipFile.Name);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }

        }

        public static string GetLocalFilePath(string fileName)
        {
            #if __ANDROID__
            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            #else

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library");
            #endif

            var path = Path.Combine(libraryPath, fileName);
            return path;
        }
    }
}
