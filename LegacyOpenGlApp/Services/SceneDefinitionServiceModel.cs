using System.Linq;
using LegacyOpenGlApp.DataAccess.Models.SceneLoading;
using LegacyOpenGlApp.DataAccess.Services;
using Unity;

namespace LegacyOpenGlApp.Services
{
	public class SceneDefinitionServiceModel
	{
		[Dependency]
		public SceneLoadingService SceneLoadingService { get; set; }

		private Scene _scene;

		public string Path { get; set; } = ConfigurationService.DefaultScenePath;

		public Scene Scene => _scene ?? (_scene = SceneLoadingService.LoadScene(Path));

		public string SupportedFormats => SceneLoadingService.SupportedFormats.Aggregate((accum, curr) => string.Join("\t", accum, curr));

		public void ReloadScene() => _scene = null;
	}
}
