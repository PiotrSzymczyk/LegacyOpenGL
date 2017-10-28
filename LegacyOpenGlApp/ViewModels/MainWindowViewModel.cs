using System.Collections.Generic;
using LegacyOpenGlApp.Models;
using LegacyOpenGlApp.Services;

namespace LegacyOpenGlApp.ViewModels
{
	public class MainWindowViewModel
	{
		private MainWindowModel _model;

		public MainWindowViewModel()
		{
			_model = new MainWindowModel { Toggles = Config.OpenGlToggles };
			Toggles = new List<OpenGlToggle>(_model.Toggles);
		}

		public IList<OpenGlToggle> Toggles { get; set; }
	}
}
