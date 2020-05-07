using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject _objectToActivate;
	[SerializeField] string _questToCheck;
	[SerializeField] bool _activeIfComplete;

	bool _initialCheckCompleted;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if (!_initialCheckCompleted)
		{
			_initialCheckCompleted = true;
			CheckCompletion();
		}
	}
	#endregion

	#region Public Methods

	public void CheckCompletion()
	{
		if (QuestManager.Instance.CheckIfComplete(_questToCheck))
		{
			_objectToActivate.SetActive(_activeIfComplete);
		}
	}
	#endregion

	#region Private Methods


	#endregion
}
