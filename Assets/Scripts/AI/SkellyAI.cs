using UnityEngine;
using System.Collections;

public class SkellyAI : MonoBehaviour {

	//horizontal handling
	public float speedx;
	public float speedy;
	public float acceleration;
	public float brakeSpeed;
	
	public float targetSpeed;
	private float axis;
	private Vector3 vec;

	public float maxDistance;
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
		targetSpeed = 3.5f;
		brakeSpeed = 1.00f;
		maxDistance = 1.5f;
		wanderPrecision = 0.25f;

		timer = 0;
		idle = false;
		moving = false;
		goalDistance = 0;
		parentPosition = parent.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		float distance = transform.position.x - parent.transform.position.x;

		if (distance < -maxDistance) {
			xAxisMovement (1);
			idle = false;
		}
		else if (distance > maxDistance) {
			xAxisMovement (-1);
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
				xAxisMovement (1);
			}
			else if (distance - goalDistance > wanderPrecision && moving) {
				xAxisMovement (-1);
			}
			else {
				xAxisMovement (0);
				moving = false;
			}
		}
	}

	void resetTimer(float newGoal, float distance){
		goalDistance = newGoal;
		timer = Random.Range(2f,6f);
		Debug.Log ("Timer set: " + timer + " Goalposition: " + goalDistance + " Distance to goal: " + (distance - goalDistance));
	}

	void xAxisMovement(int axis){
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
	
}
