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
			new ToggleModel { StateVariable = 32826, StateVariableName = "GL_RESCALE_NORMAL_EXT", Description = "Current normal rescaling on/off", IsActive = false },
			new ToggleModel { StateVariable = 2903, StateVariableName = "GL_COLOR_MATERIAL", Description = "Color tracking", IsActive = false },
			new ToggleModel { StateVariable = 2897, StateVariableName = "GL_LIGHT_MODEL_LOCAL_VIEWER", Description = "Viewer is local", IsActive = false },
			new ToggleModel { StateVariable = 2898, StateVariableName = "GL_LIGHT_MODEL_TWO_SIDE", Description = "Use two-sided lighting", IsActive = false },
			new ToggleModel { StateVariable = 2832, StateVariableName = "GL_POINT_SMOOTH", Description = "Point antialiasing", IsActive = false },
			new ToggleModel { StateVariable = 2848, StateVariableName = "GL_LINE_SMOOTH", Description = "Line antialiasing", IsActive = false },
			new ToggleModel { StateVariable = 2852, StateVariableName = "GL_LINE_STIPPLE", Description = "Line stipple", IsActive = false },
			new ToggleModel { StateVariable = 2881, StateVariableName = "GL_POLYGON_SMOOTH", Description = "Polygon antialiasing", IsActive = false },
			new ToggleModel { StateVariable = 2882, StateVariableName = "GL_POLYGON_STIPPLE", Description = "Polygon stipple", IsActive = false },
			new ToggleModel { StateVariable = 2884, StateVariableName = "GL_CULL_FACE", Description = "Polygon culling", IsActive = false },
			new ToggleModel { StateVariable = 32925, StateVariableName = "GL_MULTISAMPLE", Description = "Multisample rasterization", IsActive = true },
			new ToggleModel { StateVariable = 3089, StateVariableName = "GL_SCISSOR_TEST", Description = "Scissoring", IsActive = false },
			new ToggleModel { StateVariable = 3008, StateVariableName = "GL_ALPHA_TEST", Description = "Alpha test enabled", IsActive = false },
			new ToggleModel { StateVariable = 2960, StateVariableName = "GL_STENCIL_TEST", Description = "Stenciling", IsActive = false },
			new ToggleModel { StateVariable = 2929, StateVariableName = "GL_DEPTH_TEST", Description = "Depth buffer", IsActive = false },
			new ToggleModel { StateVariable = 3042, StateVariableName = "GL_BLEND", Description = "Blending", IsActive = false },
			new ToggleModel { StateVariable = 3024, StateVariableName = "GL_DITHER", Description = "Dithering", IsActive = true },
			new ToggleModel { StateVariable = 3344, StateVariableName = "GL_MAP_COLOR", Description = "True if colors are mapped", IsActive = false },
			new ToggleModel { StateVariable = 3345, StateVariableName = "GL_MAP_STENCIL", Description = "True if stencil values are mapped", IsActive = false },
			new ToggleModel { StateVariable = 32804, StateVariableName = "GL_HISTOGRAM_EXT", Description = "True if histogramming is enabled", IsActive = false },
		};

		public static readonly IList<TransformationModel> Transformations = new List<TransformationModel>
		{
			new TransformationModel { Transform = Transform.Translate, Z = -5f },
			new TransformationModel { Transform = Transform.Translate, Z = -4f },
		};

		public static readonly IList<LightModel> Lights = new List<LightModel>
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
			}
		};

		public static string DefaultScenePath = "C:\\Code\\LegacyOpenGL\\LegacyOpenGlApp\\body.obj";

		public static readonly CameraModel Camera = new CameraModel
		{
			PositionX = 0,
			PositionY = 0,
			PositionZ = 0,
			AimX = 0,
			AimY = 0,
			AimZ = -1,
			UpX = 0,
			UpY = 1,
			UpZ = 0
		};
	}
}
