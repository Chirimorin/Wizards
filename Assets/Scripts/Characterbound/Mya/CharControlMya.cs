using UnityEngine;
using System.Collections;

public class CharControlMya : CharControlBase {


	// Use this for initialization
	new void Start () {
		acceleration = 0.67f;
		maxSpeed = 5f;
		brakeSpeed = 1f;
		jumpheight = 500;
		baseGravity = 2.1f;
		floatyness = 0.25f;
		numJumps = 2;

		base.Start ();
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
	}

	new void FixedUpdate(){
		base.FixedUpdate ();
	}
}
