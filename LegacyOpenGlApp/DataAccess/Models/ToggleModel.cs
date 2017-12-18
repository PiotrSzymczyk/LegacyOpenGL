using LegacyOpenGlApp.Helpers;

namespace LegacyOpenGlApp.DataAccess.Models
{
	public class ToggleModel
	{
		public ToggleModel()
		{	
		}

		public ToggleModel(ToggleModel toggle)
		{
			this.IsActive = toggle.IsActive;
			this.StateVariable = toggle.StateVariable;
			this.StateVariableName = toggle.StateVariableName;
			this.DisplayName = toggle.DisplayName;
			this.Description = toggle.Description?.SplitToLines();
		}

		public uint StateVariable { get; set; }

		public string StateVariableName { get; set; }

		public string DisplayName { get; set; }

		public string Description { get; set; }

		public bool IsActive { get; set; }

		public override string ToString() => $"{DisplayName} ({StateVariableName})";
	}
}
