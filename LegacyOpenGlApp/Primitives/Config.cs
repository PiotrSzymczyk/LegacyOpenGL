using System;
using System.Collections.Generic;
using System.IO;
using LegacyOpenGlApp.DataAccess.Models;
using Newtonsoft.Json;

namespace LegacyOpenGlApp.Primitives
{
	public static class Config
	{
		private const string JsonConfigFilePath = "C:\\Code\\LegacyOpenGL\\LegacyOpenGlApp\\config.json";
		private static dynamic _conf;
		private static readonly dynamic configuration = _conf ?? (_conf = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(JsonConfigFilePath)));


		public static T LoadSection<T>(string section)
		{
			return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(configuration[section]), typeof(T));
		}

		public static readonly IList<ToggleModel> OpenGlToggles = LoadSection<List<ToggleModel>>("OpenGlToggles"); 

		public static readonly IList<TransformationModel> Transformations = LoadSection<List<TransformationModel>>("Transformations");

		public static readonly IList<LightModel> Lights = LoadSection<List<LightModel>>("Lights");

		public static string DefaultScenePath = LoadSection<string>("DefaultScenePath");

		public static readonly CameraModel Camera = LoadSection<CameraModel> ("Camera");
	}
}
