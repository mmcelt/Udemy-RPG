using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtton : MonoBehaviour
{
	#region Fields

	public Image _buttonImage;
	public Text _amountText;
	public int _buttonValue;

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

	public void OnItemButtonClicked()
	{
		if (GameMenu.Instance._theMenu.activeSelf)
		{
			if (GameManager.Instance._itemsHeld[_buttonValue] != "")
			{
				GameMenu.Instance.SelectItem(GameManager.Instance.GetItemDetails(GameManager.Instance._itemsHeld[_buttonValue]));
				//I ADDED THIS TO USE THE _SELECTEDiTEM FIELD IN GAMEMENU...
				//GameMenu.Instance._selectedItem = GameManager.Instance._itemsHeld[_buttonValue];
				GameMenu.Instance._useButton.interactable = true;
				GameMenu.Instance._dropButton.interactable = true;
			}
			else
			{
				GameMenu.Instance._useButton.interactable = false;
				GameMenu.Instance._dropButton.interactable = false;
			}
		}
		if (Shop.Instance._shopMenu.activeSelf)
		{
			if (Shop.Instance._buyMenu.activeSelf)
			{
				if (Shop.Instance._itemsForSale[_buttonValue] != "")
				{
					Shop.Instance.SelectBuyItem(GameManager.Instance.GetItemDetails(Shop.Instance._itemsForSale[_buttonValue]));

					Shop.Instance._buyButton.interactable = true;
				}
				else
				{
					Shop.Instance._buyButton.interactable = false;
					Shop.Instance._buyItemName.text = "";
					Shop.Instance._buyItemDesc.text = "";
					Shop.Instance._buyItemValue.text = "Value:";
				}
			}

			if (Shop.Instance._sellMenu.activeSelf)
			{
				if (GameManager.Instance._itemsHeld[_buttonValue] != "")
				{
					Shop.Instance.SelectSellItem(GameManager.Instance.GetItemDetails(GameManager.Instance._itemsHeld[_buttonValue]));

					Shop.Instance._sellButton.interactable = true;
				}
				else
				{
					Shop.Instance._sellButton.interactable = false;
					Shop.Instance._sellItemName.text = "";
					Shop.Instance._sellItemDesc.text = "";
					Shop.Instance._sellItemValue.text = "Value:";
				}
			}
		}

		if (GameManager.Instance._battleActive)
		{
			if(GameManager.Instance._itemsHeld[_buttonValue] != "")
			{
				BattleManager.Instance.SelectItem(GameManager.Instance.GetItemDetails(GameManager.Instance._itemsHeld[_buttonValue]));
				BattleManager.Instance._useButton.interactable = true;
			}
			else
			{
				BattleManager.Instance._useButton.interactable = false;
			}
		}
	}
	#endregion

	#region Private Methods


	#endregion
}
