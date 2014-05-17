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
	
	//Jumping
	public int jumpheight;
	private bool isGrounded;
	private bool isGrounded2;

	public GameObject parent; 

	// Use this for initialization
	void Start () {
		parent = GameObject.Find ("NecroFT(Clone)");
		acceleration = 0.67f;
		targetSpeed = 3.5f;
		brakeSpeed = 1.00f;
	}
	
	// Update is called once per frame
	void Update () {
		float distance = parent.transform.position.x - transform.position.x;

		if (distance < -1.5) {
			xAxisMovement (-1);
		}
		else if (distance > 1.5) {
			xAxisMovement (1);
		}



		Debug.Log (distance);
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
