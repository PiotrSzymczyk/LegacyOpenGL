using System;
using System.Windows;
using LegacyOpenGlApp.DataAccess.Models;
using LegacyOpenGlApp.Primitives;
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
			var popup = new AddTransformationWindow();
			popup.Add += value => ViewModel.Transformations.Add(value);
			popup.ShowDialog();
		}
	}
}
