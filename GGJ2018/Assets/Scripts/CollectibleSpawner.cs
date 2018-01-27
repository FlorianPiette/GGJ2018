using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour {

	public bool _createCollectible;

	void Start () {
		_createCollectible = false;
		StartCoroutine (DelayCreateStamina());
	}

	public void CreateStamina ()
	{
		if (_createCollectible) {
			GameObject newCollectible = Instantiate(GameManager.Instance.collectiblePrefab, transform);
			newCollectible.transform.SetParent(transform);
		}
	}

	IEnumerator DelayCreateStamina (){
		_createCollectible = false;
		yield return new WaitForSeconds (12f);
		_createCollectible = true;
		CreateStamina ();
		StartCoroutine (DelayCreateStamina());
	}
}
