using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
	#region Fields

	[SerializeField] float _waitToLoad = 1f;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if (_waitToLoad > 0)
		{
			_waitToLoad -= Time.deltaTime;
			if (_waitToLoad <= 0)
			{
				SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));
				GameManager.Instance.LoadData();
				QuestManager.Instance.LoadQuestData();
			}
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
