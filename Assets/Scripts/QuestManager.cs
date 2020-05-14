using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
	#region Fields

	public static QuestManager Instance;

	public string[] _questMarkerNames;
	public bool[] _questMarkersComplete;

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
		_questMarkersComplete = new bool[_questMarkerNames.Length];
	}
	
	//void Update() 
	//{
	//	if (Input.GetKeyDown(KeyCode.Q))
	//	{
	//		Debug.Log(CheckIfComplete("quest test"));
	//		MarkQuestComplete("quest test");
	//		MarkQuestIncomplete("fight the dragon");
	//	}
	//}
	#endregion

	#region Public Methods

	public bool CheckIfComplete(string questToCheck)
	{
		if(GetQuestNumber(questToCheck) != 0)
		{
			return _questMarkersComplete[GetQuestNumber(questToCheck)];
		}

		return false;
	}

	public void MarkQuestComplete(string questToMark)
	{
		_questMarkersComplete[GetQuestNumber(questToMark)] = true;
		UpdateLocalQuestObjects();
	}

	public void MarkQuestIncomplete(string questToMark)
	{
		_questMarkersComplete[GetQuestNumber(questToMark)] = false;
		UpdateLocalQuestObjects();
	}

	public void SaveQuestData()
	{
		for(int i=0; i<_questMarkerNames.Length; i++)
		{
			if(_questMarkersComplete[i])
			{
				PlayerPrefs.SetInt("QuestMarker_" + _questMarkerNames[i], 1);
			}
			else
			{
				PlayerPrefs.SetInt("QuestMarker_" + _questMarkerNames[i], 0);
			}
		}
	}

	public void LoadQuestData()
	{
		for (int i = 0; i < _questMarkerNames.Length; i++)
		{
			int valueToSet = 0;
			if (PlayerPrefs.HasKey("QuestMarker_" + _questMarkerNames[i]))
			{
				valueToSet = PlayerPrefs.GetInt("QuestMarker_" + _questMarkerNames[i]);
			}
			if (valueToSet == 0)
			{
				_questMarkersComplete[i] = false;
			}
			else
			{
				_questMarkersComplete[i] = true;
			}
		}
	}
	#endregion

	#region Private Methods

	int GetQuestNumber(string questToFind)
	{
		for (int i = 0; i < _questMarkerNames.Length; i++)
		{
			if (_questMarkerNames[i] == questToFind)
			{
				return i;
			}
		}
		Debug.LogError("Quest: " + questToFind + " does not exist!");
		return 0;
	}

	void UpdateLocalQuestObjects()
	{
		QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();
		if(questObjects.Length>0)
		{
			foreach(QuestObjectActivator qoa in questObjects)
			{
				qoa.CheckCompletion();
			}
		}
	}
	#endregion
}
