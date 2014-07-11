using UnityEngine;
using System.Collections;

public class VerticalJumppad : MonoBehaviour {

	public float power = 1000;

	void Start(){

	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D c){
		if(c.collider.gameObject.tag == "Player"){
			c.gameObject.rigidbody2D.velocity = new Vector2(c.gameObject.rigidbody2D.velocity.x, 0);
			c.gameObject.rigidbody2D.AddForce(new Vector2(0, power));
		}
	}
}
