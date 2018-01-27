using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	#region Variables

	Animator _animator;

	public int _playerIndex;

	[SerializeField]
	bool _allowedMovement = true;
	bool _canMove;

	bool canTakeOrbe = false;

	float _speedMove;
	float _sprintSpeed;
	float _boostSpeed;

	float _stamina;
	float _shipPiecesNumber;

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
		//if(other.tag == (Tags._collectible)) {
		////met un int en plus
		//	_stamina += 10;
		//	Destroy(other.gameObject);
		//}

		// touch Ship pieces
		if(other.tag == (Tags._shipPiece)) {
			_shipPiecesNumber += 1;
			Destroy(other.gameObject);
        }

        Debug.Log(name + "  + " + other.name);

        if (other.tag == (Tags._ship)) {
			if (_shipPiecesNumber !=0){
				_shipPiecesNumber -= 1;

                Debug.Log("TADADADA" + name + "  + " + other.name);
                GameManager.Instance.AddShipPiece();
		    }
		}
	}

	void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag == (Tags._wall)) {
			transform.position = transform.position;
		}

		if(other.gameObject.tag == (Tags._dino)) {
			gameObject.SetActive(false);
			GameManager.Instance._players.RemoveAt(_playerIndex - 1);
		}
	}

	void OnCollisionStay(Collision other) {
		if(other.gameObject.tag == (Tags._dighole)) {
			if(Input.GetButtonDown("J" + _playerIndex + "Bbutton")) {
				Destroy(other.gameObject);
			}
		}
		if(other.gameObject.tag == (Tags._collectible)) {
			//met un int en plus
			if(Input.GetButtonDown("J" + _playerIndex + "Bbutton")) {
				canTakeOrbe = true;
				StartCoroutine(TakeOrbe(other.gameObject));
			}
		}
	}

	IEnumerator TakeOrbe(GameObject p_go) {
		yield return new WaitForSeconds(1);
		Debug.Log("Destroy" + canTakeOrbe);
		if (canTakeOrbe) {
			_stamina += 10;
			Destroy(p_go);
		}

		canTakeOrbe = false;
		StopCoroutine("TakeOrbe");
	}
	#endregion
}
