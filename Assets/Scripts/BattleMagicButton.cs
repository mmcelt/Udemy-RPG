using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMagicButton : MonoBehaviour
{
	#region Fields

	public string _spellName;
	public int _spellCost;
	public Text _nameText, _costText;

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
		if(BattleManager.Instance._activeBattlers[BattleManager.Instance._currentTurn]._currentMP >= _spellCost)
		{
			BattleManager.Instance._magicMenu.SetActive(false);
			BattleManager.Instance.OpenTargetMenu(_spellName);
			BattleManager.Instance._activeBattlers[BattleManager.Instance._currentTurn]._currentMP -= _spellCost;
		}
		else
		{
			BattleManager.Instance._battleNotice.Activate("Not Enough MP!!");
			BattleManager.Instance._magicMenu.SetActive(false);
		}
	}
	#endregion

	#region Private Methods


	#endregion
}
