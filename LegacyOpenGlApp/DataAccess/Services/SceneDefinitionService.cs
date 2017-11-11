using System.IO;
using Assimp;
using ObjectBuilder2;
using Unity;

namespace LegacyOpenGlApp.DataAccess.Services
{
    public class SceneDefinitionService
    {
		[Dependency]
	    public AssimpContext Context { get; set; }

	    public string SupportedFormats => "\t" + Context.GetSupportedImportFormats().JoinStrings("\t");

		public Scene LoadScene(string path)
	    {
		    path = Path.GetFullPath(path);

			if (Context.IsImportFormatSupported(GetFileExtension(path)))
		    {
			    return Context.ImportFile(path);
		    }

		    return null;
	    }

	    private static string GetFileExtension(string path)
	    {
		    return path.Substring(path.LastIndexOf('.'));
	    }
    }
}
