using Assimp;
using LegacyOpenGlApp.DataAccess.Services;
using Unity;

namespace LegacyOpenGlApp.Services
{
    public class SceneDefinitionServiceModel
    {
		[Dependency]
		public SceneDefinitionService SceneDefinitionService { get; set; }

	    private Scene _scene;
	    private string _path = "C:\\Code\\LegacyOpenGL\\LegacyOpenGlApp\\scene.off";

	    public Scene Scene => _scene ?? (_scene = SceneDefinitionService.LoadScene(_path));
	}
}
