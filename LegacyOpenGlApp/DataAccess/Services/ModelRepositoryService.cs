using System.Collections.Generic;
using System.Linq;
using LegacyOpenGlApp.DataAccess.Models;
using LegacyOpenGlApp.Primitives;

namespace LegacyOpenGlApp.DataAccess.Services
{
    public class ModelRepositoryService
    {
	    public IList<ToggleModel> Toggles { get; set; } = Config.OpenGlToggles.Select(toggle => new ToggleModel(toggle)).ToList();

	    public IList<TransformationModel> Transformations { get; set; } = Config.Transformations.Select(transformation => new TransformationModel(transformation)).ToList();

	    public IList<LightModel> Lights { get; set; } = Config.Lights.Select(light => new LightModel(light)).ToList();

	    public CameraModel Camera { get; set; } = Config.Camera;
    }
}
