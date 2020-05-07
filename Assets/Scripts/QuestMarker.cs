using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarker: MonoBehaviour
{
	#region Fields

	[SerializeField] string _questToMark;
	[SerializeField] bool _markComplete, _markOnEnter, _deactivateOnMarking;

	bool _canMark;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if(_canMark && Input.GetButtonDown("Fire1"))
		{
			_canMark = false;
			MarkQuest();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			if (_markOnEnter)
			{
				MarkQuest();
			}
			else
				_canMark = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_canMark = false;
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	void MarkQuest()
	{
		if (_markComplete)
		{
			QuestManager.Instance.MarkQuestComplete(_questToMark);
		}
		else
		{
			QuestManager.Instance.MarkQuestIncomplete(_questToMark);
		}

		gameObject.SetActive(!_deactivateOnMarking);
	}
	#endregion
}
