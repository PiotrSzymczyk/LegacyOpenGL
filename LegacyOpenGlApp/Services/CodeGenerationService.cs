using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using LegacyOpenGlApp.DataAccess.Models;
using LegacyOpenGlApp.Primitives;

namespace LegacyOpenGlApp.Services
{
	public static class CodeGenerationService
	{
		public static string GenerateCode(SceneDefinitionServiceModel scene, SceneSettingsServiceModel settings)
		{
			var code = new StringBuilder();
			var indent = "";
			code.AppendLine("public void OpenGLDraw(OpenGL gl)");
			code.AppendLine("{");
			indent += "\t";

			SetToggles(settings.Toggles, code, indent);

			SetLights(settings.Lights, code, indent);

			code.AppendLine($"{indent}//  Clear the color and depth buffers.");
			code.AppendLine($"{indent}gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);");
			code.AppendLine();

			code.AppendLine($"{indent}//  Reset the modelview matriX:F.");
			code.AppendLine($"{indent}gl.LoadIdentity();");
			code.AppendLine();

			SetTransformations(settings.Transformations, code, indent);
			code.AppendLine();

			code.AppendLine($"{indent}gl.Begin(OpenGL.GL_QUADS);");
			code.AppendLine();

			foreach (var mesh in scene.Scene.Meshes)
			{
				foreach (var face in mesh.Faces)
				{
					foreach (var i in face.Indices)
					{
						if (mesh.HasVertexColors(i))
						{
							var color = mesh.VertexColorChannels[i];
							code.AppendLine($"{indent}gl.Color({color[0]}, {color[1]}, {color[2]}, {color[3]});");
						}

						var vert = mesh.Vertices[i];
						code.AppendLine($"{indent}gl.Vertex({vert.X:F}f, {vert.Y:F}f, {vert.Z:F}f);");
					}

					code.AppendLine();
				}

				code.AppendLine();
			}
			code.AppendLine();

			code.AppendLine($"{indent}gl.End();");
			code.AppendLine($"{indent}gl.Flush();");

			code.AppendLine("}");
			indent = indent.Substring(0, indent.Length - 1);

			return code.ToString();
		}

		private static void SetToggles(IDictionary<uint, bool> togglesDict, StringBuilder code, string indent)
		{
			var toggles = Config.OpenGlToggles.Where(toggle => toggle.IsActive != togglesDict[toggle.StateVariable]).ToList();

			if (toggles.Any() == false) return;

			code.AppendLine($"{indent}//  Set Toggles");

			foreach (var toggle in toggles)
			{
				code.AppendLine(toggle.IsActive
					? $"{indent}gl.Disable(OpenGL.{toggle.StateVariableName});"
					: $"{indent}gl.Enable(OpenGL.{toggle.StateVariableName});");
			}

			code.AppendLine();
		}

		private static void SetLights(IList<LightModel> lights, StringBuilder code, string indent)
		{
			if (lights.Any() == false) return;

			code.AppendLine($"{indent}//  Set Lights");

			var i = 0;
			foreach (var light in lights)
			{
				code.AppendLine($"{indent}gl.Enable(OpenGL.GL_LIGHT{i});");

				code.AppendLine($"{indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_AMBIENT, new[] {{ {light.Ambient[0]}, {light.Ambient[1]}, {light.Ambient[2]}, {light.Ambient[3]} }});");
				code.AppendLine($"{indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_DIFFUSE, new[] {{ {light.Diffuse[0]}, {light.Diffuse[1]}, {light.Diffuse[2]}, {light.Diffuse[3]} }});");
				code.AppendLine($"{indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_SPECULAR, new[] {{ {light.Specular[0]}, {light.Specular[1]}, {light.Specular[2]}, {light.Specular[3]} }});");
				code.AppendLine($"{indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_POSITION, new[] {{ {light.Position[0]}, {light.Position[1]}, {light.Position[2]}, {light.Position[3]} }});");
				code.AppendLine($"{indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_SPOT_DIRECTION, new[] {{ {light.SpotlightDirection[0]}, {light.SpotlightDirection[1]}, {light.SpotlightDirection[2]} }});");
				code.AppendLine($"{indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_SPOT_EXPONENT, {light.SpotlightExponent});");
				code.AppendLine($"{indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_SPOT_CUTOFF, {light.SpotlightCutoff});");
				code.AppendLine($"{indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_CONSTANT_ATTENUATION, {light.ConstantAttenuation});");
				code.AppendLine($"{indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_LINEAR_ATTENUATION, {light.LinearAttenuation});");
				code.AppendLine($"{indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_QUADRATIC_ATTENUATION, {light.QuadraticAttenuation});");
				code.AppendLine();
				i++;
			}

			code.AppendLine();
		}

		private static void SetTransformations(IList<TransformationModel> transformations, StringBuilder code, string indent)
		{
			if (transformations.Any() == false) return;

			code.AppendLine($"{indent}//  Set Transformations");

			foreach (var transformation in transformations)
			{
				switch (transformation.Transform)
				{
					case Transform.Translate:
						code.AppendLine($"{indent}gl.Translate({transformation.X:F}f, {transformation.Y:F}f, {transformation.Z:F}f);");
						break;
					case Transform.Rotate:
						code.AppendLine($"{indent}gl.Rotate({transformation.X:F}f, {transformation.Y:F}f, {transformation.Z:F}f);");
						break;
					case Transform.Scale:
						code.AppendLine($"{indent}gl.Scale({transformation.X:F}f, {transformation.Y:F}f, {transformation.Z:F}f);");
						break;
					default:
						throw new InvalidEnumArgumentException();
				}
			}
		}
	}
}
