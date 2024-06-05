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
		rotation = _rotation.eulerAngles;
	}

	public readonly Vector3 position;
	public readonly Vector3 scale;
	public readonly Vector4 rotation;

	public Vector4 GetVectorFromQuat(Quaternion _quat)
	{
		return new Vector4(_quat.x, _quat.y, _quat.z, _quat.w);
	}
}