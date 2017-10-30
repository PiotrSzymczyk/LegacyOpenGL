using System.Collections.Generic;
using LegacyOpenGlApp.DataAccess;
using Unity;

namespace LegacyOpenGlApp.Models
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
	}
}
