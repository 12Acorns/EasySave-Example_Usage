using System.Collections.Generic;
using System;

namespace NEG.Plugins.EasySave.Utility
{
    public static class FileUtility
    {
		private const int compareLength = 3;

		private static readonly HashSet<string> invalidNameSet = new()
		{
			"CON",
			"NUL",
			"PRN",
			"AUX",
			"LPT",
		};
		private static readonly HashSet<char> invalidCharsSet = new()
		{
			'<',
			'>',
			'.',
			'\"',
			'\'',
			'|',
			'?',
			'*',
			'\0',
			'\\',
			'/',
			':',
		};

		public static bool IsFileNameValid(string _fileName)
		{
			return TryValidateFile(_fileName);
		}
		private static bool TryValidateFile(string _file)
		{
			var _span = _file.AsSpan();
			return StringExistAndValidLength(_file)
				&& IsValidChars(_span)
				&& IsValidName(_span);
		}
		private static bool StringExistAndValidLength(string _file) =>
			!(string.IsNullOrEmpty(_file) && string.IsNullOrWhiteSpace(_file)) && _file.Length > 0;
		private static bool IsValidName(ReadOnlySpan<char> _path)
		{
			if(_path.Length < compareLength)
			{
				return true;
			}
			if(_path.Length == 3)
			{
				var _string = new string(_path).ToUpperInvariant();

				return !invalidNameSet.Contains(_string);
			}

			for(int i = 0; i < _path.Length - compareLength; i++)
			{
				var _slice = _path.Slice(i, 3);

				var _string = new string(_slice).ToUpperInvariant();

				if(!invalidNameSet.Contains(_string))
				{
					continue;
				}
				return false;
			}
			return true;
		}
		private static bool IsValidChars(ReadOnlySpan<char> _path)
		{
			for(int i = 0; i < _path.Length; i++)
			{
				if(!invalidCharsSet.Contains(_path[i]))
				{
					continue;
				}
				return false;
			}
			return true;
		}
	}
}
