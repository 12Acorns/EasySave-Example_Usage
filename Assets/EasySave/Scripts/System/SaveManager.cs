using NEG.Plugins.EasySave.Data.Path;

namespace NEG.Plugins.EasySave.System
{
    public sealed class SaveManager
    {
        public SaveManager(ApplicationPath _applicationPath)
        {
            ApplicationPath = _applicationPath;
		}

        public static SaveManager Instance { get; } = new(ApplicationPath.Instance);

        public ApplicationPath ApplicationPath { get; }
	}
}
