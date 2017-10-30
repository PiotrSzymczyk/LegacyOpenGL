using System.Collections.Generic;
using System.Collections.ObjectModel;
using LegacyOpenGlApp.Models;
using LegacyOpenGlApp.Primitives;

namespace LegacyOpenGlApp.ViewModels
{
	public class MainWindowViewModel
	{
		public MainWindowViewModel(MainWindowModel model)
		{
			Toggles = new List<ToggleModel>(model.Toggles);
			Transformations = new ObservableListProxy<TransformationModel>(() => model.Transformations);
		}

		public IList<ToggleModel> Toggles { get; set; }

		public IList<TransformationModel> Transformations { get; set; }
	}
}
