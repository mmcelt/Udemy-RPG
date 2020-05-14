using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStarter : MonoBehaviour
{
	#region Fields

	public BattleType[] _potentialBattles;

	[SerializeField] bool _activateOnEnter, _activateOnStay, _activateOnExit, _dactivateAfterStarting, _canRetreat = true;

	[SerializeField] float _timeBetweenBattles = 10f;

	[SerializeField] bool _shouldCompleteQuest;
	[SerializeField] string _questToComplete;

	float _betweenBattleCounter;
	bool _inArea;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_betweenBattleCounter = Random.Range(_timeBetweenBattles * 0.5f, _timeBetweenBattles * 1.5f);
	}

	void Update()
	{
		if (_inArea && PlayerController.Instance._canMove)
		{
			if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
			{
				_betweenBattleCounter -= Time.deltaTime;
			}
			if (_betweenBattleCounter <= 0)
			{
				_betweenBattleCounter = Random.Range(_timeBetweenBattles * 0.5f, _timeBetweenBattles * 1.5f);

				StartCoroutine(StartBattleRoutine());
			}
		}

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			if(_activateOnEnter)
			{
				StartCoroutine(StartBattleRoutine());
			}
			else
			{
				_inArea = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			if (_activateOnExit)
			{
				StartCoroutine(StartBattleRoutine());
			}
			else
			{
				_inArea = false;
			}
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	IEnumerator StartBattleRoutine()
	{
		UIFade.Instance.FadeToBlack();
		GameManager.Instance._battleActive = true;

		int selectedBattle = Random.Range(0, _potentialBattles.Length);
		BattleManager.Instance._rewardItems = _potentialBattles[selectedBattle]._rewardItems;
		BattleManager.Instance._rewardXP = _potentialBattles[selectedBattle]._rewardXP;

		yield return new WaitForSeconds(1.0f);

		UIFade.Instance.FadeFromBlack();

		yield return new WaitForSeconds(0.5f);

		BattleManager.Instance.BattleStart(_potentialBattles[selectedBattle]._enemies, _canRetreat);

		if (_dactivateAfterStarting)
			gameObject.SetActive(false);

		BattleRewards.Instance._markQuestComplete = _shouldCompleteQuest;
		BattleRewards.Instance._questToMark = _questToComplete;
	}
	#endregion
}
