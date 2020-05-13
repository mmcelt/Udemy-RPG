using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
	#region Fields

	[SerializeField] Slider _healthbar;
	[SerializeField] BattleChar _theChar;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_healthbar.maxValue = _theChar._maxHP;
		_healthbar.value = _theChar._currentHP;
	}
	
	void Update() 
	{
		_healthbar.value = _theChar._currentHP;
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
