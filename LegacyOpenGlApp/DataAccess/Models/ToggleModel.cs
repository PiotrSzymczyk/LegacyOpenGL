using LegacyOpenGlApp.Helpers;

namespace LegacyOpenGlApp.DataAccess.Models
{
	public class ToggleModel
	{
		private string _description;

		public ToggleModel()
		{	
		}

		public ToggleModel(ToggleModel toggle)
		{
			this.IsActive = toggle.IsActive;
			this.StateVariable = toggle.StateVariable;
			this.StateVariableName = toggle.StateVariableName;
			this.DisplayName = toggle.DisplayName;
			this.Description = toggle._description;
		}

		public uint StateVariable { get; set; }

		public string StateVariableName { get; set; }

		public string DisplayName { get; set; }

		public string Description
		{
			get => _description.SplitToLines();
			set => _description = value;
		}

		public bool IsActive { get; set; }

		public override string ToString() => $"{DisplayName} ({StateVariableName})";
	}
}
