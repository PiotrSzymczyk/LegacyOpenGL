﻿using System.Linq;
using LegacyOpenGlApp.DataAccess.Models.SceneLoading;
using SharpGL;
using Unity;

namespace LegacyOpenGlApp.Services
{
	public class OpenGlService
	{
		[Dependency]
		public SceneServiceModel OpenGlSceneService { get; set; }

		[Dependency]
		public OpenGLSettingsServiceModel SettingsService { get; set; }

		private Geometry Geometry => OpenGlSceneService.Scene.Geometry;
		private Material[] Materials => OpenGlSceneService.Scene.Materials;
		private int SelectedMaterialIndex => OpenGlSceneService.Scene.SelectedMaterial;
		private Texture Texture => OpenGlSceneService.Scene.Texture;

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
			gl.DeleteTextures(1, new []{ OpenGL.GL_TEXTURE0 });

			gl.Flush();
		}

		private void SetTexture(OpenGL gl)
		{
			gl.BindTexture(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE0);
			gl.PixelStore(OpenGL.GL_UNPACK_ALIGNMENT, 1);
			gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
			gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);
			gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
			gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);

			gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, (int)SettingsService.TextureEnvMode.Mode);

			gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, OpenGL.GL_RGB, Texture.Width, Texture.Height, 0, OpenGL.GL_RGB, OpenGL.GL_UNSIGNED_BYTE, Texture.ImageData);
		}

		public void Resize(OpenGL gl)
		{
			// 0 - Projection transformation
			gl.MatrixMode(OpenGL.GL_PROJECTION);
			gl.LoadIdentity();

			// Perform a perspective transformation
			if (SettingsService.ProjectionTransformation.SelectedIndex == 0)
			{
				gl.Perspective(
					SettingsService.ProjectionTransformation.Perspective.Fovy,
					SettingsService.ProjectionTransformation.Perspective.Aspect,
					SettingsService.ProjectionTransformation.Perspective.Near,
					SettingsService.ProjectionTransformation.Perspective.Far);
			}
			else
			{
				gl.Ortho(
					SettingsService.ProjectionTransformation.Ortographic.Left,
					SettingsService.ProjectionTransformation.Ortographic.Right,
					SettingsService.ProjectionTransformation.Ortographic.Bottom,
					SettingsService.ProjectionTransformation.Ortographic.Top,
					SettingsService.ProjectionTransformation.Ortographic.Near,
					SettingsService.ProjectionTransformation.Ortographic.Far
					);
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

			color = mtl.EmissiveColor != null
				? new[] { mtl.EmissiveColor.R, mtl.EmissiveColor.G, mtl.EmissiveColor.B, mtl.EmissiveColor.A }
				: new[] { 0.0f, 0.0f, 0.0f, 1.0f };
			gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_EMISSION, color);

			gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_SHININESS, mtl.SpecularExponent);
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
