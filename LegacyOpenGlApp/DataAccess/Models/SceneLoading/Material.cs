using System.Text;
using JeremyAnsel.Media.WavefrontObj;

namespace LegacyOpenGlApp.DataAccess.Models.SceneLoading
{
	public class Material
	{
		public Material(ObjMaterial objMaterial)
		{
			Name = objMaterial.Name;
			AmbientColor = objMaterial.AmbientColor != null ? new Color(objMaterial.AmbientColor) : new Color(0, 0, 0);
			DiffuseColor = objMaterial.DiffuseColor != null ? new Color(objMaterial.DiffuseColor, objMaterial.DissolveFactor) : new Color(0, 0, 0);
			EmissiveColor = objMaterial.EmissiveColor!= null ? new Color(objMaterial.EmissiveColor) : new Color(0, 0, 0);
			SpecularColor = objMaterial.SpecularColor != null ? new Color(objMaterial.SpecularColor) : new Color(0, 0, 0);
			SpecularExponent = objMaterial.SpecularExponent;
		}

		public string Name { get; set; }

		public Color AmbientColor { get; set; }

		public Color DiffuseColor { get; set; }

		public Color EmissiveColor { get; set; }

		public Color SpecularColor { get; set; }

		public float SpecularExponent { get; set; }

		public override string ToString()
		{
			var light = new StringBuilder();
			light.Append($"{Name}:\n");
			light.Append($"Ambient color: ( {AmbientColor.R}, {AmbientColor.G}, {AmbientColor.B}, {AmbientColor.A} )\n");
			light.Append($"Diffuse color: ( {DiffuseColor.R}, {DiffuseColor.G}, {DiffuseColor.B}, {DiffuseColor.A} )\n");
			light.Append($"Emissive color: ( {EmissiveColor.R}, {EmissiveColor.G}, {EmissiveColor.B}, {EmissiveColor.A} )\n");
			light.Append($"Specular color: ( {SpecularColor.R}, {SpecularColor.G}, {SpecularColor.B}, {SpecularColor.A} )\n");
			light.Append($"Specular exponent: {SpecularExponent}");
			return light.ToString();
		}
	}
}