using UnityEngine;
using System.Collections;

public class WispCoAI : MonoBehaviour {

	private Vector3 startPosition;
	private Vector3 diveAtPosition;
	private Vector3 normDiveAtPosition;
	
	private Vector3 DiveAtPosition{
		get{
			return diveAtPosition;
		}set{
			diveAtPosition = value;
		}
	}

	private float deltaDistanceX;
	private float deltaDistanceY;
	public float chaseSpeed = 0.1f;
	private bool diveAble;
	private bool chaseAble;
	private bool idleState;
	private bool fired;
	private float diveStateTimer = 0f;
	private float health = 20f;

	private GameObject player;
	private Healthbars playerHealth;

	private float idleTimer;
	private Vector3 idleMovement, currentPosition;
	private float speedx, speedy;
	private float randomX, randomY;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		diveAble = true;
		chaseAble = true;
		idleState = true;
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.FindGameObjectWithTag ("Player");

		try{
			deltaDistanceX = Mathf.Abs(transform.position.x - player.transform.position.x);
			deltaDistanceY = Mathf.Abs(transform.position.y - player.transform.position.y);
			playerHealth = player.GetComponent<Healthbars>();
		}catch(UnityException e){
				Debug.Log(e);		
		}


		//  
		if(deltaDistanceX < 9f && deltaDistanceX > 4f && chaseAble == true){
			idleState = false;
			diveStateTimer = 0f;
			StartCoroutine ("ChaseState");
			StopCoroutine ("DiveState");
		}else if(deltaDistanceX < 4f && diveAble == true && deltaDistanceY < 4f){
			idleState = false;
			StartCoroutine ("DiveState");
			StopCoroutine ("ChaseState");
		}

		if(fired){
			diveStateTimer += Time.deltaTime;
		}else{
			diveStateTimer = 0;
		}

		if(diveStateTimer > 0.7f){
			StopCoroutine ("DiveState");
			StartCoroutine ("BrakeState");
		}

		if(idleState){
			MoveIdleState();
		}

		if(diveStateTimer != 0){
			Debug.Log (diveStateTimer);
		}
		
		//Debug.Log (health);
		//Destroy instance on health = 0
		if(health <= 0){
			DestroyObject (this.gameObject);
		}

	}

	void MoveIdleState(){
		Physics2D.IgnoreLayerCollision (8, 11);
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
						//Debug.Log ("RandomX speed: " + randomX);
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
				transform.position = Vector3.MoveTowards (currentPosition, startPosition, 0.1f * Time.deltaTime);
				
			}
	}

	IEnumerator ChaseState(){
		while(true){
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
			yield return null;
		}
	}

	IEnumerator DiveState(){
		yield return RecoverState ();
		yield return new WaitForSeconds (1f);
		fired = true;
		DiveAtPosition = player.transform.position - transform.position;
		normDiveAtPosition = DiveAtPosition.normalized;
		rigidbody2D.AddForce (normDiveAtPosition * 1000f * Time.deltaTime);
		Physics2D.IgnoreLayerCollision (8, 11, false);
		yield break;
	}

	IEnumerator RecoverState(){
		Physics2D.IgnoreLayerCollision (8, 11);
		yield return new WaitForSeconds (1f);
		while(rigidbody2D.velocity.magnitude > 0.1f){
			rigidbody2D.velocity *= 0.9f;	
			yield return null;
		}
		yield return new WaitForSeconds (1f);
		fired = false;
		if (rigidbody2D.velocity.magnitude < 0.1f) {
			rigidbody2D.velocity = new Vector2 (0,0);
			diveAble = true;
			chaseAble = true;
		}

		yield break;
	}

	IEnumerator BrakeState(){
		while(rigidbody2D.velocity.magnitude > 0.1f){
			rigidbody2D.velocity *= 0.9f;	
			yield return null;
		}

		if (rigidbody2D.velocity.magnitude < 0.1f) {
			rigidbody2D.velocity = new Vector2 (0,0);
			diveAble = true;
			chaseAble = true;
			diveStateTimer = 0;
		}
		yield return new WaitForSeconds (1f);
		fired = false;
		yield break;

	}

	void OnCollisionEnter2D(Collision2D col){

		if(col.collider.name == "NecroFT(Clone)"){
			diveStateTimer = 0f;
			diveAble = false;
			chaseAble = false;
			StopCoroutine("DiveState");
			StopCoroutine("ChaseState");
			if(playerHealth){
				playerHealth.InflictDamage (10);
			}


		}

		StartCoroutine("RecoverState");

		if(col.collider.name == "Skull Projectile(Clone)"){
			inflictDamage (10);
		}
	}

	public void inflictDamage(float damage){
		health -= damage;
	}

}
