using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	#region Variables

	Animator _animator;

	public int _playerIndex;

	[SerializeField]
	bool _allowedMovement = true;
	bool _canMove;

	float _speedMove;
	float _sprintSpeed;
	float _boostSpeed;

	float _stamina;
	#endregion

	#region Unity_methods

	void Start () {
		Init();
	}

	void OnDisable() {
		_canMove = false;
	}

	void Init() {
		_animator = GetComponent<Animator>();
		_canMove = _allowedMovement;
		_speedMove = GameManager.Instance._startMoveSpeed;
		_stamina = GameManager.Instance._startStamina;
	}

	void FixedUpdate() {
		if (!Input.GetButton("J" + _playerIndex + "Abutton")) {
			if(_stamina < GameManager.Instance._maxStamina * 0.5f) {
				_stamina += GameManager.Instance._regenStamina;
			}
		}
	}

	void Update () {

		if(Input.GetButtonUp("J" + _playerIndex + "Abutton")) {
			_sprintSpeed = 0;
		}


		if(_stamina < GameManager.Instance._minStamina) {
			_stamina = GameManager.Instance._minStamina;
		}
		if(_stamina > GameManager.Instance._maxStamina) {
			_stamina = GameManager.Instance._maxStamina;
		}


		// Boost Here !

		if (_canMove) {
			float _xMov = Input.GetAxisRaw("J" + _playerIndex + "Horizontal");
			float _zMov = Input.GetAxisRaw("J" + _playerIndex + "Vertical");

			Vector3 _velocity = new Vector3(_xMov, 0, _zMov);

			if(Input.GetButton("J" + _playerIndex + "Abutton")) {
				if (_stamina < GameManager.Instance._limitToUseStamina) {
					_sprintSpeed = 0;
				} else {
					_stamina -= GameManager.Instance._costStamina;
					_sprintSpeed = GameManager.Instance._sprintSpeed;
				}
			} else if(Input.GetButton("J" + _playerIndex + "RBbutton")) {
				//StartCoroutine(ActivateBoost());
			}

			transform.position = transform.position + (_velocity * (_speedMove + _sprintSpeed + _boostSpeed)) * Time.deltaTime;
			if(_velocity != Vector3.zero) {
				transform.rotation = Quaternion.LookRotation(_velocity);
			}

			if(_sprintSpeed != 0) {
				_animator.SetBool("Boost", false);
				_animator.SetBool("Sprint", true);
				_animator.SetBool("Run", false);
			} else if(_boostSpeed != 0) {
				_animator.SetBool("Boost", true);
				_animator.SetBool("Sprint", false);
				_animator.SetBool("Run", false);
			} else if(_velocity == Vector3.zero) {
				_animator.SetBool("Boost", false);
				_animator.SetBool("Sprint", false);
				_animator.SetBool("Run", false);
			} else {
				_animator.SetBool("Boost", false);
				_animator.SetBool("Sprint", false);
				_animator.SetBool("Run", true);
			}

		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag.Contains(Tags._collectible)) {
			Destroy(other.gameObject);
		}

		// touch Ship
	}

	void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag.Contains(Tags._wall)) {
			transform.position = transform.position;
		}

		if(other.gameObject.tag.Contains(Tags._dino)) {
			gameObject.SetActive(false);
			GameManager.Instance._players.RemoveAt(_playerIndex - 1);
		}
	}

	void OnCollisionStay(Collision other) {
		if(other.gameObject.tag.Contains(Tags._dighole)) {
			if(Input.GetButtonDown("J" + _playerIndex + "Bbutton")) {
				Destroy(other.gameObject);
			}
		}
	}

	#endregion
}
