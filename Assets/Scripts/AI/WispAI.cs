using UnityEngine;
using System.Collections;

public class WispAI : MonoBehaviour {

	private GameObject player;
	private float absDeltaDistanceX;

	private float idleTimer;
	private Vector3 currentPosition;
	public float idleSpeed = 1.0f;

	public float Health;

	// Use this for initialization
	void Start () {
		Health = 100f;
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.Find ("NecroFT(Clone)");

		try{
			absDeltaDistanceX = Mathf.Abs(transform.position.x - player.transform.position.x);
		}catch(UnityException e){
			Debug.Log (e);
		}

		if (absDeltaDistanceX > 7f) {
			MoveIdleState ();
		} else if (absDeltaDistanceX < 7f && absDeltaDistanceX > 3f) {
			MoveChasingState ();
		} else {
			MoveAttackState (); 
		}
	}

	void MoveIdleState(){
		idleTimer += Time.deltaTime;

		if(idleTimer > Random.Range (3f, 5f)){
			idleTimer = 0;
			currentPosition = transform.position;
			float pathlength = Vector3.Distance (currentPosition, transform.position + new Vector3(-0.5f, 0, 0));
			float distanceCovered = idleSpeed * Time.deltaTime; 
			float fracJourney = distanceCovered / pathlength;
			Debug.Log (currentPosition);
			transform.position = Vector3.Lerp (currentPosition, transform.position + new Vector3(-5f, 0, 0), Time.deltaTime);
		}
	}

	void MoveChasingState(){

	}

	void MoveAttackState(){

	}

	void AddDamage(float damage){
		Health -= damage;
	}

}
