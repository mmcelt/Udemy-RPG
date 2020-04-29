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
	[SerializeField] float _waitToLoad = 1f;

	bool _shouldLoadAfterFade;
	//float _waitToLoadCounter;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		_theEntrance._transitionName = _areaTransitionName;
	}

	void Start() 
	{
		//_waitToLoadCounter = _waitToLoad;
	}

	void Update()
	{
		if (_shouldLoadAfterFade)
		{
			_waitToLoad -= Time.deltaTime;
		}
		if (_waitToLoad <= 0)
		{
			SceneManager.LoadScene(_areaToLoad);
			//_waitToLoadCounter = _waitToLoad;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerController.Instance._areaTransitionName = _areaTransitionName;

			_shouldLoadAfterFade = true;
			UIFade.Instance.FadeToBlack();
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
