using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using LegacyOpenGlApp.DataAccess.Models.SceneLoading;

namespace LegacyOpenGlApp.DataAccess.Services
{
    public class SceneLoadingService
    {
		private const string FloatValuePattern = @"-?\d+\.?\d*";
		private const string FaceIndicePattern = @"(\d+)\/?(\d+)?\/?(\d+)?";

		public string[] SupportedFormats => new[] { ".obj" };

		public Scene LoadScene(string path)
	    {
		    path = Path.GetFullPath(path);

		    if (SupportedFormats.Contains(GetFileExtension(path)))
		    {
			    var lines = File.ReadAllLines(path);

				return new Scene
				{
					Vertices = lines.Where(IsVertexDefinition).Select(CreateVertex).ToList(),
					Normals = lines.Where(IsNormalDefinition).Select(CreateNormal).ToList(),
					TextureCoordinates = lines.Where(IsTextureCoordinateDefinition).Select(CreateTextureCoordinate).ToList(),
					Faces = lines.Where(IsFaceDefinition).Select(CreateFace).ToList()
				};
			}

		    return null;
		}

	    private static string GetFileExtension(string path)
	    {
		    return path.Substring(path.LastIndexOf('.'));
	    }

		private static bool IsVertexDefinition(string line)
	    {
		    return Regex.IsMatch(line, @"^v\s");
	    }

		private static bool IsNormalDefinition(string line)
	    {
		    return Regex.IsMatch(line, @"^vn\s");
		}

	    private static bool IsTextureCoordinateDefinition(string line)
	    {
		    return Regex.IsMatch(line, @"^vt\s");
		}

	    private static bool IsFaceDefinition(string line)
	    {
		    return Regex.IsMatch(line, @"^f\s");
	    }

		private static Vertex CreateVertex(string definition)
	    {
		    var values = Regex.Matches(definition, FloatValuePattern);
		    return new Vertex
		    {
			    X = float.Parse(values[0].Value),
			    Y = float.Parse(values[1].Value),
			    Z = float.Parse(values[2].Value),
			    W = values.Count == 4 ? float.Parse(values[3].Value) : 1
		    };
		}

	    private static Normal CreateNormal(string definition)
	    {
		    var values = Regex.Matches(definition, FloatValuePattern);
		    return new Normal
		    {
			    X = float.Parse(values[0].Value),
			    Y = float.Parse(values[1].Value),
			    Z = float.Parse(values[2].Value)
		    };
	    }

		private static TextureCoordinate CreateTextureCoordinate(string definition)
	    {
		    var values = Regex.Matches(definition, FloatValuePattern);
		    return new TextureCoordinate
			{
			    X = float.Parse(values[0].Value),
			    Y = float.Parse(values[1].Value),
			    W = values.Count == 3 ? float.Parse(values[2].Value) : 0
		    };
		}

	    private static Face CreateFace(string definition)
	    {
		    var matches = Regex.Matches(definition, FaceIndicePattern);
		    var vertices = new Match[matches.Count];
		    matches.CopyTo(vertices, 0);
		    return new Face
		    {
			    Indices = vertices.Select(match => new Indice
				    {
					    VertexIndex = int.Parse(match.Groups[1].Value) - 1,
					    TextureIndex = match.Groups[2].Value != string.Empty ? (int?)int.Parse(match.Groups[2].Value) - 1 : null,
					    NormalIndex = match.Groups[3].Value != string.Empty ? (int?)int.Parse(match.Groups[3].Value) - 1 : null
				    })
					.ToList()
		    };
	    }
    }
}
