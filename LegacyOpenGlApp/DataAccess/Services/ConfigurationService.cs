using System;
using System.Collections.Generic;
using System.IO;
using LegacyOpenGlApp.DataAccess.Models;
using Newtonsoft.Json.Linq;

namespace LegacyOpenGlApp.DataAccess.Services
{
	public static class ConfigurationService
	{
		private const string JsonConfigFilePath = "C:\\Code\\LegacyOpenGL\\LegacyOpenGlApp\\config.json";
		private static JObject _config;
		private static readonly JObject Config = _config ?? (_config = JObject.Parse(File.ReadAllText(JsonConfigFilePath)));

		public static T LoadSection<T>(string section)
		{
			return Config.TryGetValue(section, StringComparison.OrdinalIgnoreCase, out var sectionContent)
				? sectionContent.ToObject<T>()
				: Activator.CreateInstance<T>();
		}

		public static readonly IList<ToggleModel> OpenGlToggles = LoadSection<List<ToggleModel>>("OpenGlToggles"); 

		public static readonly IList<TransformationModel> Transformations = LoadSection<List<TransformationModel>>("Transformations");

		public static readonly IList<LightModel> Lights = LoadSection<List<LightModel>>("Lights");

		public static string DefaultGeometryPath = Path.GetFullPath(LoadSection<string>("DefaultGeometryPath"));

		public static string DefaultMaterialsPath = Path.GetFullPath(LoadSection<string>("DefaultMaterialsPath"));

		public static string DefaultTexturePath = Path.GetFullPath(LoadSection<string>("DefaultTexturePath"));

		public static readonly CameraModel Camera = LoadSection<CameraModel> ("Camera");
	}
}
