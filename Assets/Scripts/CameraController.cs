using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
	#region Fields

	[SerializeField] Transform _target;
	[SerializeField] Tilemap _theMap;
	public int _musicToPlay;

	Vector3 _bottomLeftLimit, _topRightLimit;
	float _halfHeight, _halfWidth;
	bool _musicStarted;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_target = PlayerController.Instance.transform;

		//camera limiting data...
		_halfHeight = Camera.main.orthographicSize;
		_halfWidth = _halfHeight * Camera.main.aspect;

		_bottomLeftLimit = _theMap.localBounds.min + new Vector3(_halfWidth, _halfHeight, 0f);
		_topRightLimit = _theMap.localBounds.max + new Vector3(-_halfWidth, -_halfHeight, 0f);

		//send map boundaries to the PlayerController for limiting Player movement
		PlayerController.Instance.SetBounds(_theMap.localBounds.min, _theMap.localBounds.max);
	}
	
	void LateUpdate() 
	{
		transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);

		//keep the camera inside the bounds
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, _bottomLeftLimit.x, _topRightLimit.x), Mathf.Clamp(transform.position.y, _bottomLeftLimit.y, _topRightLimit.y), transform.position.z);

		if (!_musicStarted)
		{
			_musicStarted = true;
			AudioManager.Instance.PlayMusic(_musicToPlay);
		}

	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
