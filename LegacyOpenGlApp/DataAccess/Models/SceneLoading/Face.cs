using System.Collections.Generic;
using System.Linq;
using JeremyAnsel.Media.WavefrontObj;

namespace LegacyOpenGlApp.DataAccess.Models.SceneLoading
{
	public class Face
	{
		public Face(ObjFace face)
		{
			Indices = face.Vertices.Select(vert => new Indice(vert)).ToList();
		}

		public IList<Indice> Indices { get; set; }

		public int IndexCount => Indices.Count;
	}
}