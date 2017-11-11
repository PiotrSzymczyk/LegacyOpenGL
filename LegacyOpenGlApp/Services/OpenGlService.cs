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

			//  Clear the color and depth buffers.
			gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

			//  Reset the modelview matrix.
			gl.LoadIdentity();

			FeaturesService.SetTransformations(gl, SettingsService.Transformations);

			FeaturesService.SetLights(gl, SettingsService.Lights);
			
			var meshes = Scene.Meshes;
			foreach (var mesh in meshes)
			{
				//TODO: apply material

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
