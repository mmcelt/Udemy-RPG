using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	#region Fields

	[SerializeField] string _newGameScene;
	[SerializeField] GameObject _continueButton;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_continueButton.SetActive(PlayerPrefs.HasKey("Current_Scene"));
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods

	public void Continue()
	{

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
