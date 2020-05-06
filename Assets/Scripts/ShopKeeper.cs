using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
	#region Fields

	[SerializeField] string[] _itemsForSale = new string[40];

	bool _canOpen;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if(_canOpen && Input.GetButtonDown("Fire1") && PlayerController.Instance._canMove && !Shop.Instance._shopMenu.activeSelf)
		{
			Shop.Instance._itemsForSale = _itemsForSale;
			Shop.Instance.OpenShop();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_canOpen = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_canOpen = false;
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
