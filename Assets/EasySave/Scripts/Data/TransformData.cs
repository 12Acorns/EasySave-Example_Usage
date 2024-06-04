using NEG.Plugins.EasySave.Data;
using UnityEngine;

namespace MEG.Plugins.EasySave.Data
{
	public readonly struct TransformData : ISaveable
	{
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
}