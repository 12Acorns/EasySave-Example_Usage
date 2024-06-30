using NEG.Plugins.EasySave.Data;
using NEG.Plugins.EasySave.SerializableTypes.Material;
using System;
using UnityEngine;

[Serializable]
public struct ObjectData : ISaveable
{
	public ObjectData(Color _colour, TransformData _transform)
	{
		Colour = _colour;
		Transform = _transform;
	}
	public TransformData Transform { get; set; }
	public SColour Colour { get; set; }
}