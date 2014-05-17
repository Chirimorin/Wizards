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

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "NecroFT(Clone)") {
			Debug.Log ("yo");
			Transform platform = transform.parent;
			Physics2D.IgnoreLayerCollision (8,9);
		}

	}

	void OnCollisionExit2D(Collision2D col){
		if (col.collider.name == "NecroFT(Clone)") {

		}




	}

}
