using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : Singleton<SpawnArea> {

	public int nbCollect = 0;
	bool _stopSpawning = false;

	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnCollectibles());
	}
	
	// Update is called once per frame
	void Update () {
		if (nbCollect < 5 && _stopSpawning) {
			StartCoroutine(SpawnCollectibles());
		}
	}

	IEnumerator SpawnCollectibles() {
		while(nbCollect < 12) {
			Instantiate(GameManager.Instance.collectiblePrefab, new Vector3(Random.Range(-13.5f, 7.5f), 0.25f, Random.Range(-9.5f, 9.0f)), Quaternion.identity);
			nbCollect += 1;
			yield return new WaitForSeconds(Random.Range(0.5f, 3.0f));
		}
		_stopSpawning = true;
		StopCoroutine(SpawnCollectibles());
	}
}
