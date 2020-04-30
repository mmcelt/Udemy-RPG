using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
	#region Fields

	public static DialogManager Instance;

	public GameObject _dialogBox, _nameBox;
	[SerializeField] Text _dialogText, _nameText;
	[SerializeField] string[] _dialogLines;
	[SerializeField] int _currentLine;

	bool _justStarted;

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
		//_dialogText.text = _dialogLines[_currentLine];
	}
	
	void Update() 
	{
		if (_dialogBox.activeSelf)
		{
			if (Input.GetButtonUp("Fire1"))
			{
				if (!_justStarted)
				{
					_currentLine++;

					if (_currentLine >= _dialogLines.Length)
					{
						_dialogBox.SetActive(false);
						PlayerController.Instance._canMove = true;
					}
					else
					{
						CheckIfName();

						_dialogText.text = _dialogLines[_currentLine];
					}
				}
				else
				{
					_justStarted = false;
				}
			}
		}
	}
	#endregion

	#region Public Methods

	public void ShowDialog(string[] newLines)
	{
		_dialogLines = newLines;
		_currentLine = 0;

		CheckIfName();	//check for a character name

		_dialogText.text = _dialogLines[_currentLine];
		_dialogBox.SetActive(true);
		_justStarted = true;
		PlayerController.Instance._canMove = false;
	}
	#endregion

	#region Private Methods

	void CheckIfName()
	{
		if (_dialogLines[_currentLine].StartsWith("n-"))
		{
			_nameText.text = _dialogLines[_currentLine].Replace("n-","");
			_currentLine++;
		}
	}
	#endregion
}
