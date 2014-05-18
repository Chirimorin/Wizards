using UnityEngine;
using System.Collections;

public class Platforming : MonoBehaviour {



	private bool triggered;
	private float verticalSpeed;
	private GameObject parent;

	// Use this for initialization
	void Start () {

		triggered = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Update the parent
		parent = GameObject.Find("NecroFT(Clone)");

		verticalSpeed = parent.rigidbody2D.velocity.y;

		if (verticalSpeed < 1) {
			triggered = false;
		}
		Debug.Log (verticalSpeed);
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

}
