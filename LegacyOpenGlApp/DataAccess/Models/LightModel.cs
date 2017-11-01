namespace LegacyOpenGlApp.DataAccess.Models
{
	public class LightModel
	{
		public float[] Ambient { get; set; }

		public float[] Diffuse { get; set; }

		public float[] Specular { get; set; }

		public float[] Position { get; set; }

		public float[] SpotlightDirection { get; set; }

		public float SpotlightExponent { get; set; }

		public float SpotlightCutoff { get; set; }

		public float ConstantAttenuation { get; set; }

		public float LinearAttenuation { get; set; }

		public float QuadraticAttenuation { get; set; }
	}
}