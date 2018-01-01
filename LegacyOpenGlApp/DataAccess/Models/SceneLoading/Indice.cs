using JeremyAnsel.Media.WavefrontObj;

namespace LegacyOpenGlApp.DataAccess.Models.SceneLoading
{
	public class Indice
	{
		public Indice(ObjTriplet vert)
		{
			Vertex = vert.Vertex - 1;
			Normal = vert.Normal - 1;
			Texture = vert.Texture - 1;
		}

		public int Vertex { get; set; }

		public int Normal { get; set; }

		public int Texture { get; set; }
	}
}