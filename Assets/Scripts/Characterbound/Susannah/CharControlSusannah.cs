using UnityEngine;
using System.Collections;

public class CharControlSusannah : CharControlBase {



	// Use this for initialization
	void Start () {
		base.Start ();
		acceleration = 0.67f;
		maxSpeed = 5f;
		brakeSpeed = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
		base.Jump ();
	}

	void FixedUpdate(){
		base.FixedUpdate ();
	}
}
