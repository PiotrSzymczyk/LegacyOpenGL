﻿using System.Windows;
using LegacyOpenGlApp.Services;
using LegacyOpenGlApp.ViewModels;
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
			ViewModel.Transformations.RemoveAt(TransformationsList.SelectedIndex);
		}
	}
}
