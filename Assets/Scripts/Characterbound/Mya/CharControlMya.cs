using UnityEngine;
using System.Collections;

public class CharControlMya : CharControlBase {


	// Use this for initialization
	void Start () {
		acceleration = 0.67f;
		maxSpeed = 5f;
		brakeSpeed = 1f;
		jumpheight = 500;
		baseGravity = 1f;
		floatyness = 0.5f;

		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	void FixedUpdate(){
		base.FixedUpdate ();
	}
}
