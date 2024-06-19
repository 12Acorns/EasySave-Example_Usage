using NEG.Plugins.EasySave.Data;
using NEG.Plugins.EasySave.SerializableTypes.Vector;
using System;
using UnityEngine;

[Serializable]
public struct TransformData : ISaveable
{
	public TransformData(Transform _transform)
		: this(_transform.position, _transform.localScale, _transform.rotation) { }

	public TransformData(Vector3 _position, Vector3 _scale, Quaternion _rotation)
	{
		Position = _position;
		Scale = _scale;
		Rotation = _rotation;
	}

	public SVector3 Position { get; set; }
	public SVector3 Scale { get; set; }
	public SVector4 Rotation { get; set; }


	public static implicit operator TransformData(Transform _transform)
	{
		return new TransformData(_transform);
	}
}