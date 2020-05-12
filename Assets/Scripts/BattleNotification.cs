using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleNotification : MonoBehaviour
{
	#region Fields

	public Text _theText;
	[SerializeField] float _awakeTime;

	float _awakeCounter;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if (_awakeCounter > 0)
		{
			_awakeCounter -= Time.deltaTime;
			if (_awakeCounter <= 0)
				gameObject.SetActive(false);
		}
	}
	#endregion

	#region Public Methods

	public void Activate(string message)
	{
		gameObject.SetActive(true);
		_theText.text = message;
		_awakeCounter = _awakeTime;
	}
	#endregion

	#region Private Methods


	#endregion
}
