using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject _uiScreen, _player, _gameManager, _audioManager, _battleManager;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		if (UIFade.Instance == null)
		{
			UIFade.Instance = Instantiate(_uiScreen).GetComponent<UIFade>();
		}
		if (PlayerController.Instance == null)
		{
			PlayerController.Instance = Instantiate(_player).GetComponent<PlayerController>();
		}
		if (GameManager.Instance == null)
		{
			GameManager.Instance = Instantiate(_gameManager).GetComponent<GameManager>();
		}
		if (AudioManager.Instance == null)
		{
			AudioManager.Instance = Instantiate(_audioManager).GetComponent<AudioManager>();
		}
		if (BattleManager.Instance == null)
		{
			BattleManager.Instance = Instantiate(_battleManager).GetComponent<BattleManager>();
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
