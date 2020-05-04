using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Fields

	public static GameManager Instance;

	public CharSats[] _playerStats;
	public bool _gameMenuOpen, _dialogActive, _fadingBetweenAreas;
	public string[] _itemsHeld;
	public int[] _numberHeldOfItem;
	public Item[] _referenceItems;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if(_gameMenuOpen || _dialogActive || _fadingBetweenAreas)
		{
			PlayerController.Instance._canMove = false;
		}
		else
		{
			PlayerController.Instance._canMove = true;
		}
	}
	#endregion

	#region Public Methods

	public Item GetItemDetails(string itemToFind)
	{
		for(int i=0; i<_referenceItems.Length; i++)
		{
			if (_referenceItems[i]._itemName == itemToFind)
			{
				return _referenceItems[i];
			}
		}

		return null;
	}
	#endregion

	#region Private Methods


	#endregion
}
