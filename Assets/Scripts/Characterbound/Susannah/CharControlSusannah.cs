using UnityEngine;
using System.Collections;

public class CharControlSusannah : CharControlBase {



	// Use this for initialization
	void Start () {
		base.Start ();
		acceleration = 0.67f;
		maxSpeed = 5f;
		brakeSpeed = 1f;
		jumpheight = 800;
		rigidbody2D.gravityScale = 2.1f;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	void FixedUpdate(){
		base.FixedUpdate ();
	}
}
