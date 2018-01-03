using System.Collections.Generic;
using System.ComponentModel;
using LegacyOpenGlApp.DataAccess.Models;
using LegacyOpenGlApp.DataAccess.Models.SceneLoading;
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
			ProjectionTransformation = model.ProjectionTransformation;
		}

		public IList<ToggleModel> Toggles { get; set; }

		public IList<TransformationModel> Transformations { get; set; }

		public IList<LightModel> Lights { get; set; }

		public CameraModel Camera { get; set; }

		public ProjectionTransformation ProjectionTransformation { get; set; }

		public Scene Scene => SceneDefinitionServiceModel.Scene;

		public int VertexCount => Scene.Geometry.Vertices.Count;

		public int NormalCount => Scene.Geometry.HasNormals ? Scene.Geometry.Normals.Count : 0;

		public int FaceCount => Scene.Geometry.Faces.Count;

		public string ScenePath
		{
			get => SceneDefinitionServiceModel.GeometryPath;
			set
			{
				SceneDefinitionServiceModel.GeometryPath = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScenePath)));
			}
		}

		public int SelectedMaterialIndex
		{
			get => Scene.SelectedMaterial;
			set
			{
				Scene.SelectedMaterial = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMaterialIndex)));
			}
		}

		public string MaterialsPath
		{
			get => SceneDefinitionServiceModel.MaterialsPath;
			set
			{
				SceneDefinitionServiceModel.MaterialsPath = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MaterialsPath)));
			}
		}

		public string TexturePath
		{
			get => SceneDefinitionServiceModel.TexturePath;
			set
			{
				SceneDefinitionServiceModel.TexturePath = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TexturePath)));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
