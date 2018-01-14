using LegacyOpenGlApp.DataAccess.Models.SceneLoading;
using LegacyOpenGlApp.DataAccess.Services;
using Unity;

namespace LegacyOpenGlApp.Services
{
	public class SceneServiceModel
	{
		[Dependency]
		public SceneLoadingService SceneLoadingService { get; set; }

		private Scene _scene;

		public string GeometryPath { get; set; } = ConfigurationService.DefaultGeometryPath;
		public string MaterialsPath { get; set; } = ConfigurationService.DefaultMaterialsPath;
		public string TexturePath { get; set; } = ConfigurationService.DefaultTexturePath;

		public Scene Scene => _scene ?? (_scene = new Scene
		{
			Geometry = SceneLoadingService.LoadGeometry(GeometryPath),
			Materials = SceneLoadingService.LoadMaterials(MaterialsPath),
			Texture = SceneLoadingService.LoadTexture(TexturePath)
		});

		public void ReloadScene() => _scene = null;
	}
}
