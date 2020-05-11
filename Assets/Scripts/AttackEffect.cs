using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
	#region Fields

	[SerializeField] float _effectLength;
	[SerializeField] int _SFX;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		AudioManager.Instance.PlaySFX(_SFX);
		Destroy(gameObject, _effectLength);
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
