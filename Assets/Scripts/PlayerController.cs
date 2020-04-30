using System.Collections;
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
	public bool _canMove = true;

	Vector3 _bottomLeftLimit, _topRightLimit;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if(Instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if (_canMove)
		{
			_theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * _moveSpeed;
		}
		else
		{
			_theRB.velocity = Vector2.zero;
		}

		_theAnim.SetFloat("moveX", _theRB.velocity.x);
		_theAnim.SetFloat("moveY", _theRB.velocity.y);

		if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
		{
			if (_canMove)
			{
				_theAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
				_theAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
			}
		}

		//keep the player inside the bounds of the map
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, _bottomLeftLimit.x, _topRightLimit.x), Mathf.Clamp(transform.position.y, _bottomLeftLimit.y, _topRightLimit.y), transform.position.z);
	}
	#endregion

	#region Public Methods

	public void SetBounds(Vector3 botleft, Vector3 topRight)
	{
		_bottomLeftLimit = botleft + new Vector3(0.5f, 0.7f, 0f);
		_topRightLimit = topRight + new Vector3(-0.5f, -0.7f, 0f);
	}
	#endregion

	#region Private Methods


	#endregion
}
