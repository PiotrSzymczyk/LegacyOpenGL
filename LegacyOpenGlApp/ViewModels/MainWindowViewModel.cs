using System.Collections.Generic;
using LegacyOpenGlApp.Models;

namespace LegacyOpenGlApp.ViewModels
{
	public class MainWindowViewModel
	{
		public MainWindowViewModel(MainWindowModel Model)
		{
			Toggles = new List<ToggleModel>(Model.Toggles);
		}

		public IList<ToggleModel> Toggles { get; set; }

		public IList<TransformationModel> Transformations { get; set; }
	}
}
