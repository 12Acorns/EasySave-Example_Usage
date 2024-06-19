using NEG.Plugins.EasySave.SaveSystem;
using NEG.Plugins.EasySave.Utility;
using System.IO;
using UnityEngine;

public sealed class ObjectDataLoader : MonoBehaviour
{
	private void Awake()
	{
		saveDirectoryPath =
			Path.Combine(
				SaveManager.Instance.ApplicationPath.GetFullPath().FullName, DIRECTORYNAME);
	}

	private string saveDirectoryPath;

	private const string DIRECTORYNAME = "Scene Objects";

	[ContextMenu("Load Items")]
	public void Load()
	{
		if(!DirectoryUtility.TryGetSubDirectories(saveDirectoryPath, out var _subDirectories))
		{
			Debug.Log("Did not find directory");
			return;
		}

		foreach(var _directory in _subDirectories)
		{
			if(!FileUtility.TryGetFilesInDirectory(_directory, out var _files))
			{
				continue;
			}
			foreach(var _file in _files)
			{
				var _fileInfo = new FileInfo(_file);
				var _parentDirectoryOfFile = _fileInfo.Directory.Name;
				var _fileName = _fileInfo.Name.Split('.')[0];

				var _localPathToFile = Path.Combine(DIRECTORYNAME, _parentDirectoryOfFile);

				var _data = LoadManager.Instance.LoadFile<ObjectData>
					(_localPathToFile, _fileName);

				InitObject(_data.AsT0);
			}
		}
	}

	private void InitObject(ObjectData _data)
	{
		var _object = GameObject.CreatePrimitive(PrimitiveType.Cube);
		InitTransform(_object, _data);
		InitColour(_object, _data);
	}
	private void InitTransform(GameObject _object, ObjectData _data)
	{
		_object.transform.parent = transform;

		_object.transform.position = _data.Transform.Position.GetVector();
		_object.transform.localScale = _data.Transform.Scale.GetVector();
		_object.transform.rotation = _data.Transform.Rotation.GetQuaternion();
	}
	private static void InitColour(GameObject _object, ObjectData _data)
	{
		_object.GetComponent<Renderer>().material.color = _data.Colour.GetColour();

	}
}
