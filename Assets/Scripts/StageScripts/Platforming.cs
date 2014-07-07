using UnityEngine;
using System.Collections;

public class Platforming : MonoBehaviour {



	private static bool triggered;
	private float verticalSpeed;
	private GameObject parent;

	private static float verticalDifference;


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
			Physics2D.IgnoreLayerCollision (8,9);
		}else if (verticalSpeed < 1 && verticalDifference > 0.7f && verticalDifference < 1.2f) {
			triggered = false;
		}


		//Debug.Log (verticalSpeed);
		if (!triggered) {
			Physics2D.IgnoreLayerCollision (8, 9, false);
		} else {
			Physics2D.IgnoreLayerCollision (8,9);
		}


		//shut trigger off niet vergeten on exit/enter


		//Debug.Log (verticalDifference);
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

	void OnTriggerExit2D(Collider2D col){
		if (col.name == "NecroFT(Clone)") {
			Debug.Log ("exit");
			//triggered = false;
		}
	}


}
