namespace LegacyOpenGlApp.WindowViewModels
{
	public class AddLightWindowViewModel
	{
		public float AmbientR { get; set; }
		public float AmbientG { get; set; }
		public float AmbientB { get; set; }
		public float AmbientA { get; set; }

		public float DiffuseR { get; set; }
		public float DiffuseG { get; set; }
		public float DiffuseB { get; set; }
		public float DiffuseA { get; set; }

		public float SpecularR { get; set; }
		public float SpecularG { get; set; }
		public float SpecularB { get; set; }
		public float SpecularA { get; set; }

		public float PositionX { get; set; }
		public float PositionY { get; set; }
		public float PositionZ { get; set; }
		public float PositionW { get; set; }

		public float SpotlightDirectionX { get; set; }
		public float SpotlightDirectionY { get; set; }
		public float SpotlightDirectionZ { get; set; }

		public float SpotlightExponent { get; set; }

		public float SpotlightCutoff { get; set; }

		public float ConstantAttenuation { get; set; }

		public float LinearAttenuation { get; set; }

		public float QuadraticAttenuation { get; set; }
	}
}
