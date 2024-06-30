using NEG.Plugins.EasySave.SaveSystem;
using System.IO;
using UnityEngine;

public sealed class ObjectDataSaver : MonoBehaviour
{
	private const string DIRECTORYNAME = "Scene Objects";
	private const string FILENAME = "Object Data";

	private void Awake()
	{
		var _savePath = Path.Combine(
			SaveManager.UnityInstance.ApplicationPath.GetFullPath().FullName, DIRECTORYNAME);

		Debug.Log($"Save Path: {_savePath}");
	}

	[ContextMenu("Save Items")]
	public void Save()
	{
		var _children = GetComponentsInChildren<Transform>();

		foreach(var _child in _children)
		{
			var _object = _child.gameObject;

			if(_object.gameObject == gameObject)
			{
				continue;
			}

			var _colour = _object.GetComponent<Renderer>().material.color;

			var _objectData = new ObjectData(_colour, _child);

			// ..\\Scene Objects\\Cube (1) -> (x)
			var _subDirectory = Path.Combine(DIRECTORYNAME, _object.name);

			SaveManager.UnityInstance.TrySaveFile(_objectData, _subDirectory, FILENAME);
		}
	}
}
