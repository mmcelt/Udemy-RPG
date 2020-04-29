﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
	#region Fields

	public static PlayerController Instance;

	[SerializeField] Rigidbody2D _theRB;
	[SerializeField] float _moveSpeed = 5f;
	[SerializeField] Animator _theAnim;
	public string _areaTransitionName;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Start() 
	{
		
	}
	
	void Update() 
	{
		_theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * _moveSpeed;

		_theAnim.SetFloat("moveX", _theRB.velocity.x);
		_theAnim.SetFloat("moveY", _theRB.velocity.y);

		if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
		{
			_theAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
			_theAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}