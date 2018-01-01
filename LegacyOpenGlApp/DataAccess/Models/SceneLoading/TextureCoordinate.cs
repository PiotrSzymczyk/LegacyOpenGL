using JeremyAnsel.Media.WavefrontObj;

namespace LegacyOpenGlApp.DataAccess.Models.SceneLoading
{
	public class TextureCoordinate
	{
		public TextureCoordinate(ObjVector3 v)
		{
			X = v.X;
			Y = v.Y;
			W = v.Z;
		}

		public float X { get; set; }
		public float Y { get; set; }
		public float W { get; set; } = 0;
	}
}