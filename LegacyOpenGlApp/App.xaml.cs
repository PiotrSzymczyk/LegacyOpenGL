using System;
using System.Globalization;
using System.IO;
using System.Windows;
using LegacyOpenGlApp.DataAccess.Services;
using LegacyOpenGlApp.Services;
using LegacyOpenGlApp.WindowModels;
using Unity;

namespace LegacyOpenGlApp
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private string JsonConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
		protected override void OnStartup(StartupEventArgs e)
		{
			CultureInfo customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
			customCulture.NumberFormat.NumberDecimalSeparator = ".";

			System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

			if (e.Args.Length == 1)
			{
				JsonConfigFilePath = e.Args[0];
			}

			IUnityContainer container = new UnityContainer();

			container.RegisterType<ConfigurationService>(new ContainerControlledLifetimeManager(), new InjectionProperty("JsonConfigFilePath", JsonConfigFilePath));
			container.RegisterType<ModelRepositoryService>(new ContainerControlledLifetimeManager());
			container.RegisterType<SceneServiceModel>(new ContainerControlledLifetimeManager());
			MainWindow mainWindow = container.Resolve<MainWindow>();
			mainWindow.Show();
		}
	}
}
