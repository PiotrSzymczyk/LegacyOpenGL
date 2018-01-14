using System.Text;

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

		public float[] Ambient { get; set; } = new float[4];

		public float[] Diffuse { get; set; } = new float[4];

		public float[] Specular { get; set; } = new float[4];

		public float[] Position { get; set; } = new float[4];

		public float[] SpotlightDirection { get; set; } = new float[3];

		public float SpotlightExponent { get; set; }

		public float SpotlightCutoff { get; set; }

		public float ConstantAttenuation { get; set; } = 1;

		public float LinearAttenuation { get; set; }

		public float QuadraticAttenuation { get; set; }

		public override string ToString()
		{
			var light = new StringBuilder();
			light.Append("Light:\n");
			light.Append($"Position( {Position[0]}, {Position[1]}, {Position[2]}, {Position[3]} ),\n");
			light.Append($"Ambient( {Ambient[0]}, {Ambient[1]}, {Ambient[2]}, {Ambient[3]} ), ");
			light.Append($"Diffuse( {Diffuse[0]}, {Diffuse[1]}, {Diffuse[2]}, {Diffuse[3]} ), ");
			light.Append($"Specular( {Specular[0]}, {Specular[1]}, {Specular[2]}, {Specular[3]} ),\n");
			light.Append($"SpotlightDirection( {SpotlightDirection[0]}, {SpotlightDirection[1]}, {SpotlightDirection[2]} ), ");
			light.Append($"SpotlightExponent( {SpotlightExponent} ), ");
			light.Append($"SpotlightCutoff( {SpotlightCutoff} ),\n");
			light.Append($"ConstantAttenuation( {ConstantAttenuation} ), ");
			light.Append($"LinearAttenuation( {SpotlightExponent} ), ");
			light.Append($"QuadraticAttenuation( {SpotlightExponent} )");
			return light.ToString();
		}
	}
}