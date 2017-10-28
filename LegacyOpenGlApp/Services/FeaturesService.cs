using System.Collections.Generic;
using System.Linq;
using SharpGL;

namespace LegacyOpenGlApp.Services
{
	public static class FeaturesService
	{
		public static void SetToggles(OpenGL gl, IDictionary<uint, bool> toggles)
		{
			toggles.ToList().ForEach(toggle => SetToggle(gl, toggle));
		}

		private static void SetToggle(OpenGL gl, KeyValuePair<uint, bool> toggle)
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
}
