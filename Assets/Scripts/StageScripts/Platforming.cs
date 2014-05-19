using UnityEngine;
using System.Collections;

public class Platforming : MonoBehaviour {



	private bool triggered;
	private float verticalSpeed;
	private GameObject parent;

	private float verticalDifference;


	// Use this for initialization
	void Start () {

		triggered = false;

	}
	
	// Update is called once per frame
	void Update () {
		//Update the parent

			parent = GameObject.Find("NecroFT(Clone)");


		try{
		verticalSpeed = parent.rigidbody2D.velocity.y;
		verticalDifference = -1 * (transform.position.y - parent.transform.position.y);
		}catch(System.Exception e){

		}




		if(verticalDifference > 0.70f && Input.GetButtonDown ("Jump") && Input.GetAxisRaw ("Vertical") == -1){ //drop logix
			Debug.Log ("Drop");
			triggered = false;
		}else if (verticalSpeed < 1 && verticalDifference > 0.70f) {
			triggered = false;
		}

		//Debug.Log (verticalSpeed);
		if (!triggered) {
			Physics2D.IgnoreLayerCollision (8, 9, false);
		} else {
			Physics2D.IgnoreLayerCollision (8,9);
		}





		//Debug.Log (triggered);
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.name == "NecroFT(Clone)") {
			Debug.Log ("enter");
			triggered = true;
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.name == "NecroFT(Clone)") {
			triggered = true;

		}


	}



}
