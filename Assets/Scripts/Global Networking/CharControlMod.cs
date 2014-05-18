using UnityEngine;
using System.Collections;

public class CharControlMod : MonoBehaviour {

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
	private Raycast cast;
	// Camera
	public Camera cam;

	//Animation
	private Animator anim;


	//Networking vars
	private Vector3 syncStartPosition, syncNewPosition;
	private float lastSyncTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();

		cast = gameObject.GetComponent<Raycast> ();
		acceleration = 0.67f;
		targetSpeed = 5f;
		brakeSpeed = 1.00f;
	}
	
	// Update is called once per frame
	void Update () {

		//Set Animator variables
		anim.SetFloat ("H_Speed", Mathf.Abs (speedx));



		/*
		if (Network.peerType == NetworkPeerType.Client) {
			networkView.RPC ("xAxisMovement", RPCMode.Others);
			networkView.RPC ("Jump", RPCMode.Others);
		} else if (Network.peerType == NetworkPeerType.Server){			
			networkView.RPC ("xAxisMovement", RPCMode.Others);
			networkView.RPC ("Jump", RPCMode.Others);
		}*/
		
		if (networkView.isMine) {
			xAxisMovement ();
			Jump ();
			cam.enabled = true;
		} else {
			cam.enabled = false;
			SyncedMovement ();
		}
	}
	
	
	// Move char on x-axis
	
	void xAxisMovement(){
		axis = Input.GetAxisRaw ("Horizontal");
		transform.position += vec;
		vec = new Vector3 (speedx * Time.deltaTime, speedy, 0);
		
		if (axis < -0.5) {
			speedx -= acceleration;
			Vector3 rotate = transform.localScale;
			rotate.x = -0.5f;
			transform.localScale = rotate;
			if (speedx < targetSpeed * -1) {
				speedx = -1 * targetSpeed;
				speedx += 0;
			}
			
		} else if (axis > 0.5) {
			speedx += acceleration;
			Vector3 rotate2 = transform.localScale;
			rotate2.x = 0.5f;
			transform.localScale = rotate2;
			if(speedx > targetSpeed){
				speedx = targetSpeed;
				speedx += 0;
			}
			
		} else {
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
		if (Input.GetButtonDown ("Jump") && !Aired()) {
			rigidbody2D.AddForce(new Vector3 (0, jumpheight, 0));
		}
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){

		Vector3 storePosition = new Vector3();
		Vector3 positionVelocity = new Vector3 ();
		if (stream.isWriting) {
			storePosition = rigidbody2D.transform.position;
			stream.Serialize (ref storePosition);

			positionVelocity = rigidbody2D.velocity;
			stream.Serialize (ref positionVelocity);
		} else {
			stream.Serialize(ref storePosition);
			stream.Serialize (ref positionVelocity);
			syncTime = 0f;
			syncDelay = Time.time - lastSyncTime;
			lastSyncTime = Time.time;

			syncNewPosition = storePosition + positionVelocity * syncDelay;
			syncStartPosition = transform.position;


		}

	}

	void SyncedMovement(){
		syncTime += Time.deltaTime;
		rigidbody2D.transform.position = Vector3.Lerp (syncStartPosition, syncNewPosition, syncTime/syncDelay);
	}

	public float HorSpeed(){
		return speedx;
	}

	public bool Aired(){
		return (!(cast.collides ()) && !(cast.collides2 ()));
	}

}
