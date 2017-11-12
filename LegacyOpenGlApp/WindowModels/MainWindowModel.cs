using System.Collections.Generic;
using LegacyOpenGlApp.DataAccess.Models;
using LegacyOpenGlApp.DataAccess.Services;
using Unity;

namespace LegacyOpenGlApp.WindowModels
{
	public class MainWindowModel
	{
		[Dependency]
		public ModelRepositoryService Models { get; set; }

		public IList<ToggleModel> Toggles
		{
			get => Models.Toggles;
			set => Models.Toggles = value;
		}

		public IList<TransformationModel> Transformations
		{
			get => Models.Transformations;
			set => Models.Transformations = value;
		}

		public IList<LightModel> Lights
		{
			get => Models.Lights;
			set => Models.Lights = value;
		}

		public CameraModel Camera
		{
			get => Models.Camera;
			set => Models.Camera = value;
		}
	}
}
