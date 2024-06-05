using NEG.Plugins.EasySave.System;
using UnityEngine;

public class ObjectDataSaver : MonoBehaviour
{
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

			var _transformData = new TransformData(_child);
			var _colourData = new ObjectData(_colour);

			SaveManager.Instance.TrySaveFile(_transformData, _object.name, "Transform Data");
			SaveManager.Instance.TrySaveFile(_colourData, _object.name, "Colour Data");
		}
	}
}
