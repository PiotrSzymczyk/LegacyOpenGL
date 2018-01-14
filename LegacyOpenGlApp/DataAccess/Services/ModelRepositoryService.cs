using System.Collections.Generic;
using System.Linq;
using LegacyOpenGlApp.DataAccess.Models;
using LegacyOpenGlApp.Primitives;

namespace LegacyOpenGlApp.DataAccess.Services
{
    public class ModelRepositoryService
    {
	    public IList<ToggleModel> Toggles { get; set; } = ConfigurationService.OpenGlToggles.Select(toggle => new ToggleModel(toggle)).ToList();

	    public IList<TransformationModel> Transformations { get; set; } = ConfigurationService.Transformations.Select(transformation => new TransformationModel(transformation)).ToList();

	    public IList<LightModel> Lights { get; set; } = ConfigurationService.Lights.Select(light => new LightModel(light)).ToList();

	    public CameraModel Camera { get; set; } = ConfigurationService.Camera;

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
