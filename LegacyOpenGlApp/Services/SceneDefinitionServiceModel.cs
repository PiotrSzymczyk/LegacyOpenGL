using Assimp;
using LegacyOpenGlApp.DataAccess.Services;
using LegacyOpenGlApp.Primitives;
using Unity;

namespace LegacyOpenGlApp.Services
{
	public class SceneDefinitionServiceModel
	{
		[Dependency]
		public SceneDefinitionService SceneDefinitionService { get; set; }

		private Scene _scene;

		public string Path { get; set; } = Config.DefaultScenePath;

		public Scene Scene => _scene ?? (_scene = SceneDefinitionService.LoadScene(Path));
	}
}
