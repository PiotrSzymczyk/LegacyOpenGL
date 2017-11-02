namespace LegacyOpenGlApp.DataAccess.Models
{
	public class LightModel
	{
		public LightModel()
		{
		}

		public LightModel(LightModel light)
		{
			this.Ambient = light.Ambient;
			this.Diffuse = light.Diffuse;
			this.Specular = light.Specular;
			this.Position = light.Position;
			this.SpotlightDirection = light.SpotlightDirection;
			this.SpotlightExponent = light.SpotlightExponent;
			this.SpotlightCutoff = light.SpotlightCutoff;
			this.ConstantAttenuation = light.ConstantAttenuation;
			this.LinearAttenuation = light.LinearAttenuation;
			this.QuadraticAttenuation = light.QuadraticAttenuation;
		}

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