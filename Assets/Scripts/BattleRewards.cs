using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleRewards : MonoBehaviour
{
	#region Fields

	public static BattleRewards Instance;

	[SerializeField] Text _xpText, _itemsText;
	[SerializeField] GameObject _rewardScreen;
	[SerializeField] string[] _rewardItems;
	[SerializeField] int _xpEarned;
	//TODO: ADD GOLD...

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
	}

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if (Input.GetKeyDown(KeyCode.Y))
		{
			OpenRewardScreen(54, new string[] { "Iron Sword", "Iron Armor" });
		}
	}
	#endregion

	#region Public Methods

	public void OpenRewardScreen(int xp, string[] rewards)
	{
		_xpEarned = xp;
		_rewardItems = rewards;

		_xpText.text = "Surviving Players Earned " + _xpEarned + " XP!";
		_itemsText.text = "";

		foreach(string item in _rewardItems)
		{
			_itemsText.text += item + "\n";
		}

		_rewardScreen.SetActive(true);
	}

	public void CloseRewardScreen()
	{

		_rewardScreen.SetActive(false);
	}
	#endregion

	#region Private Methods


	#endregion
}
