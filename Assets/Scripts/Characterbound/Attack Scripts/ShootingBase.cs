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
	public float VerticalHoming;
	public bool autofire;

	public float cooldown;
	private float timeStamp;
	private float verticalVelo;
	public string button = "Fire1";

	protected GameObject[] enemies;
	
	// Use this for initialization
	protected void Start () {
		MoveControl = GetComponent<CharControlBase> ();
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
	}
	
	// Update is called once per frame
	protected void Update () {

		//TODO Homing improvement;
		foreach(GameObject enemy in enemies){
			if(enemy){
				if(Vector3.Distance (transform.position, enemy.transform.position) < 4f && VerticalHoming != null && ProjectileInstance){
					ProjectileInstance.rigidbody2D.AddRelativeForce (new Vector2(0, VerticalHoming));
				}
			}
		}



		if(ProjectileInstance){
			ProjectileInstance.rigidbody2D.gravityScale = gravityScale;
		}

		verticalVelo = rigidbody2D.velocity.y;
		if(!autofire){
			//shoot horizontal idle
			if(Time.time > timeStamp && Input.GetButtonDown (button) && Input.GetAxisRaw ("Vertical") == 0){
				timeStamp = Time.time + cooldown;
				
				ShootHorizontal ();
			}

			//JumpShoot
			if (Time.time > timeStamp && MoveControl.Aired && Input.GetButtonDown (button) && Input.GetAxisRaw ("Vertical") == -1) {
				timeStamp = Time.time + cooldown;
				JumpShoot ();
			}
			
			
			//shoot upward
			if (Time.time > timeStamp && Input.GetButtonDown (button) && Input.GetAxisRaw ("Vertical") == 1) {
				timeStamp = Time.time + cooldown;
				ShootUp ();
			}
		}else{
			if(Time.time > timeStamp && Input.GetButton (button) && Input.GetAxisRaw ("Vertical") == 0){
				timeStamp = Time.time + cooldown;
				
				ShootHorizontal ();
			}
			
			//JumpShoot
			if (Time.time > timeStamp && MoveControl.Aired && Input.GetButton ("Fire1") && Input.GetAxisRaw ("Vertical") == -1) {
				timeStamp = Time.time + cooldown;
				JumpShoot ();
			}
			
			
			//shoot upward
			if (Time.time > timeStamp && Input.GetButton (button) && Input.GetAxisRaw ("Vertical") == 1) {
				timeStamp = Time.time + cooldown;
				ShootUp ();
			}
		}
	}
	
	protected void ShootHorizontal(){
		if (transform.lossyScale.x < 0) {
			ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (0, 0, 5f), Quaternion.Euler (0,0,0)) as GameObject;
			ProjectileInstance.rigidbody2D.AddForce(new Vector2(-1000 * power,0) * Time.deltaTime);
		}else if (transform.lossyScale.x > 0) {
			ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (0, 0, 5f), Quaternion.Euler (0,0,0)) as GameObject;
			ProjectileInstance.rigidbody2D.AddForce(new Vector2(1000 * power,0) * Time.deltaTime);
		}

	}

	protected void JumpShoot(){
		ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (0, -1f, 0f), Quaternion.Euler (0,0,0)) as GameObject;
		ProjectileInstance.rigidbody2D.AddForce(new Vector2(0, -1000 * power) * Time.deltaTime);
		ProjectileInstance.transform.rotation = Quaternion.Euler (new Vector3(0,0,90));
	}
	
	protected void ShootUp(){
		ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (0, 1f, 0f), Quaternion.Euler (0,0,0)) as GameObject;
		ProjectileInstance.rigidbody2D.AddForce(new Vector2(0, 1000 * power) * Time.deltaTime);
		ProjectileInstance.transform.rotation = Quaternion.Euler (new Vector3(0,0,90));
	}

}
