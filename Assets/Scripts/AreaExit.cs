using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
	#region Fields

	[SerializeField] string _areaToLoad;
	public string _areaTransitionName;
	[SerializeField] AreaEntrance _theEntrance;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		_theEntrance._transitionName = _areaTransitionName;
	}

	void Start() 
	{
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerController.Instance._areaTransitionName = _areaTransitionName;
			SceneManager.LoadScene(_areaToLoad);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
