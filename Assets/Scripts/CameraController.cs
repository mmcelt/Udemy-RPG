using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
	#region Fields

	[SerializeField] Transform _target;
	[SerializeField] Tilemap _theMap;

	Vector3 _bottomLeftLimit, _topRightLimit;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_target = PlayerController.Instance.transform;
		_bottomLeftLimit = _theMap.localBounds.min;
		_topRightLimit = _theMap.localBounds.max;
	}
	
	void LateUpdate() 
	{
		transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);

		//keep the camera inside the bounds
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, _bottomLeftLimit.x, _topRightLimit.x), Mathf.Clamp(transform.position.y, _bottomLeftLimit.y, _topRightLimit.y), transform.position.z);

		//
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
