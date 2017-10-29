using System.Collections.Generic;
using System.Linq;
using LegacyOpenGlApp.Models;
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

		public static void SetTransformations(OpenGL gl, IList<TransformationModel> transformations)
		{
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
				}
			}
		}
	}
}
