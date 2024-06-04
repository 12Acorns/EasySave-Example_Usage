using NEG.Plugins.EasySave.Data;
using UnityEngine;
using System;

[Serializable]
public readonly struct TransformData : ISaveable
{
	public TransformData(Transform _transform)
		: this(_transform.position, _transform.localScale, _transform.rotation) { }

	public TransformData(Vector3 _position, Vector3 _scale, Quaternion _rotation)
	{
		position = _position;
		scale = _scale;
		rotation = _rotation;
	}

	public readonly Vector3 position;
	public readonly Vector3 scale;
	public readonly Quaternion rotation;
}