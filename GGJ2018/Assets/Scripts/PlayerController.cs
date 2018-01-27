using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	#region Variables

	[SerializeField]
	bool _allowedMovement = true;
	bool _canMove;

	public float _speedMove = 5f;

	#endregion

	#region Unity_methods

	void OnEnable () {

		Init();
	}

	void OnDisable() {
		_canMove = false;
	}

	void Init() {
		_canMove = _allowedMovement;
	}

	void Update () {
		if (_canMove) {
			float _xMov = Input.GetAxisRaw("Horizontal");
			float _zMov = Input.GetAxisRaw("Vertical");

			Vector3 _velocity = new Vector3(_xMov, 0, _zMov);

			transform.position = transform.position + (_velocity * _speedMove) * Time.deltaTime;
			if(_velocity != Vector3.zero) {
				transform.rotation = Quaternion.LookRotation(_velocity);
			}
		}
	}

	#endregion
}
