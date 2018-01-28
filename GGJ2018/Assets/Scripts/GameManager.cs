using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

	#region Variables
	[SerializeField]
	GameObject playerPrefab;
    public GameObject collectiblePrefab;
	public GameObject shipPiecePrefab;
    public List<PlayerController> _players = new List<PlayerController>();

    [FMODUnity.EventRef]
    public string music = "event:/MUSIQUE";


    [SerializeField]
	public Transform[] _playerSpawns;
	public Transform[] _collectibleSpawns;
	public Transform[] _ShipPieceSpawns;

	public float _startMoveSpeed = 1.9f;

	public float _sprintSpeed = 2f;

	public float _boostSpeed = 5f;

	public float _startStamina = 30f;
	public float _minStamina = 0f;
	public float _maxStamina = 100f;
	public float _regenStamina = 0.1f;
	public float _costStamina = 1.5f;
	public float _limitToUseStamina = 10f;

	bool _createCollectible;
	bool _createShipPieces;

	public GameObject _shipPieces;
    List<GameObject> _shipPiecesList = new List<GameObject>();
    #endregion

    #region Unity_methods

    void OnEnable() {
		Init();
	}

	//void Update() {

	//	if(_createShipPieces == false) {
	//		StartCoroutine(DelayCreateShipPieces());
	//	}
	//}


	#endregion

	#region Unity_methods

	void Init() {
        FMODUnity.RuntimeManager.PlayOneShot(music, Vector3.zero);

        FindShipPieces();

        int numberOfPlayers = Input.GetJoystickNames().Length;
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
			_players[i].name = "NukeMan_J" + _players[i]._playerIndex;
		}

		StartCoroutine (DelayCreateShipPieces());

	}

    public void FindShipPieces()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("IconeShip");
        for (int i = 0; i < temp.Length; ++i)
        {
            _shipPiecesList.Add(temp[i]);
            temp[i].SetActive(false);

        }
    }

    public void GenerateShipPiece() {
		if (_createShipPieces){
			for(int i = 0; i < 4; ++i) {

				GameObject newShipPiece = Instantiate(shipPiecePrefab, _ShipPieceSpawns[i]);
				newShipPiece.transform.parent = null;
			}
		}
	}

    public void AddShipPiece()
    {
        int random = UnityEngine.Random.Range(0, _shipPiecesList.Count);
        _shipPiecesList[random].SetActive(true);
        _shipPiecesList.RemoveAt(random);

        if (_shipPiecesList.Count == 0)
        {
            SceneManager.LoadScene("Win");
        }
    }

	IEnumerator DelayCreateShipPieces (){

			_createShipPieces = false;
		yield return new WaitForSeconds (16f);
		_createShipPieces = true;
		GenerateShipPiece ();
		StartCoroutine (DelayCreateShipPieces());
	}

	public void EndOfGame() {
		StartCoroutine(EndCoroutine());
	}

	public IEnumerator EndCoroutine() {
		yield return new WaitForSeconds(1.5f);

		if(_players.Count == 0) {
			SceneManager.LoadScene("Win");
		}

		StopCoroutine("EndOfGame");
	}

	#endregion
}
