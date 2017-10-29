using System.Collections.Generic;
using LegacyOpenGlApp.Models;
using LegacyOpenGlApp.Primitives;

namespace LegacyOpenGlApp.Services
{
	public static class Config
	{
		public static readonly IList<ToggleModel> OpenGlToggles = new List<ToggleModel>
		{
			new ToggleModel { StateVariable = 100204, StateVariableName = "GLU_DISPLAY_MODE", Description = "Display mode", IsActive = true },
			new ToggleModel { StateVariable = 2896, StateVariableName = "GL_LIGHTING", Description = "Lighting", IsActive = false },
			new ToggleModel { StateVariable = 2912, StateVariableName = "GL_FOG", Description = "Fog", IsActive = false },
			new ToggleModel { StateVariable = 2883, StateVariableName = "GL_EDGE_FLAG", Description = "Edge flag", IsActive = true },
		};

		public static readonly IList<TransformationModel> Transformations = new List<TransformationModel>
		{
			new TransformationModel { Transform = Transform.Translate, Z = -4f },
			new TransformationModel { Transform = Transform.Rotate, X = 20, Y = 50, Z = 80 },
		};
	}
}
