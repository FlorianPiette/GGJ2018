using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoChargeTest : MonoBehaviour {

    [FMODUnity.EventRef]
    private string focusDino_sfx = "event:/GGJ_2018_FOCUS_DINO";
    [FMODUnity.EventRef]
    private string dashDino = "event:/GGJ_2018_DASH_DINO";
    [FMODUnity.EventRef]
    private string prepDashDino = "event:/GGJ_2018_PREP_DASH_DINO";

	Animator anim;
    public Rigidbody Rigid;
	public GameObject ObjectToFollow;
	[Range (0, 50.0f)]
	public float FollowSpeed;
	public float ChargeSpeed;
	private Vector3 Direction;

	bool CanCharge = false;

	public float WaitTime;
    private bool canPlayDashSound = true;
    private bool isPreparingCharge = false;

    // Use this for initialization
    void Start () {
		
		Rigid = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Rigid.velocity = new Vector3 (0, 0, 0);
		
		if (ObjectToFollow == null || ObjectToFollow.activeSelf == false)
			SelectNextTarget ();
		else
        {
			if (!CanCharge && !isPreparingCharge)
            {
				StartCoroutine (TimeToWait ());
                isPreparingCharge = true;
			} else if (isPreparingCharge)
            {
                Rigid.transform.LookAt(ObjectToFollow.transform.position);
            }

			Charge ();
		}	
		
	}
	void OnTriggerEnter (Collider Col){
		
		if (Col.gameObject.tag == (Tags._player)) {
			//StartCoroutine (TimeToWait ());
			ObjectToFollow = Col.gameObject;

            FMODUnity.RuntimeManager.PlayOneShot(focusDino_sfx, Vector3.zero);

        }
    }
	void OnCollisionEnter (Collision Col){

		if (Col.gameObject.tag == (Tags._wall)) {
			CanCharge = false;
			Rigid.velocity = new Vector3 (0, 0, 0);

            canPlayDashSound = true;
			anim.SetBool("Charging", false);
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

        if (canPlayDashSound == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot(dashDino, Vector3.zero);
            canPlayDashSound = false;

		}


		//Look At Follow Rotate to follow (dont care of rigidbody constraint
		if (CanCharge == true) {
			
			Rigid.transform.Translate (0, 0, ChargeSpeed * Time.deltaTime);
			anim.SetBool("Charging", true);

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

		anim.SetBool("Wait", true);
		float i = 0;
        //Wait the whole time minus 1, because he'll cry 1s before dashing 
		while (i < WaitTime -1) {

			i++;
			//i+= Time.deltaTime;
			//yield return new  WaitForFixedUpdate ();
			yield return new WaitForSeconds (1.0f);
		}

        //cry then wait one last sec
        FMODUnity.RuntimeManager.PlayOneShot(prepDashDino, Vector3.zero);
		anim.SetTrigger("Roar");
        yield return new WaitForSeconds(2.0f);

        CanCharge = true;
        isPreparingCharge = false;

    }
	#endregion

	#region Animation Kill

	public void PlayerKill() {

		anim.SetTrigger("Kill");
	}

	#endregion

}