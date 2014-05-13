using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour {

	public Transform rayStart, rayEnd, rayEnd2, rayStart2;
	public bool isGrounded, isGrounded2;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine (rayStart.position, rayEnd.position, Color.cyan);
		isGrounded = Physics2D.Linecast (rayStart.position, rayEnd.position); 
		Debug.DrawLine (rayStart2.position, rayEnd2.position, Color.cyan);
		isGrounded2 = Physics2D.Linecast (rayStart2.position, rayEnd2.position); 
	}

	public bool collides(){
		return isGrounded;
	}

	public bool collides2(){
		return isGrounded2;
	}
}
