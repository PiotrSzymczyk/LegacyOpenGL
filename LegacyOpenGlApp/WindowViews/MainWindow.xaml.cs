using System;
using System.Windows;
using LegacyOpenGlApp.DataAccess.Models;
using LegacyOpenGlApp.Primitives;
using LegacyOpenGlApp.Services;
using LegacyOpenGlApp.ViewModels;
using SharpGL.SceneGraph;
using Unity;

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

		public Random Random = new Random();

		public MainWindow()
		{
			InitializeComponent();
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
			var selectedIndex = TransformationsList.SelectedIndex;
			ViewModel.Transformations.RemoveAt(selectedIndex);
			TransformationsList.SelectedIndex = selectedIndex < TransformationsList.Items.Count ? selectedIndex : --selectedIndex;
		}

		private void Button_OnClick_AddTransform(object sender, RoutedEventArgs e)
		{
			if (Random.NextDouble() < 0.5)
			{
				ViewModel.Transformations.Add(new TransformationModel
				{
					Transform = Transform.Rotate,
					X = (float) Random.Next(720) - 360,
					Y = (float) Random.Next(720) - 360,
					Z = (float) Random.Next(720) - 360
				});
			}
			else
			{
				ViewModel.Transformations.Add(new TransformationModel
				{
					Transform = Transform.Translate,
					X = (float)Random.NextDouble() * 2 - 1,
					Y = (float)Random.NextDouble() * 2 - 1,
					Z = (float)Random.NextDouble() * 2 - 1
				});
			}
		}
	}
}
