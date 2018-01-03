namespace LegacyOpenGlApp.DataAccess.Models
{
	public class ProjectionTransformation
	{
		public Perspective Perspective { get; set; }

		public Ortographic Ortographic { get; set; }

		public int SelectedIndex { get; set; }
	}
}