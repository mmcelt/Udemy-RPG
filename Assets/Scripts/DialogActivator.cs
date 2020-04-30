using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
	#region Fields

	public string[] _lines;

	bool _canActivate;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if (_canActivate && Input.GetButtonDown("Fire1") && !DialogManager.Instance._dialogBox.activeSelf)
		{
			DialogManager.Instance.ShowDialog(_lines);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_canActivate = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_canActivate = false;
		}
	}

	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
