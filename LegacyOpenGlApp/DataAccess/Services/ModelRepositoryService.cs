using System.Collections.Generic;
using System.Linq;
using LegacyOpenGlApp.DataAccess.Models;
using LegacyOpenGlApp.Primitives;
using Unity;

namespace LegacyOpenGlApp.DataAccess.Services
{
	public class ModelRepositoryService
	{
		[Dependency]
		public ConfigurationService ConfigurationService { get; set; }

		private IList<ToggleModel> _toggles;
		private IList<TransformationModel> _transformationModels;
		private IList<LightModel> _lightModels;
		private CameraModel _cameraModel;

		public IList<ToggleModel> Toggles
		{
			get => _toggles ?? (_toggles = ConfigurationService.OpenGlToggles.Select(toggle => new ToggleModel(toggle)).ToList());
			set => _toggles = value;
		}

		public IList<TransformationModel> Transformations
		{
			get => _transformationModels ?? (_transformationModels = ConfigurationService.Transformations
				       .Select(transformation => new TransformationModel(transformation)).ToList());
			set => _transformationModels = value;
		}

		public IList<LightModel> Lights
		{
			get => _lightModels ?? (_lightModels = ConfigurationService.Lights.Select(light => new LightModel(light)).ToList());
			set => _lightModels = value;
		}

		public CameraModel Camera
		{
			get => _cameraModel ?? (_cameraModel = ConfigurationService.Camera);
			set => _cameraModel = value;
		}

		public ProjectionTransformation ProjectionTransformation { get; set; } =
		    new ProjectionTransformation
		    {
			    Perspective = new Perspective
			    {
				    Aspect = 1.15571773f,
					Fovy = 60,
					Far = 100,
					Near = 0.1f
				},
			    Ortographic = new Ortographic
			    {
				    Left = -1.15571773f,
				    Right = 1.15571773f,
				    Top = 1,
				    Bottom = -1,
				    Far = 100,
				    Near = 0.1f
				}
		    };

	    public TextureEnvironmentModeModel TextureEnvMode = new TextureEnvironmentModeModel{ Mode = TextureEnvironmentMode.GL_MODULATE };
	}
}
