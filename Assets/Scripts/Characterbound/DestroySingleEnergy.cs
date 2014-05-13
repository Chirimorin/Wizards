using UnityEngine;
using System.Collections;

public class DestroySingleEnergy : MonoBehaviour {

	public GameObject particle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (this.gameObject, 0.2f);
		Destroy (particle, 3f);
	}
	void OnCollisionEnter2D(Collision2D col){
		if (col.collider.name == "Cube") {
			Destroy (this.gameObject);
			Instantiate(particle, this.gameObject.transform.position, Quaternion.Euler (0,0,0));  
		} else if (col.collider.name == "Floor") {
			Destroy (this.gameObject);
		} else if (col.collider.name == "MoveTowards") {
			Destroy (this.gameObject);
			Instantiate(particle, this.gameObject.transform.position, Quaternion.Euler (0,0,0));
		}
			


	}


}
