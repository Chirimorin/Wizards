using UnityEngine;
using System.Collections;

public class PlayerPlatforming : MonoBehaviour {

	private bool triggered;
	private GameObject platform;

	// Use this for initialization
	void Start () {
		triggered = false; 
		platform = GameObject.Find ("Oneway Platform");
	}
	
	// Update is called once per frame
	void Update () {

		if(Vector3.Distance(transform.position, platform.transform.position) > 3f){
			Debug.Log ("disable");
		}else{

		}
	}

	void OnTriggerEnter2D(Collider2D c){
		if(c.name == "Oneway Platform"){
			Debug.Log ("Player enters trigger");
		}

	}

	void OnTriggerExit2D(Collider2D c){
		if(c.name == "Oneway Platform"){
			Debug.Log ("Player leaves trigger");
		}
		
	}

	void OnTriggerStay2D(Collider2D c){
		if(c.name == "Oneway Platform"){
			Debug.Log ("Player is in trigger");
		}
		
	}
}
