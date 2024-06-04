using NEG.Plugins.EasySave.Data;
using UnityEngine;
using System;

[Serializable]
public class ObjectData : ISaveable
{
	public ObjectData(Color _colour)
	{
		Colour = _colour;
	}

	public readonly Color Colour;
}
