using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverLifetime : MonoBehaviour
{
	#region Fields

	[SerializeField] float _lifetime;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		Destroy(gameObject, _lifetime);
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
