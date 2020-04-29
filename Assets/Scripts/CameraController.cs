using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	#region Fields

	[SerializeField] Transform _target;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_target = PlayerController.Instance.transform;
		
	}
	
	void LateUpdate() 
	{
		transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
