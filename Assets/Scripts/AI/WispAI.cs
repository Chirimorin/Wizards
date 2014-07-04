using UnityEngine;
using System.Collections;

public class WispAI : MonoBehaviour {

	private GameObject player;
	private float absDeltaDistanceX;

	private float idleTimer, recoverTimer;
	private Vector3 currentPosition, startPosition, playerCurrentPosition, normShDir, shootDirection;
	private Vector3 idleMovement;
	private float speedx, speedy, randomX, randomY;

	public float fireSpeed = 12.5f;	
	public float chaseSpeed = 2.5f;
	public float chargeUpTimer;
	public float Health;

	private Healthbars bars;
	private Collider2D wispCollider;

	private bool recoverState;
	private bool attack;
	
	// Use this for initialization
	void Start () {
		Health = 100f;
		startPosition = transform.position;
		wispCollider = GetComponent<Collider2D>();
		recoverState = false;
		attack = false;
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.Find ("NecroFT(Clone)");

		try{
			absDeltaDistanceX = Mathf.Abs(transform.position.x - player.transform.position.x);
			bars = player.GetComponent ("Healthbars") as Healthbars;
		}catch(UnityException e){
			Debug.Log (e);
		}

		if (absDeltaDistanceX > 7f && !recoverState) {
			MoveIdleState ();
		} else if (absDeltaDistanceX < 7f && absDeltaDistanceX > 3f && !recoverState) {
			StartCoroutine("ChasingNumerator");
		} else if(recoverState){
			RecoverState(); 
		}else{
			StartCoroutine("DiveNumerator");
			StopCoroutine ("ChasingNumerator");
		}

		if(recoverState){
			Debug.Log (rigidbody2D.velocity);
		}
		//Debug.Log (recoverTimer);
	}

	void MoveIdleState(){
		if(!attack){
			wispCollider.enabled = false;
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
			Debug.Log ("Idle State");
		}
	}

	void MoveChasingState(){
		if(!recoverState && !attack){
			wispCollider.enabled = false;

			playerCurrentPosition = player.transform.position;
			Debug.Log ("Chase State");
		}
	}

	IEnumerator ChasingNumerator(){
		while(Vector3.Distance (transform.position, player.transform.position) > 3f){
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
			yield return null;
		}
	}

	IEnumerator DiveNumerator(){
		yield return new WaitForSeconds (1f);

		shootDirection = playerCurrentPosition - transform.position;
		normShDir = shootDirection.normalized;
		rigidbody2D.AddForce (normShDir * 1000f * Time.deltaTime);
	}

	void MoveAttackState(){
		if(!recoverState){
			attack = true;
			wispCollider.enabled = true;
			chargeUpTimer += Time.deltaTime;
				shootDirection = playerCurrentPosition - transform.position;
				normShDir = shootDirection.normalized;
				rigidbody2D.AddForce(normShDir * 1000f * Time.deltaTime);

				if(chargeUpTimer > 2f){
					chargeUpTimer = 0;
					recoverState = true;
					attack = false;
				}

		}Debug.Log ("Attack State");
	}

	void RecoverState(){
		recoverTimer += Time.deltaTime;

		if(recoverTimer > 0.3f && recoverTimer < 2f){
			rigidbody2D.velocity *= 0.9f;
		}
		
		if(recoverTimer > 2f && recoverTimer < 3.5f){
			//Debug.Log ("Wake?");
			rigidbody2D.Sleep ();
		}

		if(recoverTimer > 3.5f){
			transform.position = Vector3.MoveTowards (transform.position, startPosition, chaseSpeed * Time.deltaTime);
			recoverState = false;
		} 
		wispCollider.enabled = false;
		chargeUpTimer = 0;
		Debug.Log ("recovering");
	}

	void AddDamage(float damage){
		Health -= damage;
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.collider.name == "NecroFT(Clone)" && attack == true){
			attack = false;
			recoverState = true;
			bars.InflictDamage (20f);
			chargeUpTimer = 0;

		}
	}
}
