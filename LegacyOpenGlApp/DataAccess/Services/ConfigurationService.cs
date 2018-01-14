using System;
using System.Collections.Generic;
using System.IO;
using LegacyOpenGlApp.DataAccess.Models;
using Newtonsoft.Json.Linq;
using Unity;

namespace LegacyOpenGlApp.DataAccess.Services
{
	public class ConfigurationService
	{
		[Dependency]
		public string JsonConfigFilePath { get; set; }

		private JObject _config;
		private JObject Config => _config ?? (_config = JObject.Parse(File.ReadAllText(JsonConfigFilePath)));

		public T LoadSection<T>(string section)
		{
			return Config.TryGetValue(section, StringComparison.OrdinalIgnoreCase, out var sectionContent)
				? sectionContent.ToObject<T>()
				: Activator.CreateInstance<T>();
		}

		public IList<ToggleModel> OpenGlToggles => LoadSection<List<ToggleModel>>("OpenGlToggles"); 

		public IList<TransformationModel> Transformations => LoadSection<List<TransformationModel>>("Transformations");

		public IList<LightModel> Lights => LoadSection<List<LightModel>>("Lights");

		public string DefaultGeometryPath => Path.GetFullPath(LoadSection<string>("DefaultObjFilePath"));

		public string DefaultMaterialsPath => Path.GetFullPath(LoadSection<string>("DefaultMtlFilePath"));

		public string DefaultTexturePath => Path.GetFullPath(LoadSection<string>("DefaultTexturePath"));

		public CameraModel Camera => LoadSection<CameraModel> ("Camera");
	}
}
