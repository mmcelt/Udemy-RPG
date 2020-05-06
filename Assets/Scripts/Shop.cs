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
	}

	public void OpenSellMenu()
	{
		_buyMenu.SetActive(false);
		_sellMenu.SetActive(true);
	}
	#endregion

	#region Private Methods


	#endregion
}
