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
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
