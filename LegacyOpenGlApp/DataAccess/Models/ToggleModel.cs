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
			this.Description = toggle.Description;
		}

		public uint StateVariable { get; set; }

		public string StateVariableName { get; set; }

		public string Description { get; set; }

		public bool IsActive { get; set; }

		public override string ToString() => $"{Description} ({StateVariableName})";
	}
}
