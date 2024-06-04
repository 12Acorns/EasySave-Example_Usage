using NEG.Plugins.EasySave.Data.Path;

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

	}
}
