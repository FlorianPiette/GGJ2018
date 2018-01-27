using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

	#region Variables
	[SerializeField]
	GameObject playerPrefab;
	public GameObject collectiblePrefab;
	public GameObject shipPiecePrefab;
	List<PlayerController> _players = new List<PlayerController>();
	[SerializeField]
	 Transform[] _playerSpawns;
	public Transform[] _collectibleSpawns;
	public Transform[] _ShipPieceSpawns;

	public float _startMoveSpeed = 1.9f;

	public float _sprintSpeed = 2f;

	public float _startStamina = 30f;
	public float _minStamina = 0f;
	public float _maxStamina = 100f;
	public float _regenStamina = 0.1f;
	public float _costStamina = 1.5f;
	public float _limitToUseStamina = 10f;

	bool _createCollectible;
	bool _createShipPieces;

	#endregion

	#region Unity_methods

	void OnEnable() {
		Init();
	}

//	void Update (){
//
//		if (_createShipPieces==false){
//		StartCoroutine (DelayCreateShipPieces());
//		}
//	}


	#endregion

	#region Unity_methods

	void Init() {
		int numberOfPlayers = Input.GetJoystickNames().Length - 1;
		Debug.Log("Nombre de Manettes connectés = " + numberOfPlayers);

		for(int i = 0; i < numberOfPlayers; ++i) {
			GameObject newplayer = Instantiate(playerPrefab, _playerSpawns[i]);
			newplayer.transform.parent = null;
			_players.Add(newplayer.GetComponent<PlayerController>());
		}

		//GameObject[] players = GameObject.FindGameObjectsWithTag(Tags._player);
		//foreach (GameObject player in players) {
		//	_players.Add(player.GetComponent<PlayerController>());
		//}

		for(int i = 0; i < _players.Count; ++i) {
			_players[i]._playerIndex = i + 1;
		}

		StartCoroutine (DelayCreateShipPieces());

	}


	public void CreateShipPiece() {
		if (_createShipPieces){
			for(int i = 0; i < 4; ++i) {

				GameObject newShipPiece = Instantiate(shipPiecePrefab, _ShipPieceSpawns[i]);
				newShipPiece.transform.parent = null;
			}
		}

	}

	IEnumerator DelayCreateShipPieces (){

			_createShipPieces = false;
		yield return new WaitForSeconds (16f);
		_createShipPieces = true;
		CreateShipPiece ();
		StartCoroutine (DelayCreateShipPieces());
	}



	#endregion
}
