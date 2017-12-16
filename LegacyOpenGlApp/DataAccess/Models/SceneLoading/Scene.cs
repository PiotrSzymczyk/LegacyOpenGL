using System.Collections.Generic;

namespace LegacyOpenGlApp.DataAccess.Models.SceneLoading
{
	public class Scene
	{
		public IList<Vertex> Vertices { get; set; }

		public IList<Normal> Normals { get; set; }

		public IList<TextureCoordinate> TextureCoordinates { get; set; }

		public IList<Face> Faces { get; set; }

		public bool HasNormals => Normals.Count > 0;
	}
}