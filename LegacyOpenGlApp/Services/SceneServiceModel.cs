using System.Linq;
using LegacyOpenGlApp.DataAccess.Models.SceneLoading;
using LegacyOpenGlApp.DataAccess.Services;
using Unity;

namespace LegacyOpenGlApp.Services
{
	public class SceneServiceModel
	{
		[Dependency]
		public ConfigurationService ConfigurationService { get; set; }

		[Dependency]
		public SceneLoadingService SceneLoadingService { get; set; }

		private Scene _scene;
		private string _geometryPath;
		private string _materialsPath;
		private string _texturesPath;

		public string GeometryPath
		{
			get => _geometryPath ?? (_geometryPath = ConfigurationService.DefaultGeometryPath);
			set => _geometryPath = value;
		}

		public string MaterialsPath
		{
			get => _materialsPath ?? (_materialsPath = ConfigurationService.DefaultMaterialsPath);
			set => _materialsPath = value;
		}

		public string TexturePath
		{
			get => _texturesPath ?? (_texturesPath = ConfigurationService.DefaultTexturePath);
			set => _texturesPath = value;
		}

		public Scene Scene => _scene ?? (_scene = new Scene
		{
			Geometry = SceneLoadingService.LoadGeometry(GeometryPath),
			Materials = SceneLoadingService.LoadMaterials(MaterialsPath),
			Texture = SceneLoadingService.LoadTexture(TexturePath)
		});

		public void ReloadScene() => _scene = null;
	}
}
