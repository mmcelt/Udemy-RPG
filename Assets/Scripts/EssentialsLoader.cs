using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject _uiScreen, _player;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if(UIFade.Instance == null)
		{
			UIFade.Instance = Instantiate(_uiScreen).GetComponent<UIFade>();
		}
		if (PlayerController.Instance == null)
		{
			PlayerController clone = Instantiate(_player).GetComponent<PlayerController>();
			PlayerController.Instance = clone;
		}
	}

	void Start() 
	{
		
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
