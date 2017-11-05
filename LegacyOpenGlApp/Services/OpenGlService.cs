using System.Linq;
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
			int i = 1;

			FeaturesService.SetToggles(gl, SettingsService.Toggles);

			//  Clear the color and depth buffers.
			gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

			//  Reset the modelview matrix.
			gl.LoadIdentity();

			FeaturesService.SetTransformations(gl, SettingsService.Transformations);

			FeaturesService.SetLights(gl, SettingsService.Lights);

			gl.Begin(OpenGL.GL_QUADS);
			
			var meshes = Scene.Meshes;
			foreach (var mesh in meshes)
			{
				var faces = mesh.Faces;

				foreach (var face in faces)
				{
					var vertices = face.Indices.Select(indice => mesh.Vertices[indice]);
					gl.Color(1-i/15f, 1-i/10f, 1f-i/15f);

					foreach (var vertex in vertices)
					{
						gl.Vertex(vertex.X, vertex.Y, vertex.Z);
					}
					i++;
				}
			}

			gl.End();
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
	}
}
