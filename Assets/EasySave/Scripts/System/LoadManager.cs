using NEG.Plugins.EasySave.Data;
using NEG.Plugins.EasySave.Data.Path;
using Newtonsoft.Json;
using System;

namespace NEG.Plugins.EasySave.System
{
    public sealed class LoadManager
    {
		public LoadManager(ApplicationPath _applicationPath)
		{
			ApplicationPath = _applicationPath;
		}

		public static LoadManager Instance { get; } = new(ApplicationPath.Instance);

		public ApplicationPath ApplicationPath { get; }

		public File DeSerializeFile<File>(string _file)
			where File : ISaveable
		{
			return JsonConvert.DeserializeObject<File>(_file);
		}
	}
}
