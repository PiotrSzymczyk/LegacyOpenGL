using System.Collections.Generic;
using LegacyOpenGlApp.DataAccess.Models;
using LegacyOpenGlApp.Primitives;

namespace LegacyOpenGlApp.DataAccess.Services
{
    public class ModelRepositoryService
    {
	    public IList<ToggleModel> Toggles { get; set; } = Config.OpenGlToggles;

	    public IList<TransformationModel> Transformations { get; set; } = Config.Transformations;
	}
}
