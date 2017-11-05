using System;
using System.Windows;
using LegacyOpenGlApp.DataAccess.Models;
using LegacyOpenGlApp.WindowViewModels;

namespace LegacyOpenGlApp.WindowViews
{
	/// <summary>
	/// Interaction logic for AddLightWindow.xaml
	/// </summary>
	public partial class AddLightWindow : Window
	{
		public AddLightWindowViewModel ViewModel
		{
			get => DataContext as AddLightWindowViewModel;
			set => DataContext = value;
		}

		public event Action<LightModel> Add;

		public AddLightWindow()
		{
			ViewModel = new AddLightWindowViewModel
			{
				AmbientA = 1,
				DiffuseA = 1,
				SpecularA = 1,
				PositionZ = 1,
				SpotlightDirectionZ = -1,
				ConstantAttenuation = 1
			};

			InitializeComponent();
		}

		private void Button_AddLight_OnClick(object sender, RoutedEventArgs e)
		{
			Add?.Invoke(new LightModel
			{
				Ambient = new[] { ViewModel.AmbientR, ViewModel.AmbientG, ViewModel.AmbientB, ViewModel.AmbientA },
				Diffuse = new[] { ViewModel.DiffuseR, ViewModel.DiffuseG, ViewModel.DiffuseB, ViewModel.DiffuseA },
				Specular = new[] { ViewModel.SpecularR, ViewModel.SpecularG, ViewModel.SpecularB, ViewModel.SpecularA },
				Position = new[] { ViewModel.PositionX, ViewModel.PositionY, ViewModel.PositionZ, ViewModel.PositionW },
				SpotlightDirection = new[] { ViewModel.SpotlightDirectionX, ViewModel.SpotlightDirectionY, ViewModel.SpotlightDirectionZ },
				SpotlightExponent = ViewModel.SpotlightExponent,
				SpotlightCutoff = ViewModel.SpotlightCutoff,
				ConstantAttenuation = ViewModel.ConstantAttenuation,
				LinearAttenuation = ViewModel.LinearAttenuation,
				QuadraticAttenuation = ViewModel.QuadraticAttenuation
			});
		}
	}
}
