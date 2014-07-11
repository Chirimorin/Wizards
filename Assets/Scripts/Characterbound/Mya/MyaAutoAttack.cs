using UnityEngine;
using System.Collections;

public class MyaAutoAttack : MeleeBase {

	// Use this for initialization
	new void Start () {
		width = 0.5f;
		height = 1f;
		offsetX = 0.5f;
		offsetY = 0f;

		damage = 49;
		knockback = 0;
		attackTime = 0.1f;
		attackCooldown = 0.2f;

		attackButton = "Fire1";

		base.Start ();
	}
	
	// Update is called once per frame
	new void Update () {

		base.Update ();
	}
}
