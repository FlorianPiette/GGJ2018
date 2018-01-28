using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (lifeTime ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator lifeTime (){
		yield return new WaitForSeconds (Random.Range(4.0f, 8.0f));
		SpawnArea.Instance.nbCollect -= 1;
		Destroy (gameObject);
	}
}
