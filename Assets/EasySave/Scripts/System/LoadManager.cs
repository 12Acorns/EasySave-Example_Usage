using NEG.Plugins.EasySave.Data;
using NEG.Plugins.EasySave.Data.Path;
using Newtonsoft.Json;
using System;

namespace NEG.Plugins.EasySave.System
{
    public sealed class LoadManager
    {
		public LoadManager(ApplicationPath _applicationPath, Formatting _formatOption = Formatting.None)
		{
			ApplicationPath = _applicationPath;
			formatOption = _formatOption;
		}

		public static LoadManager Instance { get; } = new(ApplicationPath.Instance);

		public ApplicationPath ApplicationPath { get; }
		private readonly Formatting formatOption;

		public ReadOnlySpan<char> SerializeFile<File>(File _file)
			where File : ISaveable
		{
			return JsonConvert.SerializeObject(_file, formatOption);
		}
	}
}
