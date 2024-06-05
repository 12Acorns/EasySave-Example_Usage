using System.Collections.Generic;
using UnityEngine;

public sealed class ObjectDataHolder : MonoBehaviour
{
	private TransformData[] transformData;
	private ObjectData[] objectData;

	[ContextMenu("Cache Data")]
	private void SaveData()
	{
		var _children = GetComponentsInChildren<Transform>();

		transformData = new TransformData[_children.Length - 1];
		objectData = new ObjectData[_children.Length - 1];

		int _index = 0;
		foreach(var _child in _children)
		{
			var _object = _child.gameObject;

			if(_object == gameObject)
			{
				continue;
			}

			var _material = _object.GetComponent<Renderer>().material;

			transformData[_index] = new TransformData(_child);
			objectData[_index] = new ObjectData(_material.color);

			_index++;
		}
	}
}
