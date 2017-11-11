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
		static string _indent = "";

		public static string GenerateCode(SceneDefinitionServiceModel scene, SceneSettingsServiceModel settings)
		{
			var code = new StringBuilder();

			code.AppendLine($"{_indent}using SharpGL;");
			code.AppendLine();
			code.AppendLine($"{_indent}public static class OpenGlGeneratedCode");
			code.AppendLine($"{_indent}{{");
			_indent += "\t";

			GenerateInitializeMethod(settings, code);

			code.AppendLine();

			GenerateDrawMethod(scene, settings, code);

			_indent = _indent.Substring(0, _indent.Length - 1);

			code.AppendLine($"{_indent}}}");

			return code.ToString();
		}

		private static void GenerateInitializeMethod(SceneSettingsServiceModel settings, StringBuilder code)
		{
			code.AppendLine($"{_indent}public void OpenGLInitialize(OpenGL gl)");
			code.AppendLine($"{_indent}{{");
			_indent += "\t";

			SetToggles(settings.Toggles, code);

			code.AppendLine();

			SetLights(settings.Lights, code);

			_indent = _indent.Substring(0, _indent.Length - 1);

			code.AppendLine($"{_indent}}}");
		}

		private static void GenerateDrawMethod(SceneDefinitionServiceModel scene, SceneSettingsServiceModel settings, StringBuilder code)
		{
			code.AppendLine($"{_indent}public void OpenGLDraw(OpenGL gl)");
			code.AppendLine($"{_indent}{{");
			_indent += "\t";

			code.AppendLine($"{_indent}//  Clear the color and depth buffers.");
			code.AppendLine($"{_indent}gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);");
			code.AppendLine();

			code.AppendLine($"{_indent}//  Reset the modelview matrix.");
			code.AppendLine($"{_indent}gl.LoadIdentity();");
			code.AppendLine();

			SetTransformations(settings.Transformations, code);
			code.AppendLine();

			foreach (var mesh in scene.Scene.Meshes)
			{
				//TODO: Apply material

				foreach (var face in mesh.Faces)
				{
					var faceMode = GetFaceDrawingMode(face.IndexCount);

					code.AppendLine($"{_indent}gl.Begin({faceMode});");

					foreach (var index in face.Indices)
					{
						if (mesh.HasVertexColors(0))
						{
							var color = mesh.VertexColorChannels[0][index];
							code.AppendLine($"{_indent}gl.Color({color.R:F}, {color.G:F}, {color.B:F}, {color.A:F});");
						}

						if (mesh.HasNormals)
						{
							var normal = mesh.Normals[index];
							code.AppendLine($"{_indent}gl.Normal({normal.X:F}, {normal.Y:F}, {normal.Z:F});");
						}

						var vertex = mesh.Vertices[index];
						code.AppendLine($"{_indent}gl.Vertex({vertex.X:F}, {vertex.Y:F}, {vertex.Z:F});");
					}

					code.AppendLine($"{_indent}gl.End();");

					code.AppendLine();
				}
			}
			code.AppendLine();

			code.AppendLine($"{_indent}gl.Flush();");

			_indent = _indent.Substring(0, _indent.Length - 1);

			code.AppendLine($"{_indent}}}");
		}

		private static string GetFaceDrawingMode(int faceIndexCount)
		{
			switch (faceIndexCount)
			{
				case 1: return "OpenGL.GL_POINTS";
				case 2: return "OpenGL.GL_LINES";
				case 3: return "OpenGL.GL_TRIANGLES";
				case 4: return "OpenGL.GL_QUADS";
				default: return "OpenGL.GL_POLYGON_MODE";
			}
		}

		private static void SetToggles(IDictionary<uint, bool> togglesDict, StringBuilder code)
		{
			var toggles = Config.OpenGlToggles.Where(toggle => toggle.IsActive != togglesDict[toggle.StateVariable]).ToList();

			if (toggles.Any() == false) return;

			code.AppendLine($"{_indent}//  Set Toggles");

			foreach (var toggle in toggles)
			{
				code.AppendLine(toggle.IsActive
					? $"{_indent}gl.Disable(OpenGL.{toggle.StateVariableName});"
					: $"{_indent}gl.Enable(OpenGL.{toggle.StateVariableName});");
			}
		}

		private static void SetLights(IList<LightModel> lights, StringBuilder code)
		{
			if (lights.Any() == false) return;

			code.AppendLine($"{_indent}//  Set Lights");

			var i = 0;
			foreach (var light in lights)
			{
				code.AppendLine($"{_indent}gl.Enable(OpenGL.GL_LIGHT{i});");

				code.AppendLine($"{_indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_AMBIENT, new[] {{ {light.Ambient[0]}, {light.Ambient[1]}, {light.Ambient[2]}, {light.Ambient[3]} }});");
				code.AppendLine($"{_indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_DIFFUSE, new[] {{ {light.Diffuse[0]}, {light.Diffuse[1]}, {light.Diffuse[2]}, {light.Diffuse[3]} }});");
				code.AppendLine($"{_indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_SPECULAR, new[] {{ {light.Specular[0]}, {light.Specular[1]}, {light.Specular[2]}, {light.Specular[3]} }});");
				code.AppendLine($"{_indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_POSITION, new[] {{ {light.Position[0]}, {light.Position[1]}, {light.Position[2]}, {light.Position[3]} }});");
				code.AppendLine($"{_indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_SPOT_DIRECTION, new[] {{ {light.SpotlightDirection[0]}, {light.SpotlightDirection[1]}, {light.SpotlightDirection[2]} }});");
				code.AppendLine($"{_indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_SPOT_EXPONENT, {light.SpotlightExponent});");
				code.AppendLine($"{_indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_SPOT_CUTOFF, {light.SpotlightCutoff});");
				code.AppendLine($"{_indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_CONSTANT_ATTENUATION, {light.ConstantAttenuation});");
				code.AppendLine($"{_indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_LINEAR_ATTENUATION, {light.LinearAttenuation});");
				code.AppendLine($"{_indent}gl.Light(OpenGL.GL_LIGHT{i}, OpenGL.GL_QUADRATIC_ATTENUATION, {light.QuadraticAttenuation});");
				code.AppendLine();
				i++;
			}
		}

		private static void SetTransformations(IList<TransformationModel> transformations, StringBuilder code)
		{
			if (transformations.Any() == false) return;

			code.AppendLine($"{_indent}//  Set Transformations");

			foreach (var transformation in transformations)
			{
				switch (transformation.Transform)
				{
					case Transform.Translate:
						code.AppendLine($"{_indent}gl.Translate({transformation.X:F}f, {transformation.Y:F}f, {transformation.Z:F}f);");
						break;
					case Transform.Rotate:
						code.AppendLine($"{_indent}gl.Rotate({transformation.X:F}f, {transformation.Y:F}f, {transformation.Z:F}f);");
						break;
					case Transform.Scale:
						code.AppendLine($"{_indent}gl.Scale({transformation.X:F}f, {transformation.Y:F}f, {transformation.Z:F}f);");
						break;
					default:
						throw new InvalidEnumArgumentException();
				}
			}
		}
	}
}
