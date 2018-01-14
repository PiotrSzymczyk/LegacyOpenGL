using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using LegacyOpenGlApp.DataAccess.Models;
using LegacyOpenGlApp.DataAccess.Services;
using LegacyOpenGlApp.Primitives;
using SharpGL;
using Unity;

namespace LegacyOpenGlApp.Services
{
	public class CodeGenerationService
	{
		[Dependency]
		public ConfigurationService ConfigurationService { get; set; }

		string _indent = "";

		public string GenerateCode(SceneServiceModel scene, OpenGLSettingsServiceModel settings)
		{
			var code = new StringBuilder();

			GenerateMainMethod(code);

			code.AppendLine();

			GenerateInitializeMethod(settings, code);

			code.AppendLine();

			GenerateReshapeMethod(code, settings);

			code.AppendLine();

			GenerateDrawMethod(scene, settings, code);

			return code.ToString();
		}

		private void GenerateReshapeMethod(StringBuilder code, OpenGLSettingsServiceModel settings)
		{
			code.AppendLine($"{_indent}void reshape(int w, int h)");
			code.AppendLine($"{_indent}{{");
			_indent += "\t";

			code.AppendLine($"{_indent}glViewport(0, 0, (GLsizei) w, (GLsizei) h);");
			code.AppendLine($"{_indent}glMatrixMode(GL_PROJECTION);");
			code.AppendLine($"{_indent}glLoadIdentity();");
			code.AppendLine($"{_indent}int xyRatio = (GLfloat) w / (GLfloat) h;");
			code.AppendLine($"{_indent}gluPerspective(45.0f, xyRatio, 0.1f, 100.0f);");
			code.AppendLine($"{_indent}glMatrixMode(GL_MODELVIEW);");

			_indent = _indent.Substring(0, _indent.Length - 1);
			code.AppendLine($"{_indent}}}");
		}

		private void GenerateMainMethod(StringBuilder code)
		{
			code.AppendLine($"{_indent}int main(int argc, char** argv)");
			code.AppendLine($"{_indent}{{");
			_indent += "\t";

			code.AppendLine($"{_indent}glutInit(&argc, argv);");
			code.AppendLine($"{_indent}glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);");
			code.AppendLine($"{_indent}glutInitWindowSize(500, 500);");
			code.AppendLine($"{_indent}glutInitWindowPosition(100, 100);");
			code.AppendLine($"{_indent}glutCreateWindow(argv[0]);");
			code.AppendLine($"{_indent}init();");
			code.AppendLine($"{_indent}glutDisplayFunc(display);");
			code.AppendLine($"{_indent}glutReshapeFunc(reshape);");
			code.AppendLine($"{_indent}glutMainLoop();");
			code.AppendLine($"{_indent}return 0;");

			_indent = _indent.Substring(0, _indent.Length - 1);
			code.AppendLine($"{_indent}}}");
		}

		private void GenerateInitializeMethod(OpenGLSettingsServiceModel settings, StringBuilder code)
		{
			code.AppendLine($"{_indent}void init(void)");
			code.AppendLine($"{_indent}{{");
			_indent += "\t";

			code.AppendLine($"{_indent}glClearColor(0.0, 0.0, 0.0, 0.0)");
			code.AppendLine($"{_indent}glShadeModel(GL_SMOOTH);");
			code.AppendLine($"{_indent}glEnable(GL_DEPTH_TEST);");

			SetToggles(code, settings.Toggles);

			code.AppendLine();

			if (IsLightingEnabled(settings.Toggles))
			{
				DefineLights(code, settings.Lights);
			}

			_indent = _indent.Substring(0, _indent.Length - 1);
			code.AppendLine($"{_indent}}}");
		}

		private void GenerateDrawMethod(SceneServiceModel scene, OpenGLSettingsServiceModel settings, StringBuilder code)
		{
			code.AppendLine($"{_indent}void draw(void)");
			code.AppendLine($"{_indent}{{");
			_indent += "\t";

			code.AppendLine($"{_indent}//  Clear the color and depth buffers.");
			code.AppendLine($"{_indent}glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);");
			code.AppendLine();

			code.AppendLine($"{_indent}//  Reset the modelview matrix.");
			code.AppendLine($"{_indent}glLoadIdentity();");
			code.AppendLine();

			code.AppendLine($"{_indent}glLookAt(");
			_indent += "\t";
			code.AppendLine($"{_indent}{settings.Camera.PositionX:F}, {settings.Camera.PositionY:F}, {settings.Camera.PositionZ:F},");
			code.AppendLine($"{_indent}{settings.Camera.AimX:F}, {settings.Camera.AimY:F}, {settings.Camera.AimZ:F},");
			code.AppendLine($"{_indent}{settings.Camera.UpX:F}, {settings.Camera.UpY:F}, {settings.Camera.UpZ:F}");
			_indent = _indent.Substring(0, _indent.Length - 1);
			code.AppendLine($"{_indent});");

			if (IsLightingEnabled(settings.Toggles))
			{
				PositionLights(code, settings.Lights);
			}

			SetTransformations(code, settings.Transformations);
			code.AppendLine();
			
				//TODO: Apply material

			foreach (var face in scene.Scene.Geometry.Faces)
			{
				var faceMode = GetFaceDrawingMode(face.IndexCount);

				code.AppendLine($"{_indent}glBegin({faceMode});");

				foreach (var indice in face.Indices)
				{
					if (scene.Scene.Geometry.HasNormals)
					{
						var normal = scene.Scene.Geometry.Normals[indice.Normal];
						code.AppendLine($"{_indent}glNormal({normal.X:F}, {normal.Y:F}, {normal.Z:F});");
					}

					var vertex = scene.Scene.Geometry.Vertices[indice.Vertex];
					code.AppendLine($"{_indent}glVertex({vertex.X:F}, {vertex.Y:F}, {vertex.Z:F});");
				}

				code.AppendLine($"{_indent}glEnd();");

				code.AppendLine();
			}
			code.AppendLine();

			code.AppendLine($"{_indent}glFlush();");

			_indent = _indent.Substring(0, _indent.Length - 1);

			code.AppendLine($"{_indent}}}");
		}

