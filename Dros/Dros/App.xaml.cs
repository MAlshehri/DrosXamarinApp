using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Dros.Data;
using Dros.Views;
using Xamarin.Forms;

namespace Dros
{
	public partial class App : Application
	{
        public static string DatabaseFilePath => Helpers.GetLocalFilePath("dros.db");
        public static string ZippedDbPath => Helpers.GetLocalFilePath("dros.zip");

        public App ()
		{
			InitializeComponent();

            

            MainPage = new MainPage();
        }

		protected override void OnStart ()
		{
            MainPage = new MainPage();

            if (!File.Exists(DatabaseFilePath))
            {
                using (var source = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("dros.zip"))
                {
                    if (!File.Exists(ZippedDbPath))
                    {
                        using (var destination = File.Create(ZippedDbPath))
                        {
                            source.CopyTo(destination);
                        }
                    }
                }
                Helpers.UnzipFile(ZippedDbPath, DatabaseFilePath);
            }
            if (File.Exists(DatabaseFilePath))
            {
                SQLitePCL.Batteries_V2.Init();
                using (var context = new DrosDbContext())
                {
                    Debug.WriteLine(context.Authors.FirstOrDefault().Name);
                }
            }
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
