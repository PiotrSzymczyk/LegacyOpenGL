using JeremyAnsel.Media.WavefrontObj;

namespace LegacyOpenGlApp.DataAccess.Models.SceneLoading
{
	public class Material
	{
		public Material(ObjMaterial objMaterial)
		{
			Name = objMaterial.Name;
			AmbientColor = objMaterial.AmbientColor != null ? new Color(objMaterial.AmbientColor) : null;
			DiffuseColor = objMaterial.DiffuseColor != null ? new Color(objMaterial.DiffuseColor, objMaterial.DissolveFactor) : null;
			SpecularColor = objMaterial.SpecularColor != null ? new Color(objMaterial.SpecularColor) : null;
			IlluminationModel = objMaterial.IlluminationModel;
		}

		public string Name { get; set; }

		public Color AmbientColor { get; set; }

		public Color DiffuseColor { get; set; }

		public Color SpecularColor { get; set; }

		public int IlluminationModel { get; set; }
	}
}