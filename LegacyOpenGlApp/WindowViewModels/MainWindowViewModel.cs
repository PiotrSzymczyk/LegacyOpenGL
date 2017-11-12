using System.Collections.Generic;
using System.ComponentModel;
using LegacyOpenGlApp.DataAccess.Models;
using LegacyOpenGlApp.Primitives;
using LegacyOpenGlApp.Services;
using LegacyOpenGlApp.WindowModels;
using Unity;

namespace LegacyOpenGlApp.WindowViewModels
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		[Dependency]
		public SceneDefinitionServiceModel SceneDefinitionServiceModel { get; set; }

		public MainWindowViewModel(MainWindowModel model)
		{
			Toggles = new List<ToggleModel>(model.Toggles);
			Transformations = new ObservableListProxy<TransformationModel>(() => model.Transformations);
			Lights = new ObservableListProxy<LightModel>(() => model.Lights);
			Camera = model.Camera;
		}

		public IList<ToggleModel> Toggles { get; set; }

		public IList<TransformationModel> Transformations { get; set; }

		public IList<LightModel> Lights { get; set; }

		public CameraModel Camera { get; set; }

		public string SceneSupportedFormats => SceneDefinitionServiceModel.SupportedFormats;

		public string ScenePath
		{
			get => SceneDefinitionServiceModel.Path;
			set
			{
				SceneDefinitionServiceModel.Path = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ScenePath"));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
