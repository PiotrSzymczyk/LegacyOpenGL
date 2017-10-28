using System.Collections.Generic;
using LegacyOpenGlApp.Models;

namespace LegacyOpenGlApp.Services
{
	public static class Config
	{
		public static readonly List<OpenGlToggle> OpenGlToggles = new List<OpenGlToggle>
		{
			new OpenGlToggle { StateVariable = 100204, StateVariableName = "GLU_DISPLAY_MODE", Description = "Display mode", IsActive = true },
			new OpenGlToggle { StateVariable = 2896, StateVariableName = "GL_LIGHTING", Description = "Lighting", IsActive = false },
			new OpenGlToggle { StateVariable = 2912, StateVariableName = "GL_FOG", Description = "Fog", IsActive = false },
			new OpenGlToggle { StateVariable = 2883, StateVariableName = "GL_EDGE_FLAG", Description = "Edge flag", IsActive = true },
		};
	}
}
