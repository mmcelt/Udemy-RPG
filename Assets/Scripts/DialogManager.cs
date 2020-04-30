using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject _dialogBox, _nameBox;
	[SerializeField] Text _dialogText, _nameText;
	[SerializeField] string[] _dialogLines;
	[SerializeField] int _currentLine;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_dialogText.text = _dialogLines[_currentLine];
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
