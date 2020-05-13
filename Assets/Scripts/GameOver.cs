using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	#region Fields

	[SerializeField] string _mainMenuScene, _loadGameScene;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		AudioManager.Instance.PlayMusic(4);
		PlayerController.Instance.gameObject.SetActive(false);
		GameMenu.Instance.gameObject.SetActive(false);
		BattleManager.Instance.gameObject.SetActive(false);
	}
	#endregion

	#region Public Methods

	public void ReturnToMainMenuButtonClicked()
	{
		Destroy(GameManager.Instance.gameObject);
		Destroy(PlayerController.Instance.gameObject);
		Destroy(GameMenu.Instance.gameObject);
		Destroy(AudioManager.Instance.gameObject);
		Destroy(BattleManager.Instance.gameObject);

		SceneManager.LoadScene(_mainMenuScene);
	}

	public void LastSaveButtonClicked()
	{
		Destroy(GameManager.Instance.gameObject);
		Destroy(PlayerController.Instance.gameObject);
		Destroy(GameMenu.Instance.gameObject);
		Destroy(BattleManager.Instance.gameObject);

		SceneManager.LoadScene(_loadGameScene);
	}
	#endregion

	#region Private Methods


	#endregion
}
