using Assimp;
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

		public void Draw(OpenGL gl)
		{
			FeaturesService.SetToggles(gl, SettingsService.Toggles);

			FeaturesService.SetLights(gl, SettingsService.Lights);

			//  Clear the color and depth buffers.
			gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

			//  Reset the modelview matrix.
			gl.LoadIdentity();

			FeaturesService.SetTransformations(gl, SettingsService.Transformations);
			
			var meshes = Scene.Meshes;
			foreach (var mesh in meshes)
			{
				if (Scene.HasMaterials && mesh.MaterialIndex < Scene.MaterialCount)
				{
					ApplyMaterial(gl, Scene.Materials[mesh.MaterialIndex]);
				}

				foreach (var face in mesh.Faces)
				{
					var faceMode = GetFaceDrawingMode(face.IndexCount);

					gl.Begin(faceMode);

					foreach (var index in face.Indices)
					{
						if (mesh.HasVertexColors(0))
						{
							var color = mesh.VertexColorChannels[0][index];
							gl.Color(color.R, color.G, color.B, color.A);
						}

						if (mesh.HasNormals)
						{
							var normal = mesh.Normals[index];
							gl.Normal(normal.X, normal.Y, normal.Z);
						}

						var vertex = mesh.Vertices[index];
						gl.Vertex(vertex.X, vertex.Y, vertex.Z);
					}

					gl.End();
				}
			}

			gl.Flush();
		}

		public void Resize(OpenGL gl)
		{
			// Load and clear the projection matrix.
			gl.MatrixMode(OpenGL.GL_PROJECTION);
			gl.LoadIdentity();

			// Perform a perspective transformation
			gl.Perspective(45.0f, (float) gl.RenderContextProvider.Width / (float) gl.RenderContextProvider.Height, 0.1f, 100.0f);

			// Load the modelview.
			gl.MatrixMode(OpenGL.GL_MODELVIEW);
		}

		private void ApplyMaterial(OpenGL gl, Material mtl)
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
		}

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
