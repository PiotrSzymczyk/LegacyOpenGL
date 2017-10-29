using System.Collections.Generic;
using LegacyOpenGlApp.Models;
using LegacyOpenGlApp.Services;

namespace LegacyOpenGlApp.DataAccess
{
    public class ModelRepositoryService
    {
	    public IList<ToggleModel> Toggles { get; set; } = Config.OpenGlToggles;
	}
}
