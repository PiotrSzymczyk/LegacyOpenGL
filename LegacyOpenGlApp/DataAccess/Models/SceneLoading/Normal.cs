using JeremyAnsel.Media.WavefrontObj;

namespace LegacyOpenGlApp.DataAccess.Models.SceneLoading
{
	public class Normal
	{
		public Normal(ObjVector3 v)
		{
			X = v.X;
			Y = v.Y;
			Z = v.Z;
		}

		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }
	}
}