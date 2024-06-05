using NEG.Plugins.EasySave.Data.Path;
using NEG.Plugins.EasySave.Utility;
using NEG.Plugins.EasySave.Data;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using UnityEngine;
using System.IO;
using System;

namespace NEG.Plugins.EasySave.System
{
    public sealed class SaveManager
    {
        private const string json = ".json";

        public SaveManager(ApplicationPath _applicationPath, Formatting _formatOption = Formatting.None,
            JsonSerializerSettings _serializeSettings = null)
        {
            ApplicationPath = _applicationPath;
            formatOption = _formatOption;
            serializeSettings = _serializeSettings;
        }

        public static SaveManager Instance { get; } = new(ApplicationPath.Instance);

        public ApplicationPath ApplicationPath { get; }

        private readonly JsonSerializerSettings serializeSettings;
        private readonly Formatting formatOption;

		public async Task<SaveOutputResponce> TrySaveFileAsync<File>
            (File _file, string _subDirectories, string _fileName)
	        where File : ISaveable
		{
            var _directoryInfo = ArgumentsValid(_file, _subDirectories, _fileName);
            if(_directoryInfo == null)
			{
				return SaveOutputResponce.InvalidArguments;
			}

			return await TrySaveFileImplAsync(_file, _directoryInfo, _fileName).ConfigureAwait(false);
		}
		public SaveOutputResponce TrySaveFile<File>(File _file, string _subDirectories, string _fileName)
            where File : ISaveable
        {
            var _directoryInfo = ArgumentsValid(_file, _subDirectories, _fileName);
            if(_directoryInfo == null)
            {
                return SaveOutputResponce.InvalidArguments;
            }

            return TrySaveFileImpl(_file, _directoryInfo, _fileName);
        }
        private DirectoryInfo? ArgumentsValid<File>
            (File _file, string _subDirectories, string _fileName)
            where File : ISaveable
        {
            if(_file == null)
            {
                return null;
            }

			var _fullPath = Path.Combine(ApplicationPath.GetFullPath().FullName, _subDirectories);
            if(!DirectoryUtility.TryCreateDirectory(_fullPath, out DirectoryInfo _fullPathDir))
            {
                return null;
            }
            if(!FileUtility.IsFileNameValid(_fileName))
            {
                return null;
            }
            return _fullPathDir;
        }
        private async ValueTask<SaveOutputResponce> TrySaveFileImplAsync<File>
            (File _file, DirectoryInfo _fullPath, string _fileName)
	        where File : ISaveable
		{
			if(!CombinePaths(_fullPath, _fileName, out var _savePath))
			{
				return SaveOutputResponce.PathConcatenationFailure;
			}

            try
            {
                using var _fileStream =
                    new FileStream(
                        _savePath,
                        FileMode.Create, FileAccess.Write,
                        FileShare.None, 4096, true);

                var _serializedFile = SerializeFile(_file).ToString();

                try
                {
                    return await SaveFileAsync(_fileStream, _serializedFile).ConfigureAwait(false);
                }
                catch(Exception _ex)
                {
					Debug.LogError(_ex);
                    return SaveOutputResponce.FileIOFailure;
				}
            }
            catch(Exception _ex)
            {
				Debug.LogError(_ex);
                return SaveOutputResponce.BadStream;
			}
		}
		private SaveOutputResponce TrySaveFileImpl<File>
            (File _file, DirectoryInfo _fullPath, string _fileName)
            where File : ISaveable
        {
            if(!CombinePaths(_fullPath, _fileName, out var _savePath))
            {
                return SaveOutputResponce.PathConcatenationFailure;
			}

            try
            {
				using var _fileStream =
	                new FileStream(
		                _savePath,
		                FileMode.Create, FileAccess.Write,
		                FileShare.None, 4096, false);

				var _serializedFile = SerializeFile(_file);

                try
                {
				    return SaveFile(_fileStream, _serializedFile);
                }
                catch(Exception _ex)
                {
                    Debug.LogError(_ex);
					return SaveOutputResponce.FileIOFailure;
				}
			}
            catch(Exception _ex)
            {
				Debug.LogError(_ex);
				return SaveOutputResponce.BadStream;
            }
        }
		private async Task<SaveOutputResponce> SaveFileAsync(FileStream _stream, string _serializedFile)
		{
			var _bytes = Encoding.ASCII.GetBytes(_serializedFile);

			try
			{
				await _stream.WriteAsync(_bytes).ConfigureAwait(false);
			}
			catch(Exception _ex)
			{
				Debug.LogError(_ex);
				return SaveOutputResponce.FileIOFailure;
			}
			finally
			{
				await _stream.DisposeAsync();
			}
			return SaveOutputResponce.Success;
		}
		private SaveOutputResponce SaveFile(FileStream _stream, ReadOnlySpan<char> _serializedFile)
        {
            var _length = Encoding.ASCII.GetByteCount(_serializedFile);
            Span<byte> _bytes = new byte[_length];
			Encoding.ASCII.GetBytes(_serializedFile, _bytes);

			try
			{
				_stream.Write(_bytes);
			}
			catch(Exception _ex)
			{
				Debug.LogError(_ex);
				return SaveOutputResponce.FileIOFailure;
			}
            finally
            {
                _stream.Dispose();
            }
			return SaveOutputResponce.Success;
		}
		private bool CombinePaths
            (DirectoryInfo _fullPathDir, string _fileName, out string _fullPath)
        {
            _fullPath = Path.Combine(_fullPathDir.FullName, _fileName + json);
			return true;
        }

		private ReadOnlySpan<char> SerializeFile<File>(File _file)
            where File : ISaveable
        {
            if(serializeSettings == null)
            {
                return JsonConvert.SerializeObject(_file, formatOption);
            }
            return JsonConvert.SerializeObject(_file, formatOption, serializeSettings);
        }
    }
}
