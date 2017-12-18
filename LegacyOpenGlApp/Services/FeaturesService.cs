using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using LegacyOpenGlApp.DataAccess.Models;
using LegacyOpenGlApp.Primitives;
using SharpGL;

namespace LegacyOpenGlApp.Services
{
	public static class FeaturesService
	{
		public static void SetToggles(OpenGL gl, IDictionary<uint, bool> toggles)
		{
			foreach (var toggle in toggles)
			{
				if (toggle.Value)
				{
					gl.Enable(toggle.Key);
				}
				else
				{
					gl.Disable(toggle.Key);
				}
			}
		}

		public static void SetTransformations(OpenGL gl, IEnumerable<TransformationModel> transformations, bool isFixedCoordinateSystem = true)
		{
			transformations = isFixedCoordinateSystem ? transformations.Reverse() : transformations;
			foreach (var transformation in transformations)
			{
				switch (transformation.Transform)
				{
					case Transform.Translate:
						gl.Translate(transformation.X, transformation.Y, transformation.Z);
						break;
					case Transform.Rotate:
						gl.Rotate(transformation.X, transformation.Y, transformation.Z);
						break;
					case Transform.Scale:
						gl.Scale(transformation.X, transformation.Y, transformation.Z);
						break;
					default:
						throw new InvalidEnumArgumentException();
				}
			}
		}

		public static void SetLights(OpenGL gl, IList<LightModel> lights)
		{
			for (uint i = 0; i < 8; i++)
			{
				if (i < lights.Count)
				{
					var light = lights[(int) i];

					gl.Enable(OpenGL.GL_LIGHT0 + i);
					gl.Light(OpenGL.GL_LIGHT0 + i, OpenGL.GL_AMBIENT, light.Ambient);
					gl.Light(OpenGL.GL_LIGHT0 + i, OpenGL.GL_DIFFUSE, light.Diffuse);
					gl.Light(OpenGL.GL_LIGHT0 + i, OpenGL.GL_SPECULAR, light.Specular);
					gl.Light(OpenGL.GL_LIGHT0 + i, OpenGL.GL_POSITION, light.Position);
					gl.Light(OpenGL.GL_LIGHT0 + i, OpenGL.GL_SPOT_DIRECTION, light.SpotlightDirection);
					gl.Light(OpenGL.GL_LIGHT0 + i, OpenGL.GL_SPOT_EXPONENT, light.SpotlightExponent);
					gl.Light(OpenGL.GL_LIGHT0 + i, OpenGL.GL_SPOT_CUTOFF, light.SpotlightCutoff);
					gl.Light(OpenGL.GL_LIGHT0 + i, OpenGL.GL_CONSTANT_ATTENUATION, light.ConstantAttenuation);
					gl.Light(OpenGL.GL_LIGHT0 + i, OpenGL.GL_LINEAR_ATTENUATION, light.LinearAttenuation);
					gl.Light(OpenGL.GL_LIGHT0 + i, OpenGL.GL_QUADRATIC_ATTENUATION, light.QuadraticAttenuation);

					DrawMarkerInLightPosition(gl, light.Position);
				}
				else
				{
					gl.Disable(OpenGL.GL_LIGHT0 + i);
				}
			}
		}

		private static void DrawMarkerInLightPosition(OpenGL gl, float[] position)
		{
			gl.Disable(OpenGL.GL_LIGHTING);
			gl.Color(0.0, 1.0, 1.0);
			gl.PushMatrix();
			gl.Translate(position[0], position[1], position[2]);
			gl.Sphere(gl.NewQuadric(), 0.05, 16, 16);
			gl.PopMatrix();
			gl.Enable(OpenGL.GL_LIGHTING);
		}
	}
}
