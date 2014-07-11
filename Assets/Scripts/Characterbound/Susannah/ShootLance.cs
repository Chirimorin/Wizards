using UnityEngine;
using System.Collections;

public class ShootLance : ShootingBase {

	// Use this for initialization
	void Start () {
		gravityScale = 0;
		VerticalHoming = 0;
		power = 50f;
		cooldown = 2f;
		autofire = false;
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}
}
