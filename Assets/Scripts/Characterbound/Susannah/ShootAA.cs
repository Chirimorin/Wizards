using UnityEngine;
using System.Collections;

public class ShootAA : ShootingBase {

	// Use this for initialization
	void Start () {
		gravityScale = 0;
		VerticalHoming = 0;
		power = 25f;
		cooldown = 0.3f;
		autofire = true;
		button = "Fire1";
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}
}
