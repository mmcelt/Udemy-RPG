using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
	#region Fields

	public static UIFade Instance;

	[SerializeField] Image _fadePanel;
	[SerializeField] float _fadeSpeed;

	bool _shouldFadeToBlack, _shouldFadeFromBlack;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if( Instance == null)
			Instance = this;
		else if(Instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Start() 
	{
		_shouldFadeFromBlack = false;
		_shouldFadeToBlack = false;
	}
	
	void Update() 
	{
		if (_shouldFadeToBlack)
		{
			_fadePanel.color = new Color(_fadePanel.color.r, _fadePanel.color.g, _fadePanel.color.b, Mathf.MoveTowards(_fadePanel.color.a, 1f, _fadeSpeed * Time.deltaTime));

			if (_fadePanel.color.a == 1f)
			{
				_shouldFadeToBlack = false;
			}
		}
		if (_shouldFadeFromBlack)
		{
			_fadePanel.color = new Color(_fadePanel.color.r, _fadePanel.color.g, _fadePanel.color.b, Mathf.MoveTowards(_fadePanel.color.a, 0f, _fadeSpeed * Time.deltaTime));

			if (_fadePanel.color.a == 1f)
			{
				_shouldFadeFromBlack = false;
			}
		}
	}
	#endregion

	#region Public Methods

	public void FadeToBlack()
	{
		_shouldFadeToBlack = true;
		_shouldFadeFromBlack = false;
	}

	public void FadeFromBlack()
	{
		_shouldFadeFromBlack = true;
		_shouldFadeToBlack = false;
	}
	#endregion

	#region Private Methods


	#endregion
}
