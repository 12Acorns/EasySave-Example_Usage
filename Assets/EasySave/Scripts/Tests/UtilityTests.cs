using NEG.Plugins.EasySave.Utility;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.IO;

namespace NEG.Plugins.EasySave.Tests.Editor
{
    public sealed class UtilityTests
    {
        private static readonly string userPath = Application.persistentDataPath;

		private readonly IReadOnlyList<string> testPassFiles = new List<string>()
		{
			"File",
			"lifi",
			"Extra Cool Name (REAL)"
		};
		private readonly IReadOnlyList<string> testFailFiles = new List<string>()
		{
			$"{userPath}\\Doijhdfsaoipoh\\DeeperDir\0",
			$"{userPath}\\Doijhdfsaoipoh\\..>",
			"C::",
			"NUL",
			"NULL",
			"CON",
			"AUX",
			"FileName.json",
			"Wee::"
		};

		private readonly IReadOnlyList<string> testPassDirectories = new List<string>() 
        {
            $"{userPath}\\TestDir",
            $"{userPath}\\Dir",
            $"{userPath}\\Dir\\DeeperDir",
            $"{userPath}\\Doijhdfsaoipoh\\DeeperDir\\",
        };
		private readonly IReadOnlyList<string> testFailDirectories = new List<string>()
		{
			$"{userPath}\\Doijhdfsaoipoh\\DeeperDir\0",
			$"{userPath}\\Doijhdfsaoipoh\\..>",
			"Dingleberry",
			"C::",
            "NUL",
            "NULL",
            "CON",
            "AUX",
        };

		[Test]
		public void ValidFileValidationTest()
		{
			foreach(var _test in testPassFiles)
			{
				Assert.IsTrue(FileUtility.IsFileNameValid(_test));
			}
		}
		[Test]
		public void InvalidFileValidationTest()
		{
			foreach(var _test in testFailFiles)
			{
				Assert.IsFalse(FileUtility.IsFileNameValid(_test));
			}
		}

		[Test]
        public void ValidDirectoryValidationTest()
        {
            foreach(var _test in testPassDirectories)
            {
                Assert.IsTrue(DirectoryUtility.TryCreateDirectory(_test, out _));
                try
                {
                    Directory.Delete(_test);
                }
                catch { }
			}
		}
		[Test]
		public void InvalidDirectoryValidationTest()
		{
			foreach(var _test in testFailDirectories)
			{
				Assert.IsFalse(DirectoryUtility.TryCreateDirectory(_test, out _));
			}
		}
	}
}
