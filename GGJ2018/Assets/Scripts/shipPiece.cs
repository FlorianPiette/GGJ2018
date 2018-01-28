using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipPiece : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (lifeTime ());
	}
	
	IEnumerator lifeTime (){
		yield return new WaitForSeconds (Random.Range(7.0f, 12.0f));
		Destroy (gameObject);
	}
}
