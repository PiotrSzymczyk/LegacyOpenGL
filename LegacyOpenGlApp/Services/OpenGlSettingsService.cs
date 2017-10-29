using System.Collections.Generic;
using System.Linq;
using LegacyOpenGlApp.DataAccess;
using Unity;

namespace LegacyOpenGlApp.Services
{
    public class OpenGlSettingsService
	{
		[Dependency]
		public ModelRepositoryService Models { get; set; }

		public IDictionary<uint, bool> Toggles =>
			Models.Toggles.ToDictionary(
				toggle => toggle.StateVariable,
				toggle => toggle.IsActive);
	}
}
