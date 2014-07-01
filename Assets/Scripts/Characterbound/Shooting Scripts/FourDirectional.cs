using UnityEngine;
using System.Collections;

public class FourDirectional : MonoBehaviour {

	private CharControlMod MoveController;
	private float speedx;
	public GameObject projectile;
	private GameObject ProjectileInstance;

	private bool aired;

	private float timeStamp;
	private Animator anim;
	private Animator ProjectileAnim;
	private float verticalVelo;


	// Use this for initialization
	void Start () {
		MoveController = GetComponent<CharControlMod> ();
		anim = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		verticalVelo = rigidbody2D.velocity.y;
		//Debug.Log (verticalVelo);

		aired = MoveController.Aired ();

		speedx = MoveController.HorSpeed ();


		//shoot horizontal idle
		if(Time.time > timeStamp && Input.GetButtonDown ("Fire1") && Input.GetAxisRaw ("Vertical") == 0 && speedx == 0){
			timeStamp = Time.time + 0.5f;

			ShootHorizontalIdle ();
		}

		//JumpShoot
		if (Time.time > timeStamp && aired && Input.GetButtonDown ("Fire1") && Input.GetAxisRaw ("Vertical") == -1) {
			timeStamp = Time.time + 0.5f;
			JumpShoot ();
		}


		//shoot upward
		if (Time.time > timeStamp && Input.GetButtonDown ("Fire1") && Input.GetAxisRaw ("Vertical") == 1) {
			timeStamp = Time.time + 0.5f;
			ShootUp ();
		}
					
		//shoot horizontal while moving
		if(Time.time > timeStamp && Input.GetButtonDown ("Fire1") && Input.GetAxisRaw ("Vertical") == 0 && Mathf.Abs (speedx) > 0){
			timeStamp = Time.time + 0.5f;
			
			ShootHorizontalMoving ();
		}


	}

	void ShootHorizontalIdle(){
		if (transform.lossyScale.x < 0) {
			anim.SetTrigger ("IdleShoot");
			ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (-1, 0, 0f), Quaternion.Euler (0,0,0)) as GameObject;
			ProjectileInstance.rigidbody2D.AddForce(new Vector2(-25000, 0) * Time.deltaTime);
			ProjectileInstance.transform.localScale = new Vector3(-1, 1, 1);

			//will figure this out later
			//ProjectileInstance.transform.position = Vector3.Lerp (transform.position + new Vector3 (-1f, 0, 0f),(Vector3)(ProjectileInstance.transform.position + new Vector3(-5,0,0)), 200.0f);
		}else if (transform.lossyScale.x > 0) {
			anim.SetTrigger ("IdleShoot");
			ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (1f, 0, 0f), Quaternion.Euler (0,0,0)) as GameObject;
			ProjectileInstance.rigidbody2D.AddForce(new Vector2(25000,0) * Time.deltaTime);
		}


	}

	void ShootHorizontalMoving(){
		if (transform.lossyScale.x < 0 ) {
			ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (-1f, 0, 0f), Quaternion.Euler (0,0,0)) as GameObject;
			ProjectileInstance.rigidbody2D.AddForce(new Vector2(-25000f, 0) * Time.deltaTime);
			ProjectileInstance.transform.localScale = new Vector3(-1, 1, 1);
		}
		if (transform.lossyScale.x > 0 ) {
			ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (1f, 0, 0f), Quaternion.Euler (0,0,0)) as GameObject;
			ProjectileInstance.rigidbody2D.AddForce(new Vector2(25000, 0) * Time.deltaTime);
		}


	}


	void JumpShoot(){
		ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (0, -1f, 0f), Quaternion.Euler (0,0,0)) as GameObject;
		ProjectileInstance.rigidbody2D.AddForce(new Vector2(0, -50000) * Time.deltaTime);
	}

	void ShootUp(){
		ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (0, 1f, 0f), Quaternion.Euler (0,0,0)) as GameObject;
		ProjectileInstance.rigidbody2D.AddForce(new Vector2(0, 50000) * Time.deltaTime);
	}


}
