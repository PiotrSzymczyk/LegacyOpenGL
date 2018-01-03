using System.Linq;
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

		private Geometry Geometry => OpenGlSceneDefinitionService.Scene.Geometry;
		private Material[] Materials => OpenGlSceneDefinitionService.Scene.Materials;
		private int SelectedMaterialIndex => OpenGlSceneDefinitionService.Scene.SelectedMaterial;
		private Texture Texture => OpenGlSceneDefinitionService.Scene.Texture;

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
				FeaturesService.SetLights(gl, SettingsService.Lights);
			}

			// 3 - Modeling transformation
			FeaturesService.SetTransformations(gl, SettingsService.Transformations);

			if (Materials.Any())
			{
				ApplyMaterial(gl, Materials[SelectedMaterialIndex]);
			}

			SetTexture(gl);

			foreach (var face in Geometry.Faces)
			{
				var faceMode = GetFaceDrawingMode(face.IndexCount);

				gl.Begin(faceMode);

				foreach (var index in face.Indices)
				{
					if (Geometry.HasNormals)
					{
						var normal = Geometry.Normals[index.Normal];
						gl.Normal(normal.X, normal.Y, normal.Z);
					}

					if (Geometry.HasTextures)
					{
						var texCoord = Geometry.TextureCoordinates[index.Texture];
						gl.TexCoord(texCoord.X, texCoord.Y, texCoord.W);
					}
					
					var vertex = Geometry.Vertices[index.Vertex];
					gl.Vertex(vertex.X, vertex.Y, vertex.Z);
				}

				gl.End();
			}

			gl.Flush();
		}

		private void SetTexture(OpenGL gl)
		{
			var textures = new uint[1];
			gl.GenTextures(1, textures);
			gl.BindTexture(OpenGL.GL_TEXTURE_2D, textures.First());
			gl.PixelStore(OpenGL.GL_UNPACK_ALIGNMENT, 1);
			gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
			gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);
			gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
			gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
			gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_MODULATE);

			gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, OpenGL.GL_RGB, Texture.Width, Texture.Height, 0, OpenGL.GL_RGB, OpenGL.GL_UNSIGNED_BYTE, Texture.ImageData);
		}

		public void Resize(OpenGL gl)
		{
			// 0 - Projection transformation
			gl.MatrixMode(OpenGL.GL_PROJECTION);
			gl.LoadIdentity();

			// Perform a perspective transformation
			var xyRatio = (float)gl.RenderContextProvider.Width / gl.RenderContextProvider.Height;
			if (SettingsService.ProjectionTransformation.SelectedIndex == 0)
			{
				gl.Perspective(75.0f, xyRatio, 0.1f, 100.0f);
			}
			else
			{
				gl.Ortho(-2 * xyRatio, 2 * xyRatio, -2, 2, 0.1, 100);
			}

			// Re-load the modelview matrix.
			gl.MatrixMode(OpenGL.GL_MODELVIEW);
		}

		private void ApplyMaterial(OpenGL gl, Material mtl)
		{
			var color = mtl.AmbientColor != null
				? new[] {mtl.AmbientColor.R, mtl.AmbientColor.G, mtl.AmbientColor.B, mtl.AmbientColor.A}
				: new[] {0.2f, 0.2f, 0.2f, 1.0f};
			gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT, color);

			color = mtl.DiffuseColor != null
				? new[] {mtl.DiffuseColor.R, mtl.DiffuseColor.G, mtl.DiffuseColor.B, mtl.DiffuseColor.A}
				: new[] {0.8f, 0.8f, 0.8f, 1.0f};
			gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_DIFFUSE, color);

			color = mtl.SpecularColor != null
				? new[] {mtl.SpecularColor.R, mtl.SpecularColor.G, mtl.SpecularColor.B, mtl.SpecularColor.A}
				: new[] {0.0f, 0.0f, 0.0f, 1.0f};
			gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_SPECULAR, color);

			gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_SHININESS, 0);
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
