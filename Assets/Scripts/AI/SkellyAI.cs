using UnityEngine;
using System.Collections;

public class SkellyAI : MonoBehaviour {
	
	//horizontal handling
	public float speedx;
	public float speedy;
	public float acceleration;
	public float brakeSpeed;
	
	public float targetSpeed;
	public float targetIdleSpeed;
	private float axis;
	private Vector3 vec;

	public float maxDistance;
	public float teleportDistance;
	public float wanderPrecision;
	
	//Jumping
	public int jumpheight;

	private GameObject parent; 
	private float parentPosition;
	private float timer;
	private float goalDistance;
	private bool idle;
	private bool moving;

	private DetectionScript DS;

	// Use this for initialization
	void Start () {
		parent = GameObject.Find ("NecroFT(Clone)");
		acceleration = 0.67f;
		targetSpeed = 5f;
		targetIdleSpeed = 2f;
		brakeSpeed = 1.00f;
		maxDistance = 1.5f;
		teleportDistance = 3.5f; 
		wanderPrecision = 0.25f;

		timer = 0;
		idle = false;
		moving = false;
		goalDistance = 0;
		parentPosition = parent.transform.position.x;

		DS = this.GetComponent<DetectionScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, parent.transform.position) > teleportDistance && !(((CharControlMod)(parent.GetComponent("CharControlMod"))).Aired())) {
			Debug.Log("Teleporting to player...");
			transform.position = new Vector3(parent.transform.position.x + Random.Range (-0.5f, 0.5f), parent.transform.position.y, 0);
		}
		else {
			float distance = transform.position.x - parent.transform.position.x;

			if (distance < -maxDistance) {
				goalDistance = -maxDistance;
				xAxisMovement (1, targetSpeed);
				idle = false;
			}
			else if (distance > maxDistance) {
				goalDistance = maxDistance;
				xAxisMovement (-1, targetSpeed);
				idle = false;
			}
			else {
				// Idle mode
				if (idle) {
					timer -= Time.deltaTime;
					if (timer <= 0)
					{
						resetTimer(Random.Range (-maxDistance,maxDistance), distance);
						moving = true;
					}
				}
				else {
					idle = true;
					moving = true;
					resetTimer(distance, distance);
				}

				if (distance - goalDistance < -wanderPrecision && moving) {
					xAxisMovement (1, targetIdleSpeed);
				}
				else if (distance - goalDistance > wanderPrecision && moving) {
					xAxisMovement (-1, targetIdleSpeed);
				}
				else {
					xAxisMovement (0, 0);
					moving = false;
				}
			}
		}
	}

	void resetTimer(float newGoal, float distance){
		goalDistance = newGoal;
		timer = Random.Range(2f,6f);
		Debug.Log ("Timer set: " + timer + " Goal distance: " + goalDistance + " Distance to goal: " + (distance - goalDistance));
	}

	void xAxisMovement(int axis, float currentTargetSpeed){
		bool move;
		bool jump = false; 

		if ( DS.SolidInFront(0.5f) ||
		     !DS.IsGrounded() ||
		     transform.position.y > parent.transform.position.y ) 
		{
			move = true;
		} else {
			if ( DS.IsSolid(new Vector2 (parent.transform.position.x + goalDistance, transform.position.y), 1f) && 
			    (((transform.position.x - parent.transform.position.x - goalDistance) * -axis) > 0.5f)){
				Debug.Log(transform.position.x - parent.transform.position.x - goalDistance);
				jump = true;
				move = true;
			}
			else if ((transform.position.x - parent.transform.position.x - goalDistance) > 0.5f) {
				Debug.Log("distance: " + ((transform.position.x - parent.transform.position.x - goalDistance) * axis) + " move true");
				move = true;
			}
			else {
				Debug.Log("distance: " + ((transform.position.x - parent.transform.position.x - goalDistance) * axis) + " move false");
				move = false;
			}
		}

		if (axis == -1 && move) {
			speedx -= acceleration;
			Vector3 rotate = transform.localScale;
			//rotate.x = -0.5f;
			//transform.localScale = rotate;
			if (speedx < -currentTargetSpeed) {
				speedx = -currentTargetSpeed;
			}
			
		} else if (axis == 1 && move) {
			speedx += acceleration;
			Vector3 rotate2 = transform.localScale;
			//rotate2.x = 0.5f;
			//transform.localScale = rotate2;
			if(speedx > currentTargetSpeed){
				speedx = currentTargetSpeed;
			}
			
		} else {
			if (speedx > 0) {
				speedx -= brakeSpeed;
				if (speedx < 0) {
					speedx = 0;
				}
			}
			if (speedx < 0) {
				speedx += brakeSpeed;
				if (speedx > 0) {
					speedx = 0;
				}
			}
		}

		transform.position += new Vector3 (speedx * Time.deltaTime, speedy, 0);

		if (jump) {
			Jump ();
		}
	}

	void Jump(){
		Debug.Log ("Jump = " + DS.IsGrounded());
		if (DS.IsGrounded ()) {
			rigidbody2D.velocity += new Vector2(0,5); //.AddForce(new Vector3 (0, 100), ForceMode.Impulse);
		}
	}

//	bool IsGrounded() {
//		Vector2 pos1 = transform.position + bottomLeft; //new Vector2 (
//			//(transform.position.x - (((coll.size * 0.5f).x))),
//			//(transform.position.y - ((coll.size * 0.5f).y)));
//		Vector2 pos2 = transform.position + bottomRight; //new Vector2 (
//			//(transform.position.x + (((coll.size * 0.5f).x))),
//			//(transform.position.y - ((coll.size * 0.5f).y)));
//
//		Debug.DrawRay (pos1, -Vector2.up * 0.1f, Color.yellow);
//		Debug.DrawRay (pos2, -Vector2.up * 0.1f, Color.yellow);
//		return Physics2D.Raycast(pos1, -Vector2.up, 0.1f);
//	}
//
//	bool solidInFront(int axis) {
//		Vector2 pos = new Vector2 (
//			(transform.position.x + (((coll.size * 0.5f).x) * axis)),
//			(transform.position.y - ((coll.size * 0.5f).y)));
//		Debug.DrawRay (pos, -Vector2.up * 0.1f, Color.red);
//
//		return Physics2D.Raycast(pos, -Vector2.up, 0.1f, maskLayer);
//	}
//
//	bool goalIsSolid(){
//		Vector2 pos = new Vector2 (parent.transform.position.x + goalDistance, transform.position.y);
//		Debug.DrawRay (pos, -Vector2.up * 1, Color.green);
//		return Physics2D.Raycast(pos, -Vector2.up, 1f, maskLayer);
//	}
	
}
