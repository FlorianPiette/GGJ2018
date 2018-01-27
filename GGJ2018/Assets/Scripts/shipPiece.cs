using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipPiece : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (lifeTime ());
	}
	
	IEnumerator lifeTime (){
		yield return new WaitForSeconds (7f);
		Destroy (gameObject);
	}
}
