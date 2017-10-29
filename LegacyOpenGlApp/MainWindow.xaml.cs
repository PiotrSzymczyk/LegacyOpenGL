using System.Linq;
using System.Windows;
using LegacyOpenGlApp.Models;
using LegacyOpenGlApp.Services;
using LegacyOpenGlApp.ViewModels;
using SharpGL;
using SharpGL.SceneGraph;
using Unity;

namespace LegacyOpenGlApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		[Dependency]
		public MainWindowViewModel ViewModel
		{
			get => DataContext as MainWindowViewModel;
			set => DataContext = value;
		}

		[Dependency]
		public OpenGlService OpenGlService { get; set; }

		[Dependency]
		public SceneDefinitionService SceneDefinitionService { get; set; }

		public MainWindow()
		{
			InitializeComponent();
		}

		private void OpenGLControl_OnOpenGLDraw(object sender, OpenGLEventArgs args)
		{
			OpenGlService.Draw(args.OpenGL, new OpenGlSettingsModel
			{
				Toggles = ViewModel.Toggles.ToDictionary(tg => tg.StateVariable, tg => tg.IsActive)
			},
			SceneDefinitionService.Scene);
		}

		private void OpenGLControl_OnResized(object sender, OpenGLEventArgs args)
		{
			OpenGlService.Resize(args.OpenGL);
		}
	}
}
