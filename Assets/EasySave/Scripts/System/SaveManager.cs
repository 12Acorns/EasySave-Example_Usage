using NEG.Plugins.EasySave.Data.Path;
using NEG.Plugins.EasySave.Data;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Buffers;
using System;

namespace NEG.Plugins.EasySave.System
{
    public sealed class SaveManager
    {
        public SaveManager(ApplicationPath _applicationPath, Formatting _formatOption = Formatting.None)
        {
            ApplicationPath = _applicationPath;
            formatOption = _formatOption;
		}

        public static SaveManager Instance { get; } = new(ApplicationPath.Instance);

        public ApplicationPath ApplicationPath { get; }
        private readonly Formatting formatOption;


        public ReadOnlySpan<char> SerializeFile<File>(File _file)
            where File : ISaveable
        {
            return JsonConvert.SerializeObject(_file, formatOption);
		}
	}
}
