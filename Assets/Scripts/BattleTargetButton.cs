using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTargetButton : MonoBehaviour
{
	#region Fields

	public string _moveName;
	public int _activeBattlerTarget;
	public Text _targetName;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods

	public void OnButtonClicked()
	{
		BattleManager.Instance.PlayerAttack(_moveName, _activeBattlerTarget);
	}
	#endregion

	#region Private Methods


	#endregion
}
