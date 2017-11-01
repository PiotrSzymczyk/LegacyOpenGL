using System.Collections.Generic;
using LegacyOpenGlApp.DataAccess.Models;

namespace LegacyOpenGlApp.Primitives
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

		public  static readonly IList<LightModel> Lights = new List<LightModel>
		{
			new LightModel
			{
				Ambient = new []{0f,0f,0f,1f},
				Diffuse = new []{1f,1f,1f,1f},
				Specular = new []{1f,1f,1f,1f},
				Position = new []{0f,0f,1f,0f},
				SpotlightDirection = new []{0f,0f,-1f},
				SpotlightExponent = 0f,
				SpotlightCutoff = 180f,
				ConstantAttenuation = 1,
				LinearAttenuation = 0,
				QuadraticAttenuation = 0
			},
			new LightModel
			{
				Ambient = new []{1f,0f,0f,1f},
				Diffuse = new []{0f,0f,0f,1f},
				Specular = new []{0f,0f,0f,1f},
				Position = new []{0f,0f,1f,0f},
				SpotlightDirection = new []{0f,0f,-1f},
				SpotlightExponent = 0f,
				SpotlightCutoff = 1f,
				ConstantAttenuation = 1,
				LinearAttenuation = 0,
				QuadraticAttenuation = 0
			}
		};
	}
}
