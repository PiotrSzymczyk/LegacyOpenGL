using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using JeremyAnsel.Media.WavefrontObj;
using LegacyOpenGlApp.DataAccess.Models.SceneLoading;

namespace LegacyOpenGlApp.DataAccess.Services
{
    public class SceneLoadingService
    {
		private string _sceneFormat => ".obj";

	    public Scene LoadScene(string objPath, string mtlPath, string texturePath)
	    {
		    return new Scene
		    {
			    Geometry = LoadGeometry(objPath),
				Materials = LoadMaterials(mtlPath),
				Texture = LoadTexture(texturePath)
		    };
	    }

		public Geometry LoadGeometry(string path)
	    {
		    path = Path.GetFullPath(path);

		    if (path.EndsWith(_sceneFormat))
		    {
			    var obj = ObjFile.FromFile(path);

				return new Geometry
				{
					Vertices = obj.Vertices.Select(vert => new Vertex(vert)).ToList(),
					Normals = obj.VertexNormals.Select(nrml => new Normal(nrml)).ToList(),
					TextureCoordinates = obj.TextureVertices.Select(tcrd => new TextureCoordinate(tcrd)).ToList(),
					Faces = obj.Faces.Select(face => new Face(face)).ToList()
				};
			}

		    return null;
		}

	    public Material[] LoadMaterials(string path)
		{
			path = Path.GetFullPath(path);

			return ObjMaterialFile
				.FromFile(path)
				.Materials
				.Select(mtl => new Material(mtl))
				.ToArray();
	    }

	    public Texture LoadTexture(string path)
	    {
			var bitmap = Image.FromFile(path) as Bitmap;
		    var bytes = new byte[bitmap.Height * bitmap.Width * 3];

		    for (int j = 0; j < bitmap.Width * bitmap.Height; j++)
		    {
			    var color = bitmap.GetPixel(j % bitmap.Width, j / bitmap.Width);

			    bytes[3 * j] = color.R;
			    bytes[3 * j + 1] = color.G;
			    bytes[3 * j + 2] = color.B;
		    }

		    return new Texture
		    {
			    Height = bitmap.Height,
				Width = bitmap.Width,
				ImageData = bytes
		    };
	    }
    }
}
