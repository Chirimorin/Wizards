using UnityEngine;
using System.Collections;

public class Platforming : MonoBehaviour {

	public GameObject parent;

	// Use this for initialization
	void Start () {
		parent = GameObject.Find ("NecroFT(Clone)");
	}
	
	// Update is called once per frame
	void Update () {
		//float distance = parent.transform.position.y - transform.position.y;
		//Debug.Log (Vector3.Distance(parent.transform.position, transform.position));

	}

	/*void OnCollisionEnter2D(Collision2D col){
		if (col.collider.name == "NecroFT(Clone)" && col.collider.transform.position.y < transform.position.y) {
			Debug.Log ("yo");
			Transform platform = transform.parent;
			Physics2D.IgnoreLayerCollision (8, 9);
		} else {
			Physics2D.IgnoreLayerCollision (8, 9, true);

		}

	}*/

	void OnTriggerEnter2D(Collider2D col){
		if (col.name == "NecroFT(Clone)") {
			Debug.Log ("whattup");
			Physics2D.IgnoreLayerCollision (8, 9);
		}


	}

	void OnTriggerExit2D(Collider2D col){


		if (col.name == "NecroFT(Clone)") {
			Debug.Log ("hallo");
		}




	}

}
