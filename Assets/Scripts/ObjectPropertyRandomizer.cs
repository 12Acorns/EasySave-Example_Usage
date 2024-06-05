using UnityEngine;

internal sealed class ObjectPropertyRandomizer : MonoBehaviour
{
	private void Awake()
	{
		var _children = GetComponentsInChildren<Transform>();

		foreach(var _child in _children)
		{
			// Get Components
			var _object = _child.gameObject;
			if(!_object.TryGetComponent<Renderer>(out var _renderer))
			{
				continue;
			}
			var _material = _renderer.material;

			// Rotation
			var _positionOffset = GetRandomPosition();
			_object.transform.position += _positionOffset;

			// Rotation
			var _rotationOffset = GetRandomRotation();

			_object.transform.rotation = SetRotationY(_object.transform.rotation, _rotationOffset);

			// Colour
			_material.color = Random.ColorHSV(0, 1, 0, 1, 0, 1);
		}
	}
	private static Vector3 GetRandomPosition()
	{
		return new Vector3(Random.Range(0, 1), 0, Random.Range(0, 1));
	}
	private static Quaternion GetRandomRotation()
	{
		return Random.rotation;
	}
	private static Quaternion SetRotationY(Quaternion _original, Quaternion _new)
	{
		return new Quaternion(_original.x, _new.y, _original.z, _original.w);
	}
}
