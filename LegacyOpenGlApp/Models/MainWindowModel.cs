using System.Collections.Generic;
using LegacyOpenGlApp.DataAccess;
using Unity;

namespace LegacyOpenGlApp.Models
{
	public class MainWindowModel
	{
		[Dependency]
		public ModelRepositoryService Models { get; set; }

		public IList<ToggleModel> Toggles => Models.Toggles;
	}
}
