using LegacyOpenGlApp.DataAccess.Models.SceneLoading;
using SharpGL;
using Unity;

namespace LegacyOpenGlApp.Services
{
	public class OpenGlService
	{
		[Dependency]
		public SceneDefinitionServiceModel OpenGlSceneDefinitionService { get; set; }

		[Dependency]
		public SceneSettingsServiceModel SettingsService { get; set; }

		private Scene Scene => OpenGlSceneDefinitionService.Scene;
		private int angle = 0;

		public void Draw(OpenGL gl)
		{
			FeaturesService.SetToggles(gl, SettingsService.Toggles);

			//  Clear the color and depth buffers.
			gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

			//  Reset the modelview matrix.
			gl.LoadIdentity();

			// 1 - Vieweing transformtion
			gl.LookAt(
				SettingsService.Camera.PositionX, SettingsService.Camera.PositionY, SettingsService.Camera.PositionZ,
				SettingsService.Camera.AimX, SettingsService.Camera.AimY, SettingsService.Camera.AimZ,
				SettingsService.Camera.UpX, SettingsService.Camera.UpY, SettingsService.Camera.UpZ
			);

			// 2 - Set lights
			if (gl.IsEnabled(OpenGL.GL_LIGHTING))
			{
				gl.PushMatrix();
				gl.Translate(0, 0, -5);
				gl.Rotate(angle++, 0, 10, 0);
				FeaturesService.SetLights(gl, SettingsService.Lights);
				gl.PopMatrix();
			}

			// 3 - Modeling transformation
			FeaturesService.SetTransformations(gl, SettingsService.Transformations);


			int i = 0;

			// TODO: https://trello.com/c/nHW9FoIa
			//	if (Scene.HasMaterials)
			//	{
			//		ApplyMaterial(gl, Scene.Materials[mesh.MaterialIndex]);
			//	}

			foreach (var face in Scene.Faces)
			{
				var faceMode = GetFaceDrawingMode(face.IndexCount);

				gl.Begin(faceMode);

				foreach (var index in face.Indices)
				{
					if (Scene.HasNormals)
					{
						var normal = Scene.Normals[(int)index.NormalIndex];
						gl.Normal(normal.X, normal.Y, normal.Z);
					}

					i++;
					var vertex = Scene.Vertices[index.VertexIndex];
					// TODO: https://trello.com/c/Mhj9JaxG
					// gl.Color((16 + i * 1f % 128) / 128, (16 + i * 2f % 128) / 128, (16 + i * 4f % 128) / 128, 1);
					gl.Vertex(vertex.X, vertex.Y, vertex.Z);
				}

				gl.End();
			}

			gl.Flush();
		}

		public void Resize(OpenGL gl)
		{
			// 0 - Projection transformation
			gl.MatrixMode(OpenGL.GL_PROJECTION);
			gl.LoadIdentity();

			// Perform a perspective transformation
			var xyRatio = (float)gl.RenderContextProvider.Width / gl.RenderContextProvider.Height;
			gl.Perspective(75.0f, xyRatio, 0.1f, 100.0f);
			// gl.Ortho(-5 * xyRatio, 5 * xyRatio, -5, 5, 0, 100);
			// gl.ShadeModel(ShadeModel.Smooth);

			// Re-load the modelview matrix.
			gl.MatrixMode(OpenGL.GL_MODELVIEW);
		}

		/*private void ApplyMaterial(OpenGL gl, Material mtl)
		{
			float[] color;

			color = mtl.HasColorDiffuse
				? new[] { mtl.ColorDiffuse.R, mtl.ColorDiffuse.G, mtl.ColorDiffuse.B, mtl.ColorDiffuse.A }
				: new[] { 0.8f, 0.8f, 0.8f, 1.0f };
			gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_DIFFUSE, color);

			color = mtl.HasColorSpecular
				? new[] { mtl.ColorSpecular.R, mtl.ColorSpecular.G, mtl.ColorSpecular.B, mtl.ColorSpecular.A }
				: new[] { 0.0f, 0.0f, 0.0f, 1.0f };
			gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_SPECULAR, color);

			color = mtl.HasColorAmbient
				? new[] { mtl.ColorAmbient.R, mtl.ColorAmbient.G, mtl.ColorAmbient.B, mtl.ColorAmbient.A }
				: new[] { 0.2f, 0.2f, 0.2f, 1.0f };
			gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT, color);

			color = mtl.HasColorEmissive
				? new[] { mtl.ColorEmissive.R, mtl.ColorEmissive.G, mtl.ColorEmissive.B, mtl.ColorEmissive.A }
				: new[] { 0.0f, 0.0f, 0.0f, 1.0f };
			gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_EMISSION, color);


			if (mtl.HasShininess)
			{
				if (mtl.HasShininessStrength)
				{
					gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_SHININESS, mtl.Shininess * mtl.ShininessStrength);
				}
				else
				{
					gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_SHININESS, mtl.Shininess);
				}
			}
			else
			{
				gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_SHININESS, 0);
				gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_SPECULAR, new[] { 0f, 0f, 0f, 0f });
			}

			gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, mtl.HasWireFrame && mtl.IsWireFrameEnabled ? OpenGL.GL_LINE : OpenGL.GL_FILL);

			if (mtl.IsTwoSided && mtl.HasTwoSided)
			{
				gl.Disable(OpenGL.GL_CULL_FACE);
			}
			else
			{
				gl.Enable(OpenGL.GL_CULL_FACE);
			}
		}*/

		private static uint GetFaceDrawingMode(int faceIndexCount)
		{
			switch (faceIndexCount)
			{
				case 1: return OpenGL.GL_POINTS;
				case 2: return OpenGL.GL_LINES;
				case 3: return OpenGL.GL_TRIANGLES;
				case 4: return OpenGL.GL_QUADS;
				default: return OpenGL.GL_POLYGON_MODE;
			}
		}
	}
}
