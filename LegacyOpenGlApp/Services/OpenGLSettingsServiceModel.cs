using System.Collections.Generic;
using System.Linq;
using LegacyOpenGlApp.DataAccess.Models;
using LegacyOpenGlApp.DataAccess.Services;
using Unity;

namespace LegacyOpenGlApp.Services
{
    public class OpenGLSettingsServiceModel
	{
		[Dependency]
		public ModelRepositoryService Models { get; set; }

		public IDictionary<uint, bool> Toggles =>
			Models.Toggles.ToDictionary(
				toggle => toggle.StateVariable,
				toggle => toggle.IsActive);

		public IList<TransformationModel> Transformations => Models.Transformations;

		public IList<LightModel> Lights => Models.Lights;

		public CameraModel Camera => Models.Camera;

		public ProjectionTransformation ProjectionTransformation => Models.ProjectionTransformation;

		public TextureEnvironmentModeModel TextureEnvMode => Models.TextureEnvMode;
	}
}
