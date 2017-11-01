using System;
using System.ComponentModel;
using System.Windows;
using LegacyOpenGlApp.DataAccess.Models;
using LegacyOpenGlApp.Primitives;
using LegacyOpenGlApp.WindowViewModels;

namespace LegacyOpenGlApp.WindowViews
{
	/// <summary>
	/// Interaction logic for AddTransformationWindow.xaml
	/// </summary>
	public partial class AddTransformationWindow : Window
	{
		public AddTransformationWindowViewModel ViewModel
		{
			get => DataContext as AddTransformationWindowViewModel;
			set => DataContext = value;
		}

		public event Action<TransformationModel> Add;

		public AddTransformationWindow()
		{
			ViewModel = new AddTransformationWindowViewModel
			{
				ScaleX = 100,
				ScaleY = 100,
				ScaleZ = 100
			};

			InitializeComponent();
		}

		private void Button_AddTransformation_OnClick(object sender, RoutedEventArgs e)
		{
			switch (ViewModel.Transform)
			{
				case 0:
					Add?.Invoke(new TransformationModel
					{
						Transform = Transform.Translate,
						X = ViewModel.TranslateX,
						Y = ViewModel.TranslateY,
						Z = ViewModel.TranslateZ
					});
					break;
				case 1:
					Add?.Invoke(new TransformationModel
					{
						Transform = Transform.Rotate,
						X = ViewModel.RotateX,
						Y = ViewModel.RotateY,
						Z = ViewModel.RotateZ
					});
					break;
				case 2:
					Add?.Invoke(new TransformationModel
					{
						Transform = Transform.Scale,
						X = ViewModel.ScaleX / 100,
						Y = ViewModel.ScaleY / 100,
						Z = ViewModel.ScaleZ / 100
					});
					break;
				default:
					throw new InvalidEnumArgumentException();
			}
		}
	}
}
