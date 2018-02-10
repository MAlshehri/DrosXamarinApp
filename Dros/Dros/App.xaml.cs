using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Dros.Data;
using Dros.Data.Models;
using Dros.Views;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;
using System.IO.Compression;
using static Dros.Helper;

namespace Dros
{
	public partial class App : Application
	{
        

        public App ()
		{
			InitializeComponent();

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
                UnzipFile(ZippedDbPath, DatabaseFilePath);
            }
            SQLitePCL.Batteries_V2.Init();
            MainPage = new MainPage();
        }

		protected override void OnStart ()
		{

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
