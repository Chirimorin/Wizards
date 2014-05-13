using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//horizontal handling
	public float speedx;
	public float speedy;
	public float acceleration;
	public float brakeSpeed;

	public float targetSpeed;
	private float axis;
	private Vector3 vec;

	//Jumping
	public int jumpheight;
	private bool isGrounded;
	private bool isGrounded2;
	private Raycast cast;

	// Animation
	//public Animator anim;

	// Use this for initialization
	void Start () {
		cast = gameObject.GetComponent<Raycast> ();
		//anim = gameObject.GetComponent<Animator> ();
		acceleration = 0.67f;
		targetSpeed = 3.5f;
		brakeSpeed = 1.00f;
	}
	
	// Update is called once per frame
	void Update () {
		 
		//vars that should update
		isGrounded = cast.collides ();
		isGrounded2 = cast.collides2 ();
		//anim.SetFloat("NecroSpeed", Mathf.Abs(speedx));

		/*
		if (Network.peerType == NetworkPeerType.Client) {
			networkView.RPC ("xAxisMovement", RPCMode.Others);
			networkView.RPC ("Jump", RPCMode.Others);
		} else if (Network.peerType == NetworkPeerType.Server){			
			networkView.RPC ("xAxisMovement", RPCMode.Others);
			networkView.RPC ("Jump", RPCMode.Others);
		}*/

		//if (networkView.isMine) {
			xAxisMovement ();
			Jump ();
		//}
	}


	// Move char on x-axis

	void xAxisMovement(){
		axis = Input.GetAxisRaw ("Horizontal");
		transform.position += vec;
		vec = new Vector3 (speedx * Time.deltaTime, speedy, 0);

		if (axis == -1) {
			speedx -= acceleration;
			Vector3 rotate = transform.localScale;
			rotate.x = -0.5f;
			transform.localScale = rotate;
			if (speedx < targetSpeed * -1) {
				speedx = -1 * targetSpeed;
				speedx += 0;
			}
			
		} else if (axis == 1) {
			speedx += acceleration;
			Vector3 rotate2 = transform.localScale;
			rotate2.x = 0.5f;
			transform.localScale = rotate2;
			if(speedx > targetSpeed){
				speedx = targetSpeed;
				speedx += 0;
			}
			
		} else if (axis == 0) {
			if (speedx <= targetSpeed && speedx > 0) {
				speedx -= brakeSpeed;
				if (speedx <= 0) {
					speedx = 0;
				}
			}
			if (speedx >= -targetSpeed && speedx < 0) {
				speedx += brakeSpeed;
				if (speedx >= 0) {
					speedx = 0;
				}
			}
		}
	}


	void Jump(){
		if (Input.GetButtonDown ("Jump") && isGrounded) {

			rigidbody2D.AddForce(new Vector3 (0, jumpheight, 0));

		}else if(Input.GetButtonDown("Jump") && isGrounded2){

			rigidbody2D.AddForce(new Vector3 (0, jumpheight, 0));
		}
	}

}
