﻿using UnityEngine;
using System.Collections;

public class FourDirectional : MonoBehaviour {

	private CharControlMod MoveController;
	private float speedx;
	public GameObject projectile;
	private GameObject ProjectileInstance;

	private bool aired;

	private float timeStamp;
	private Animator anim;

	// Use this for initialization
	void Start () {
		MoveController = GetComponent<CharControlMod> ();
		anim = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		aired = MoveController.Aired ();

		speedx = MoveController.HorSpeed ();


		//shoot horizontal
		if(Time.time > timeStamp && Input.GetButtonDown ("Fire1") && Input.GetAxisRaw ("Vertical") == 0){
			timeStamp = Time.time + 0.5f;

			ShootHorizontalIdle ();
		}

		//JumpShoot
		if (Time.time > timeStamp && aired && Input.GetButtonDown ("Fire1") && Input.GetAxisRaw ("Vertical") == -1) {
			timeStamp = Time.time + 0.5f;
			JumpShoot ();
		}



		if (Time.time > timeStamp && Input.GetButtonDown ("Fire1") && Input.GetAxisRaw ("Vertical") == 1) {
			timeStamp = Time.time + 0.5f;
			ShootUp ();
		}
						


	}

	void ShootHorizontalIdle(){
		if (transform.lossyScale.x < 0 && speedx == 0) {
			anim.SetTrigger ("IdleShoot");
			ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (-1f, 0, 0f), Quaternion.Euler (0,0,0)) as GameObject;
			ProjectileInstance.rigidbody2D.AddForce(new Vector2(-50000,0) * Time.deltaTime);
		}else if (transform.lossyScale.x > 0 && speedx == 0) {
			anim.SetTrigger ("IdleShoot");
			ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (1f, 0, 0f), Quaternion.Euler (0,0,0)) as GameObject;
			ProjectileInstance.rigidbody2D.AddForce(new Vector2(50000,0) * Time.deltaTime);
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
