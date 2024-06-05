using NEG.Plugins.EasySave.Data;
using UnityEngine;
using System;

[Serializable]
public readonly struct ObjectData : ISaveable
{
	public ObjectData(Color _colour)
	{
		Red = _colour.r;
		Green = _colour.g;
		Blue = _colour.b;
		Alpha = _colour.a;
	}

	public readonly float Red;
	public readonly float Green;
	public readonly float Blue;
	public readonly float Alpha;
}
