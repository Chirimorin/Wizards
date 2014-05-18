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
	private bool isGrounded;
	private bool isGrounded2;


	private GameObject parent; 
	private float parentPosition;
	private float timer;
	private float goalDistance;
	private bool idle;
	private bool moving;


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
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, parent.transform.position) > teleportDistance && !(((CharControlMod)(parent.GetComponent("CharControlMod"))).Aired())) {
			transform.position = new Vector3(parent.transform.position.x + Random.Range (-0.5f, 0.5f), parent.transform.position.y, 0);
		}
		else {
			float distance = transform.position.x - parent.transform.position.x;

			if (distance < -maxDistance) {
				xAxisMovement (1, targetSpeed);
				idle = false;
			}
			else if (distance > maxDistance) {
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
		transform.position += vec;
		vec = new Vector3 (speedx * Time.deltaTime, speedy, 0);

		if (axis == -1 && Physics2D.Raycast(transform.position + new Vector3(-0.5f, 0f), -Vector2.up, 1f)) {
			speedx -= acceleration;
			Vector3 rotate = transform.localScale;
			rotate.x = -0.5f;
			transform.localScale = rotate;
			if (speedx < -currentTargetSpeed) {
				speedx = -currentTargetSpeed;
			}
			
		} else if (axis == 1 && Physics2D.Raycast(transform.position + new Vector3(0.5f, 0f), -Vector2.up, 1f)) {
			speedx += acceleration;
			Vector3 rotate2 = transform.localScale;
			rotate2.x = 0.5f;
			transform.localScale = rotate2;
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
	}
	
}
