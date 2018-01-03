﻿using JeremyAnsel.Media.WavefrontObj;

namespace LegacyOpenGlApp.DataAccess.Models.SceneLoading
{
	public class Color
	{
		public Color(ObjMaterialColor objMaterialColor, float dissolveFactor = 0)
		{

			R = objMaterialColor.Color.X;
			G = objMaterialColor.Color.Y;
			B = objMaterialColor.Color.Z;
			A = 1 - dissolveFactor;
		}

		public Color(float r, float g, float b, float a = 1)
		{
			R = r;
			G = g;
			B = b;
			A = a;
		}

		public float R { get; set; }

		public float G { get; set; }

		public float B { get; set; }

		public float A { get; set; }
	}
}