using UnityEngine;
using System.Collections;

public class FourDirectional : MonoBehaviour {

	private CharControlMod MoveController;
	private float speedx;
	public GameObject projectile;
	private GameObject ProjectileInstance;

	private float timeStamp;
	private Animator anim;
	private Animator secondanoim;
	// Use this for initialization
	void Start () {
		MoveController = GetComponent<CharControlMod> ();
		anim = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {


		speedx = MoveController.HorSpeed ();

		if(Time.time > timeStamp && Input.GetButtonDown ("Fire1")){
			timeStamp = Time.time + 0.5f;
			anim.SetTrigger ("IdleShoot");
			ShootHorizontalIdle ();
		}

		JumpShoot ();
		ShootUp ();

	}

	void ShootHorizontalIdle(){
		if (transform.lossyScale.x < 0 && speedx == 0) {
			ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (-1f, 0, 0f), Quaternion.Euler (0,0,0)) as GameObject;
			ProjectileInstance.rigidbody2D.AddForce(new Vector2(-50000,0) * Time.deltaTime);
		}else if (transform.lossyScale.x > 0 && speedx == 0) {
			ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (1f, 0, 0f), Quaternion.Euler (0,0,0)) as GameObject;
			ProjectileInstance.rigidbody2D.AddForce(new Vector2(50000,0) * Time.deltaTime);
		}


	}

	void JumpShoot(){

	}

	void ShootUp(){

	}




}
