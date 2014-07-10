using UnityEngine;
using System.Collections;

public class CharControlSusannah : CharControlBase {

	// Use this for initialization
	void Start () {
		acceleration = 0.67f;
		maxSpeed = 5f;
		brakeSpeed = 1f;
		jumpheight = 800;
		baseGravity = 2.1f;
		floatyness = 0;
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
