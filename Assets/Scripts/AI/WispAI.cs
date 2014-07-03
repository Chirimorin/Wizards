using UnityEngine;
using System.Collections;

public class WispAI : MonoBehaviour {

	private GameObject player;
	private float absDeltaDistanceX;

	private float idleTimer;
	private Vector3 currentPosition, startPosition, playerCurrentPosition;
	private Vector3 idleMovement;
	private float speedx, speedy, randomX, randomY;

	public float fireSpeed = 12.5f;	
	public float chaseSpeed = 2.5f;
	public float chargeUpTimer;
	public float Health;

	// Use this for initialization
	void Start () {
		Health = 100f;
		startPosition = transform.position;
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
		//Debug.Log ("Timer is: " + idleTimer);
	}

	void MoveIdleState(){
		idleTimer += Time.deltaTime;
		idleMovement = new Vector3(speedx * Time.deltaTime, speedy * Time.deltaTime, 0);
		transform.position += idleMovement;
		bool newRandom = true;

		if(Mathf.Abs(transform.position.x - startPosition.x) < 0.5f && Mathf.Abs(transform.position.y - startPosition.y) < 0.5f){
			if(idleTimer > Random.Range (3f, 5f)){
				if(speedx != 0 || speedy != 0){newRandom = false;}
				if(newRandom){
					randomX = Random.Range (-0.25f, 0.25f);
					randomY = Random.Range (-0.25f, 0.25f);
					Debug.Log ("RandomX speed: " + randomX);
				}
				speedx = randomX; 
				speedy = randomY;
				if(idleTimer > Random.Range (5.1f, 6.1f)){
					idleTimer = Random.Range (-2f, 1f);
					newRandom = true;
					speedx = 0;
					speedy = 0;
				}
			}
		}else{
			currentPosition = transform.position;
			transform.position = Vector3.MoveTowards (currentPosition, startPosition, 1.0f * Time.deltaTime);
			Debug.Log ("Out of bounds");
		}
			
	}

	void MoveChasingState(){
		transform.position = Vector3.MoveTowards (transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
		playerCurrentPosition = player.transform.position;
	}

	void MoveAttackState(){
		chargeUpTimer += Time.deltaTime;
		if(chargeUpTimer > 1f){
			//transform.position = Vector3.MoveTowards (transform.position, playerCurrentPosition, fireSpeed * Time.deltaTime);
			
			Vector3 shootDirection = playerCurrentPosition - transform.position;
			Vector3 normShDir = shootDirection.normalized;
			rigidbody2D.AddForce(normShDir * 1000f * Time.deltaTime);
			if(playerCurrentPosition == transform.position){
				
				chargeUpTimer = 0;
			}
		}
	}

	void AddDamage(float damage){
		Health -= damage;
	}

}
