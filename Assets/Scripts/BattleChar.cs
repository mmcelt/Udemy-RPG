using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChar : MonoBehaviour
{
	#region Fields

	public bool _isPlayer;
	public string[] _movesAvailable;

	public string _charName;
	public int _currentHP, _maxHP, _currentMP, _maxMP, _STR, _DEF, _wpnPwr, _armPwr;
	public string _equippedWpn, _equippedArm;
	public bool _hasDied;

	public SpriteRenderer _theSprite;
	public Sprite _deadSprite, _aliveSprite;

	bool _shouldFade;
	public float _fadeSpeed = 1f;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}

	void Update()
	{
		if (_shouldFade)
		{
			_theSprite.color = new Color(Mathf.MoveTowards(_theSprite.color.r, 1, _fadeSpeed * Time.deltaTime), Mathf.MoveTowards(_theSprite.color.g, 0, _fadeSpeed * Time.deltaTime), Mathf.MoveTowards(_theSprite.color.b, 0, _fadeSpeed * Time.deltaTime), Mathf.MoveTowards(_theSprite.color.a, 0, _fadeSpeed * Time.deltaTime));

			if(_theSprite.color.a == 0)
			{
				gameObject.SetActive(false);
			}
		}
	}
	#endregion

	#region Public Methods

	public void EnemyFade()
	{
		_shouldFade = true;

	}
	#endregion

	#region Private Methods


	#endregion
}
