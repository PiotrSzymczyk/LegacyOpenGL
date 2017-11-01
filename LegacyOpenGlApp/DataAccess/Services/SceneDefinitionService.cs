using System.IO;
using Assimp;

namespace LegacyOpenGlApp.DataAccess.Services
{
    public class SceneDefinitionService
    {
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
