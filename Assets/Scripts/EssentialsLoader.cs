using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject _uiScreen, _player, _gameManager;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
	}

	void Start() 
	{
		if (UIFade.Instance == null)
		{
			UIFade.Instance = Instantiate(_uiScreen).GetComponent<UIFade>();
		}
		if (PlayerController.Instance == null)
		{
			PlayerController clone = Instantiate(_player).GetComponent<PlayerController>();
			PlayerController.Instance = clone;
		}
		if (GameManager.Instance == null)
		{
			GameManager.Instance = Instantiate(_gameManager).GetComponent<GameManager>();
		}

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
