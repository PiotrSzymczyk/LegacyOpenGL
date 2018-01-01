namespace LegacyOpenGlApp.DataAccess.Models.SceneLoading
{
	public class Scene
	{
		public Geometry Geometry { get; set; }

		public Material[] Materials { get; set; }

		public Texture Texture { get; set; }
	}
}