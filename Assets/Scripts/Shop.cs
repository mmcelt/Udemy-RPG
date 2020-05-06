using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
	#region Fields

	public static Shop Instance;

	public GameObject _shopMenu, _buyMenu, _sellMenu;
	[SerializeField] Text _goldText;
	public string[] _itemsForSale;
	[SerializeField] ItemButtton[] _buyItemButtons;
	[SerializeField] ItemButtton[] _sellItemButtons;
	Item _selectedItem;
	public Text _buyItemName, _buyItemDesc, _buyItemValue;
	public Text _sellItemName, _sellItemDesc, _sellItemValue;
	public Button _buyButton, _sellButton;

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
		if(Input.GetKeyDown(KeyCode.K) && !_shopMenu.activeSelf)
		{
			OpenShop();
		}
	}
	#endregion

	#region Public Methods

	public void OpenShop()
	{
		_shopMenu.SetActive(true);
		_buyMenu.SetActive(true);
		GameManager.Instance._shopOpen = true;
		_goldText.text = GameManager.Instance._currentGold + "g";
		OpenBuyMenu();
	}

	public void CloseShop()
	{
		_shopMenu.SetActive(false);
		_sellMenu.SetActive(false);
		GameManager.Instance._shopOpen = false;
	}

	public void OpenBuyMenu()
	{
		_buyMenu.SetActive(true);
		_sellMenu.SetActive(false);

		_buyItemButtons[0].OnItemButtonClicked();

		for (int i = 0; i < _buyItemButtons.Length; i++)
		{
			_buyItemButtons[i]._buttonValue = i;

			if (_itemsForSale[i] != "")
			{
				_buyItemButtons[i]._buttonImage.gameObject.SetActive(true);
				_buyItemButtons[i]._buttonImage.sprite = GameManager.Instance.GetItemDetails(_itemsForSale[i])._itemSprite;
				_buyItemButtons[i]._amountText.text = "";
			}
			else
			{
				_buyItemButtons[i]._buttonImage.gameObject.SetActive(false);
				_buyItemButtons[i]._amountText.text = "";
			}
		}
	}

	public void OpenSellMenu()
	{
		_buyMenu.SetActive(false);
		_sellMenu.SetActive(true);

		_sellItemButtons[0].OnItemButtonClicked();

		UpdateSellItems();
	}

	public void SelectBuyItem(Item buyItem)
	{
		_selectedItem = buyItem;
		_buyItemName.text = _selectedItem._itemName;
		_buyItemDesc.text = _selectedItem._itemDesc;
		_buyItemValue.text = "Value: " + _selectedItem._itemValue + "g";
	}

	public void SelectSellItem(Item sellItem)
	{
		_selectedItem = sellItem;
		_sellItemName.text = _selectedItem._itemName;
		_sellItemDesc.text = _selectedItem._itemDesc;
		_sellItemValue.text = "Value: " + Mathf.RoundToInt(_selectedItem._itemValue * 0.5f) + "g";
	}

	public void BuyItem()
	{
		if (_selectedItem == null) return;

		if(GameManager.Instance._currentGold >= _selectedItem._itemValue)
		{
			GameManager.Instance._currentGold -= _selectedItem._itemValue;
			GameManager.Instance.AddItem(_selectedItem._itemName);
		}
		_goldText.text = GameManager.Instance._currentGold + "g";
	}

	public void SellItem()
	{
		if (_selectedItem == null) return;

		GameManager.Instance._currentGold += Mathf.RoundToInt(_selectedItem._itemValue * 0.5f);
		GameManager.Instance.RemoveItem(_selectedItem._itemName);
		_goldText.text = GameManager.Instance._currentGold + "g";

		UpdateSellItems();
	}
	#endregion

	#region Private Methods

	void UpdateSellItems()
	{
		GameManager.Instance.SortItems();

		for (int i = 0; i < _sellItemButtons.Length; i++)
		{
			_sellItemButtons[i]._buttonValue = i;

			if (GameManager.Instance._itemsHeld[i] != "")
			{
				_sellItemButtons[i]._buttonImage.gameObject.SetActive(true);
				_sellItemButtons[i]._buttonImage.sprite = GameManager.Instance.GetItemDetails(GameManager.Instance._itemsHeld[i])._itemSprite;
				_sellItemButtons[i]._amountText.text = GameManager.Instance._numberHeldOfItem[i].ToString();
			}
			else
			{
				_sellItemButtons[i]._buttonImage.gameObject.SetActive(false);
				_sellItemButtons[i]._amountText.text = "";
			}
		}
	}
	#endregion
}
