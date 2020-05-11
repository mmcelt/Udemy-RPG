using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
	#region Fields

	[SerializeField] Text _damageText;
	[SerializeField] float _lifetime = 1f;
	[SerializeField] float _moveSpeed = 1f;
	[SerializeField] float _placementJitter = 0.5f;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		Destroy(gameObject, _lifetime);
	}
	
	void Update() 
	{
		transform.position += new Vector3(0f, _moveSpeed * Time.deltaTime, 0f);
	}
	#endregion

	#region Public Methods

	public void SetDamage(int damage)
	{
		_damageText.text = "-" + damage;
		transform.position += new Vector3(Random.Range(-_placementJitter, _placementJitter), Random.Range(-_placementJitter, _placementJitter), 0f);
	}
	#endregion

	#region Private Methods


	#endregion
}
