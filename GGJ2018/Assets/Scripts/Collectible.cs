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
		yield return new WaitForSeconds (8f);
		Destroy (gameObject);
	}
}
