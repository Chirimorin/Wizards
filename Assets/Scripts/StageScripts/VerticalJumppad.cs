using UnityEngine;
using System.Collections;

public class VerticalJumppad : MonoBehaviour {

	public float power = 1600;
	private GameObject player;

	void Start(){

	}

	// Update is called once per frame
	void Update () {
		try{
			player = GameObject.Find ("NecroFT(Clone)");
		}catch(UnityException e){

		}
	}

	void OnCollisionEnter2D(Collision2D c){
		if(c.collider.name == "NecroFT(Clone)"){
			Debug.Log ("jump");
			player.rigidbody2D.AddForce(new Vector2(0, power));
		}
	}
}
