using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoChargeTest : MonoBehaviour {

	public Rigidbody Rigid;
	public GameObject ObjectToFollow;
	[Range (0, 10.0f)]
	public float FollowSpeed;
	public float ChargeSpeed;
	private Vector3 Direction;

	bool CanCharge = false;

	public float WaitTime;

	// Use this for initialization
	void Start () {
		
		Rigid = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Rigid.velocity = new Vector3 (0, 0, 0);
		
		if (ObjectToFollow == null || ObjectToFollow.activeSelf == false)
			SelectNextTarget ();
		else {
			if (CanCharge == false) {
				StartCoroutine (TimeToWait ());
				Rigid.transform.LookAt (ObjectToFollow.transform.position);
			}

			Charge ();
		}	
		
	}
	void OnTriggerEnter (Collider Col){
		
		if (Col.gameObject.tag == (Tags._player)) {
			//StartCoroutine (TimeToWait ());
			ObjectToFollow = Col.gameObject;
		}
	}
	void OnCollisionEnter (Collision Col){

		if (Col.gameObject.tag == (Tags._wall)) {
			CanCharge = false;
			Rigid.velocity = new Vector3 (0, 0, 0);
			//Debug.Log (Rigid.velocity);
		    }

	}
	void OnCollisionStay (Collision Col){

		if (Col.gameObject.tag == (Tags._wall)) {
			CanCharge = false;
			Rigid.velocity = new Vector3 (0, 0, 0);
			}
	}

#region The Charge
	void Charge (){

		//Look At Follow Rotate to follow (dont care of rigidbody constraint
		if (CanCharge == true) {
			
			Rigid.transform.Translate (0, 0, ChargeSpeed * Time.deltaTime);

		}
			Debug.DrawLine (transform.position, ObjectToFollow.transform.position, Color.red);		
	}
#endregion

#region Select Nearest Player
	void SelectNextTarget(){ //Select the NearestTarget
		GameObject[] PotentialTarget;
		float SaveDist = -1.0f;
		GameObject NearestPlayer = null;

		PotentialTarget = GameObject.FindGameObjectsWithTag ("Player");

		//Debug.Log (PotentialTarget[0] + " " + PotentialTarget[1]);

		for (int i = 0; i < PotentialTarget.Length; i++) {
			float Dist = -1.0f;
			float Result = -1.0f;

			//Debug.Log (PotentialTarget [i].activeSelf);

			if (PotentialTarget [i].activeSelf == true)


				Dist = Vector3.Distance (transform.position, PotentialTarget [i].transform.position);
			if (SaveDist == -1.0f) {

				SaveDist = Dist;
				NearestPlayer = PotentialTarget [i].gameObject;
			}
			else {

				Result = Mathf.Min (SaveDist, Dist);

			} if (Result != SaveDist) {
				Result = SaveDist;
				NearestPlayer = PotentialTarget [i].gameObject;

			}

		}
		ObjectToFollow = NearestPlayer ;
	}
#endregion

#region Coroutine Wait Dino
	IEnumerator TimeToWait () {
		float i = 0;
		while (i < WaitTime) {

			i++;
			//i+= Time.deltaTime;
			//yield return new  WaitForFixedUpdate ();
			yield return new WaitForSeconds (1.0f);
		}
		//Debug.Log ("DONE WAIT");
		CanCharge = true;

	}
#endregion

}