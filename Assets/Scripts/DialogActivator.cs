using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
	#region Fields

	[SerializeField] bool _isPerson = true;
	public string[] _lines;

	[SerializeField] bool _shouldActivatQuest, _markComplete;
	[SerializeField] string _questToMark;

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
			DialogManager.Instance.ShowDialog(_lines, _isPerson);

			if (_shouldActivatQuest)
			{
				DialogManager.Instance.ShouldActivateQuestAtEnd(_questToMark, _markComplete);
			}
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
