using System.IO;
using Assimp;

namespace LegacyOpenGlApp.Services
{
    public class OpenGlSceneDefinitionService
    {
	    private Scene _scene;
	    private string _path = "C:\\Code\\LegacyOpenGL\\LegacyOpenGlApp\\scene.off";


		public Scene Scene => _scene ?? (_scene = LoadScene(_path));

		public Scene LoadScene(string path)
	    {
		    path = Path.GetFullPath(path);
			var context = new AssimpContext();

			if (context.IsImportFormatSupported(GetFileExtension(path)))
		    {
			    return context.ImportFile(path);
		    }

		    return null;
	    }

	    private static string GetFileExtension(string path)
	    {
		    return path.Substring(path.LastIndexOf('.'));
	    }
    }
}
