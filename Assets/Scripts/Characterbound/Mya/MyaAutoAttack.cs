using UnityEngine;
using System.Collections;

public class MyaAutoAttack : MeleeBase {

	// Use this for initialization
	new void Start () {
		width = 2;
		height = 2;
		offsetX = 0.5f;
		offsetY = -0.2f;

		base.Start ();
	}
	
	// Update is called once per frame
	new void Update () {

		base.Update ();
	}
}
