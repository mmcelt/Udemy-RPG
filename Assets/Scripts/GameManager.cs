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

	public int _currentGold;

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
		SortItems();
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

		if (Input.GetKeyDown(KeyCode.J))
		{
			AddItem("Iron Armor");
			AddItem("Pooper Scooper");

			RemoveItem("Health Potion");
			RemoveItem("Crapola");
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

		Debug.LogError("Can't Find: " + itemToFind);
		return null;
	}

	public void SortItems()
	{
		bool itemAfterSpace = true;
		while (itemAfterSpace)
		{
			itemAfterSpace = false;

			for (int i = 0; i < _itemsHeld.Length - 1; i++)
			{
				if (_itemsHeld[i] == "")
				{
					_itemsHeld[i] = _itemsHeld[i + 1];
					_itemsHeld[i + 1] = "";

					_numberHeldOfItem[i] = _numberHeldOfItem[i + 1];
					_numberHeldOfItem[i + 1] = 0;

					if(_itemsHeld[i] != "")
					{
						itemAfterSpace = true;
					}
				}
			}
		}
	}

	public void AddItem(string itemToAdd)
	{
		int newItemPosition = 0;
		bool foundSpace = false;

		for(int i=0; i<_itemsHeld.Length; i++)
		{
			if(_itemsHeld[i]=="" || _itemsHeld[i] == itemToAdd)
			{
				newItemPosition = i;
				foundSpace = true;
				break;
			}
		}

		if (foundSpace)
		{
			bool itemExists = false;
			foreach(Item item in _referenceItems)
			{
				if (item._itemName == itemToAdd)
				{
					itemExists = true;
					break;
				}
			}

			if (itemExists)
			{
				_itemsHeld[newItemPosition] = itemToAdd;
				_numberHeldOfItem[newItemPosition]++;
			}
			else
			{
				Debug.LogError(itemToAdd + " New Item Not Valid!" );
			}
		}

		GameMenu.Instance.ShowItems();
	}

	public void RemoveItem(string itemToRemove)
	{
		bool foundItem = false;
		int itemPosition = 0;

		for(int i=0; i<_itemsHeld.Length; i++)
		{
			if (_itemsHeld[i] == itemToRemove)
			{
				foundItem = true;
				itemPosition = i;
				break;
			}
		}

		if (foundItem)
		{
			_numberHeldOfItem[itemPosition]--;
			if (_numberHeldOfItem[itemPosition] <= 0)
			{
				_numberHeldOfItem[itemPosition] = 0;
				_itemsHeld[itemPosition] = "";
			}
		}
		else
		{
			Debug.LogError(itemToRemove + " Item to Remove NOT FOUND!");
		}

		GameMenu.Instance.ShowItems();
	}
	#endregion

	#region Private Methods


	#endregion
}
