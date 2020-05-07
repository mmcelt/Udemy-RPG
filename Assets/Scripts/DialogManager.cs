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

	string _questToMark;
	bool _markQuestComplete, _shouldMarkQuest;

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
						GameManager.Instance._dialogActive = false;

						if (_shouldMarkQuest)
						{
							_shouldMarkQuest = false;
							if (_markQuestComplete)
							{
								QuestManager.Instance.MarkQuestComplete(_questToMark);
							}
							else
							{
								QuestManager.Instance.MarkQuestIncomplete(_questToMark);
							}
						}
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

	public void ShowDialog(string[] newLines, bool isPerson)
	{
		_dialogLines = newLines;
		_currentLine = 0;

		CheckIfName();	//check for a character name

		_dialogText.text = _dialogLines[_currentLine];
		_dialogBox.SetActive(true);

		if(!isPerson)
			_nameBox.SetActive(false);

		_justStarted = true;
		//PlayerController.Instance._canMove = false;
		GameManager.Instance._dialogActive = true;
	}

	public void ShouldActivateQuestAtEnd(string questName, bool markComplete)
	{
		_questToMark = questName;
		_markQuestComplete = markComplete;

		_shouldMarkQuest = true;
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
