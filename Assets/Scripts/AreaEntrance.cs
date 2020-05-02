using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
	#region Fields

	public string _transitionName;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		if (_transitionName == PlayerController.Instance._areaTransitionName)
		{
			PlayerController.Instance.transform.position = transform.position;
		}

		UIFade.Instance.FadeFromBlack();
		GameManager.Instance._fadingBetweenAreas = false;

	}

	void Update() 
	{
		
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
