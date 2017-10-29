using System.Collections.Generic;
using System.Collections.ObjectModel;
using LegacyOpenGlApp.Models;
using Unity;

namespace LegacyOpenGlApp.ViewModels
{
	public class MainWindowViewModel
	{
		[Dependency]
		public MainWindowModel Model { get; set; }

		private IList<ToggleModel> _toggles;
		private IList<TransformationModel> _transformations;

		public IList<ToggleModel> Toggles => _toggles ?? (_toggles = new List<ToggleModel>(Model.Toggles));

		public IList<TransformationModel> Transformations
			=> _transformations ?? (_transformations = new ObservableCollection<TransformationModel>(Model.Transformations));
	}
}
