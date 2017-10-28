using LegacyOpenGlApp.Models;
using SharpGL;

namespace LegacyOpenGlApp.Services
{
    public class OpenGlService
	{
		float rotatePyramid;

		public void Draw(OpenGL gl, OpenGlSettingsModel settings)
		{
			FeaturesService.SetToggles(gl, settings.Toggles);

			//  Clear the color and depth buffers.
			gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

			//  Reset the modelview matrix.
			gl.LoadIdentity();

			//  Move the geometry into a fairly central position.
			gl.Translate(0.0f, 0.0f, -6.0f);

			//  Draw a pyramid. First, rotate the modelview matrix.
			gl.Rotate(rotatePyramid, 0.0f, 1.0f, 0.0f);

			//  Start drawing triangles.
			gl.Begin(OpenGL.GL_TRIANGLES);

			gl.Color(1.0f, 0.0f, 0.0f);
			gl.Vertex(0.0f, 1.0f, 0.0f);
			gl.Color(0.0f, 1.0f, 0.0f);
			gl.Vertex(-1.0f, -1.0f, 1.0f);
			gl.Color(0.0f, 0.0f, 1.0f);
			gl.Vertex(1.0f, -1.0f, 1.0f);

			gl.Color(1.0f, 0.0f, 0.0f);
			gl.Vertex(0.0f, 1.0f, 0.0f);
			gl.Color(0.0f, 0.0f, 1.0f);
			gl.Vertex(1.0f, -1.0f, 1.0f);
			gl.Color(0.0f, 1.0f, 0.0f);
			gl.Vertex(1.0f, -1.0f, -1.0f);

			gl.Color(1.0f, 0.0f, 0.0f);
			gl.Vertex(0.0f, 1.0f, 0.0f);
			gl.Color(0.0f, 1.0f, 0.0f);
			gl.Vertex(1.0f, -1.0f, -1.0f);
			gl.Color(0.0f, 0.0f, 1.0f);
			gl.Vertex(-1.0f, -1.0f, -1.0f);

			gl.Color(1.0f, 0.0f, 0.0f);
			gl.Vertex(0.0f, 1.0f, 0.0f);
			gl.Color(0.0f, 0.0f, 1.0f);
			gl.Vertex(-1.0f, -1.0f, -1.0f);
			gl.Color(0.0f, 1.0f, 0.0f);
			gl.Vertex(-1.0f, -1.0f, 1.0f);

			gl.End();
			gl.Flush();

			//  Rotate the geometry a bit.
			rotatePyramid += 3.0f;
		}

		public void Resize(OpenGL gl)
		{
			// Load and clear the projection matrix.
			gl.MatrixMode(OpenGL.GL_PROJECTION);
			gl.LoadIdentity();

			// Perform a perspective transformation
			gl.Perspective(45.0f, (float)gl.RenderContextProvider.Width /
								  (float)gl.RenderContextProvider.Height,
				0.1f, 100.0f);

			// Load the modelview.
			gl.MatrixMode(OpenGL.GL_MODELVIEW);
		}
	}
}
