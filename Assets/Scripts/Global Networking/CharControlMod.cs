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
	private float offGroundTimer;
	// Camera
	public Camera cam;

	//Animation
	private Animator anim;

	//Platforming
	private float verticalVelocity;

	//Collisions
	private Vector3 getHorPos;


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

		//Vertical velocity in case needed
		verticalVelocity = rigidbody2D.velocity.y;
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
		
		//if (networkView.isMine) {
		//xAxisMovement ();
		Jump ();
		if (Aired ()) {
			offGroundTimer += Time.deltaTime;
			//Debug.Log (offGroundTimer);
		} else {
			offGroundTimer = 0;
		}

		if(Input.GetButton ("Jump") && verticalVelocity < -0.1){
			Debug.Log (verticalVelocity);
			rigidbody2D.gravityScale = 0.8f;
		}else{
			rigidbody2D.gravityScale = 1f;
		}
			//cam.enabled = true;
		//} else {
			//cam.enabled = false;
			//SyncedMovement ();
		//}
	}

	void FixedUpdate(){
		xAxisMovement ();
		//if (Input.GetKeyDown ("a")) {
		//	rigidbody2D.position += new Vector2(2.0f, 0);
		//}
	}
	
	// Move char on x-axis
	
	void xAxisMovement(){
		axis = Input.GetAxisRaw ("Horizontal");
		//rigidbody2D.position += Vector2.right * speedx *Time.deltaTime;
		//rigidbody2D.MovePosition (rigidbody2D.position + (Vector2) vec );
		transform.position += vec;
		vec = new Vector3 (speedx * Time.deltaTime, speedy, 0);
		getHorPos = transform.position;

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
		if (Input.GetButtonDown ("Jump") && /*!Aired ()*/offGroundTimer < 0.1f && verticalVelocity < 0.1f && Input.GetAxisRaw ("Vertical") != -1) {
			rigidbody2D.AddForce (new Vector3 (0, jumpheight, 0));
		} else if (Input.GetButtonDown ("Jump") && Input.GetAxisRaw ("Vertical") == -1) {
			return;
		}
	}

	/*void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){

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
	}*/

	void OnCollisionEnter2D(Collision2D col){
		if(col.collider.tag == "Wall"){
			speedx = 0;
		}
	
	}

	//void OnCollisionStay2D(Collision2D col){
		//if(col.collider.tag == "Wall"){
			//speedx = 0;
	//	}
	//}

	public float HorSpeed(){
		return speedx;
	}

	public bool Aired(){
		return (!(cast.collides ()) && !(cast.collides2 ()));
	}

	public float GetVerticalSpeed(){
		return verticalVelocity; 
	}
}
