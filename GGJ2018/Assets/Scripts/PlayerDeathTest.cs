using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathTest : MonoBehaviour {
	public bool Activate;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision Col) {

		if (Col.gameObject.tag == "Dino")
			gameObject.SetActive (Activate);

	}
}