		private string GetFaceDrawingMode(int faceIndexCount)
		{
			switch (faceIndexCount)
			{
				case 1: return "GL_POINTS";
				case 2: return "GL_LINES";
				case 3: return "GL_TRIANGLES";
				case 4: return "GL_QUADS";
				default: return "GL_POLYGON_MODE";
			}
		}

		private bool IsLightingEnabled(IDictionary<uint, bool> toggles) =>
			toggles.Single(toggle => toggle.Key == OpenGL.GL_LIGHTING).Value;

		private void SetToggles(StringBuilder code, IDictionary<uint, bool> togglesDict)
		{
			var toggles = ConfigurationService.OpenGlToggles.Where(toggle => toggle.IsActive != togglesDict[toggle.StateVariable]).ToList();

			if (toggles.Any() == false) return;

			code.AppendLine($"{_indent}//  Set Toggles");

			foreach (var toggle in toggles)
			{
				code.AppendLine(toggle.IsActive
					? $"{_indent}glDisable({toggle.StateVariableName});"
					: $"{_indent}glEnable({toggle.StateVariableName});");
			}
		}

		private void DefineLights(StringBuilder code, IList<LightModel> lights)
		{
			if (lights.Any() == false) return;

			code.AppendLine($"{_indent}//  Define & enable lights");

			var i = 0;
			foreach (var light in lights)
			{
				code.AppendLine($"{_indent}glEnable(GL_LIGHT{i});");

				code.AppendLine($"{_indent}glLightfv(GL_LIGHT{i}, GL_AMBIENT, new[] {{ {light.Ambient[0]}, {light.Ambient[1]}, {light.Ambient[2]}, {light.Ambient[3]} }});");
				code.AppendLine($"{_indent}glLightfv(GL_LIGHT{i}, GL_DIFFUSE, new[] {{ {light.Diffuse[0]}, {light.Diffuse[1]}, {light.Diffuse[2]}, {light.Diffuse[3]} }});");
				code.AppendLine($"{_indent}glLightfv(GL_LIGHT{i}, GL_SPECULAR, new[] {{ {light.Specular[0]}, {light.Specular[1]}, {light.Specular[2]}, {light.Specular[3]} }});");
				code.AppendLine($"{_indent}glLightfv(GL_LIGHT{i}, GL_SPOT_EXPONENT, {light.SpotlightExponent});");
				code.AppendLine($"{_indent}glLightfv(GL_LIGHT{i}, GL_SPOT_CUTOFF, {light.SpotlightCutoff});");
				code.AppendLine($"{_indent}glLightfv(GL_LIGHT{i}, GL_CONSTANT_ATTENUATION, {light.ConstantAttenuation});");
				code.AppendLine($"{_indent}glLightfv(GL_LIGHT{i}, GL_LINEAR_ATTENUATION, {light.LinearAttenuation});");
				code.AppendLine($"{_indent}glLightfv(GL_LIGHT{i}, GL_QUADRATIC_ATTENUATION, {light.QuadraticAttenuation});");
				code.AppendLine();
				i++;
			}
		}

		public void PositionLights(StringBuilder code, IList<LightModel> lights)
		{
			if (lights.Any() == false) return;

			code.AppendLine($"{_indent}//  Position Lights");

			var i = 0;
			foreach (var light in lights)
			{
				code.AppendLine($"{_indent}glLightfv(GL_LIGHT{i}, GL_POSITION, new[] {{ {light.Position[0]}, {light.Position[1]}, {light.Position[2]}, {light.Position[3]} }});");
				code.AppendLine($"{_indent}glLightfv(GL_LIGHT{i}, GL_SPOT_DIRECTION, new[] {{ {light.SpotlightDirection[0]}, {light.SpotlightDirection[1]}, {light.SpotlightDirection[2]} }});");
				code.AppendLine();
				i++;
			}
		}

		private void SetTransformations(StringBuilder code, IList<TransformationModel> transformations, bool isFixedCoordinateSystem = true)
		{
			if (transformations.Any() == false) return;

			code.AppendLine($"{_indent}//  Set Transformations");

			transformations = isFixedCoordinateSystem ? transformations.Reverse().ToList() : transformations;
			foreach (var transformation in transformations)
			{
				switch (transformation.Transform)
				{
					case Transform.Translate:
						code.AppendLine($"{_indent}glTranslate3f({transformation.X:F}f, {transformation.Y:F}f, {transformation.Z:F}f);");
						break;
					case Transform.Rotate:
						code.AppendLine($"{_indent}glRotate3f({transformation.X:F}f, {transformation.Y:F}f, {transformation.Z:F}f);");
						break;
					case Transform.Scale:
						code.AppendLine($"{_indent}glScale3f({transformation.X:F}f, {transformation.Y:F}f, {transformation.Z:F}f);");
						break;
					default:
						throw new InvalidEnumArgumentException();
				}
			}
		}
	}
}
