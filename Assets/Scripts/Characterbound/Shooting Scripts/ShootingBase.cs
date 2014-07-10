using UnityEngine;
using System.Collections;

public class ShootingBase : MonoBehaviour {

	private CharControlBase MoveControl;
	protected float speedx;
	public float power; //* 10^3
	public GameObject projectile;
	protected GameObject ProjectileInstance;
	
	private bool aired;
	public float gravityScale = 0; //shoot in straight line at default
	public float homing;
	public float autofire;

	public float cooldown;
	private float timeStamp;
	private float verticalVelo;
	
	
	// Use this for initialization
	protected void Start () {
		MoveControl = GetComponent<CharControlBase> ();
	}
	
	// Update is called once per frame
	protected void Update () {

		//TODO Homing;
		//TODO Autofire;

		if(ProjectileInstance){
			ProjectileInstance.rigidbody2D.gravityScale = gravityScale;
		}

		verticalVelo = rigidbody2D.velocity.y;

		//shoot horizontal idle
		if(Time.time > timeStamp && Input.GetButtonDown ("Fire1") && Input.GetAxisRaw ("Vertical") == 0){
			timeStamp = Time.time + cooldown;
			
			ShootHorizontal ();
		}

		//JumpShoot
		if (Time.time > timeStamp && MoveControl.Aired && Input.GetButtonDown ("Fire1") && Input.GetAxisRaw ("Vertical") == -1) {
			timeStamp = Time.time + cooldown;
			JumpShoot ();
		}
		
		
		//shoot upward
		if (Time.time > timeStamp && Input.GetButtonDown ("Fire1") && Input.GetAxisRaw ("Vertical") == 1) {
			timeStamp = Time.time + cooldown;
			ShootUp ();
		}

	}
	
	protected void ShootHorizontal(){
		if (transform.lossyScale.x < 0) {
			ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (0, 0, 5f), Quaternion.Euler (0,0,0)) as GameObject;
			ProjectileInstance.rigidbody2D.AddForce(new Vector2(-1000 * power, 0) * Time.deltaTime);
		}else if (transform.lossyScale.x > 0) {
			ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (0, 0, 5f), Quaternion.Euler (0,0,0)) as GameObject;
			ProjectileInstance.rigidbody2D.AddForce(new Vector2(1000 * power,0) * Time.deltaTime);
		}

	}

	protected void JumpShoot(){
		ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (0, -1f, 0f), Quaternion.Euler (0,0,0)) as GameObject;
		ProjectileInstance.rigidbody2D.AddForce(new Vector2(0, -50000) * Time.deltaTime);
	}
	
	protected void ShootUp(){
		ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (0, 1f, 0f), Quaternion.Euler (0,0,0)) as GameObject;
		ProjectileInstance.rigidbody2D.AddForce(new Vector2(0, 50000) * Time.deltaTime);
	}

}
