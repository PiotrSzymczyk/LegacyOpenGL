using System.Globalization;
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
		protected override void OnStartup(StartupEventArgs e)
		{
			CultureInfo customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
			customCulture.NumberFormat.NumberDecimalSeparator = ".";

			System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

			IUnityContainer container = new UnityContainer();

			container.RegisterType<ConfigurationService>(new ContainerControlledLifetimeManager(), new InjectionProperty("JsonConfigFilePath", "C:\\Code\\LegacyOpenGL\\LegacyOpenGlApp\\config.json"));
			container.RegisterType<ModelRepositoryService>(new ContainerControlledLifetimeManager());
			container.RegisterType<SceneServiceModel>(new ContainerControlledLifetimeManager());
			MainWindow mainWindow = container.Resolve<MainWindow>();
			mainWindow.Show();
		}
	}
}
