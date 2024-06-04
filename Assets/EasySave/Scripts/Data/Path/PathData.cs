using NEG.Plugins.EasySave.Utility;
using System.IO;

namespace NEG.Plugins.EasySave.Data.Path
{
    public sealed class PathData
    {
        /// <param name="_path">The path to the save location directory, 
        /// IE
        /// <para></para>
        /// C:\\PathToUser\\USER\\Saves
        /// </param>
        /// <param name="_saveDirectoryName">Name for files to be created in under root</param>
        /// <exception cref="InvalidDataException"></exception>
        public PathData(string _path, string _saveDirectoryName)
        {
            if(!DirectoryUtility.TryCreateDirectory(_path, out var _directory))
            {
                throw new InvalidDataException("Invalid path");
            }
            if(!FileUtility.IsFileNameValid(_saveDirectoryName))
            {
				throw new InvalidDataException("Invalid file name");
			}

			Root = _directory;
            SaveDirectory = _saveDirectoryName;
            FullPath = Root.CreateSubdirectory(SaveDirectory);
		}

        public DirectoryInfo FullPath;
        public DirectoryInfo Root { get; }
        public string SaveDirectory { get; }
    }
}
