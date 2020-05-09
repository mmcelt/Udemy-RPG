using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	#region Fields

	[SerializeField] string _continueGameScene, _newGameScene;
	[SerializeField] GameObject _continueButton;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_continueButton.SetActive(PlayerPrefs.HasKey("Current_Scene"));
	}
	#endregion

	#region Public Methods

	public void Continue()
	{
		SceneManager.LoadScene(_continueGameScene);
	}

	public void NewGame()
	{
		SceneManager.LoadScene(_newGameScene);
	}

	public void ExitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}
	#endregion

	#region Private Methods


	#endregion
}
