using System.Windows;
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
			IUnityContainer container = new UnityContainer();

			MainWindow mainWindow = container.Resolve<MainWindow>();
			mainWindow.Show();
		}
	}
}
