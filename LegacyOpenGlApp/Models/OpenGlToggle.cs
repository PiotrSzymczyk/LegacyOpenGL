namespace LegacyOpenGlApp.Models
{
	public class OpenGlToggle
	{
		public uint StateVariable { get; set; }

		public string StateVariableName { get; set; }

		public string Description { get; set; }

		public bool IsActive { get; set; }

		public override string ToString() => $"{Description} ({StateVariableName})";
	}
}
