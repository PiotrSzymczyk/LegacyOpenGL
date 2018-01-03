using System.Diagnostics;
using System.IO;
using System.Windows;
using LegacyOpenGlApp.Services;
using LegacyOpenGlApp.WindowViewModels;
using SharpGL.SceneGraph;
using Unity;
using LegacyOpenGlApp.WindowViews;

namespace LegacyOpenGlApp.WindowModels
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

		public MainWindow()
		{
			InitializeComponent();
			OpenGlControl.DrawFPS = true;
		}

		private void OpenGLControl_OnOpenGLDraw(object sender, OpenGLEventArgs args)
		{
			OpenGlService.Draw(args.OpenGL);
		}

		private void OpenGLControl_OnResized(object sender, OpenGLEventArgs args)
		{
			OpenGlService.Resize(args.OpenGL);
		}

		private void Button_OnClick_RemoveTransform(object sender, RoutedEventArgs e)
		{
			if (ViewModel.Transformations.Count > 0)
			{
				var selectedIndex = TransformationsList.SelectedIndex;
				ViewModel.Transformations.RemoveAt(selectedIndex);
				TransformationsList.SelectedIndex = selectedIndex < TransformationsList.Items.Count ? selectedIndex : --selectedIndex;
			}
		}

		private void Button_OnClick_AddTransform(object sender, RoutedEventArgs e)
		{
			var popup = new AddTransformationWindow();
			popup.Add += value => ViewModel.Transformations.Add(value);
			popup.ShowDialog();
		}

		private void Button_GenerateCode_OnClick(object sender, RoutedEventArgs e)
		{
			File.WriteAllText("generatedCode.cpp", CodeGenerationService.GenerateCode(OpenGlService.OpenGlSceneDefinitionService, OpenGlService.SettingsService));
			Process.Start(Path.GetFullPath("generatedCode.cpp"));
		}

		private void Button_AddLight_OnClick(object sender, RoutedEventArgs e)
		{
			var popup = new AddLightWindow();
			popup.Add += value => ViewModel.Lights.Add(value);
			popup.ShowDialog();
		}

		private void Button_RemoveLight_OnClick(object sender, RoutedEventArgs e)
		{
			if (ViewModel.Lights.Count > 0)
			{
				var selectedIndex = LightsList.SelectedIndex;
				ViewModel.Lights.RemoveAt(selectedIndex);
				LightsList.SelectedIndex = selectedIndex < LightsList.Items.Count ? selectedIndex : --selectedIndex;
			}
		}

		private void Button_SelectScenePath_OnClick(object sender, RoutedEventArgs e)
		{
			var path = SelectPath();
			if (path != null)
			{
				ViewModel.ScenePath = path;
			}
		}

		private void Button_SelectMaterialsPath_OnClick(object sender, RoutedEventArgs e)
		{
			var path = SelectPath();
			if (path != null)
			{
				ViewModel.MaterialsPath = path;
			}
		}

		private void Button_SelectTexturePath_OnClick(object sender, RoutedEventArgs e)
		{
			var path = SelectPath();
			if (path != null)
			{
				ViewModel.TexturePath = path;
			}
		}

		private string SelectPath()
		{
			using (var dialog = new System.Windows.Forms.OpenFileDialog())
			{
				var result = dialog.ShowDialog();
				if (result == System.Windows.Forms.DialogResult.OK)
				{
					return dialog.FileName;
				}
				return null;
			}
		}

		private void Button_LoadScene_OnClick(object sender, RoutedEventArgs e)
		{
			ViewModel.SceneDefinitionServiceModel.ReloadScene();
		}

		private void Button_LoadMaterials_OnClick(object sender, RoutedEventArgs e)
		{
			ViewModel.SceneDefinitionServiceModel.ReloadScene();
		}

		private void Button_LoadTexture_OnClick(object sender, RoutedEventArgs e)
		{
			ViewModel.SceneDefinitionServiceModel.ReloadScene();
		}

		private void Button_SetProjection_OnClick(object sender, RoutedEventArgs e)
		{
			OpenGlService.Resize(OpenGlControl.OpenGL);
		}
	}
}
