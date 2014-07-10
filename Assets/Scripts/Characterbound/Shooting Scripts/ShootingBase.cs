using UnityEngine;
using System.Collections;

public class ShootingBase : MonoBehaviour {

	private CharControlBase MoveControl;
	protected float speedx;
	public GameObject projectile;
	protected GameObject ProjectileInstance;
	
	private bool aired;
	public float gravityScale = 0; //shoot in straight line at default

	public float cooldown;
	private float timeStamp;
	private float verticalVelo;
	
	
	// Use this for initialization
	protected void Start () {
		MoveControl = GetComponent<CharControlBase> ();
		
	}
	
	// Update is called once per frame
	protected void Update () {

		if(ProjectileInstance){
			ProjectileInstance.rigidbody2D.gravityScale = gravityScale;
		}

		verticalVelo = rigidbody2D.velocity.y;
		//Debug.Log (verticalVelo);
		
		aired = MoveControl.Aired;
		
		speedx = MoveControl.Speedx;
		
		
		//shoot horizontal idle
		if(Time.time > timeStamp && Input.GetButtonDown ("Fire1") && Input.GetAxisRaw ("Vertical") == 0 && speedx == 0){
			timeStamp = Time.time + cooldown;
			
			ShootHorizontal ();
		}
		
		//JumpShoot
		if (Time.time > timeStamp && aired && Input.GetButtonDown ("Fire1") && Input.GetAxisRaw ("Vertical") == -1) {
			timeStamp = Time.time + cooldown;
			JumpShoot ();
		}
		
		
		//shoot upward
		if (Time.time > timeStamp && Input.GetButtonDown ("Fire1") && Input.GetAxisRaw ("Vertical") == 1) {
			timeStamp = Time.time + cooldown;
			ShootUp ();
		}
		
		//shoot horizontal while moving
		if(Time.time > timeStamp && Input.GetButtonDown ("Fire1") && Input.GetAxisRaw ("Vertical") == 0 && Mathf.Abs (speedx) > 0){
			timeStamp = Time.time + cooldown;
			
			ShootHorizontal();
		}
		
		
	}
	
	protected void ShootHorizontal(){
		if (transform.lossyScale.x < 0) {
			ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (0, 0, 5f), Quaternion.Euler (0,0,0)) as GameObject;
			ProjectileInstance.rigidbody2D.AddForce(new Vector2(-25000, 0) * Time.deltaTime);
			ProjectileInstance.transform.localScale = new Vector3(-1, 1, 1);
		}else if (transform.lossyScale.x > 0) {
			ProjectileInstance = Instantiate (projectile, transform.position + new Vector3 (0, 0, 5f), Quaternion.Euler (0,0,0)) as GameObject;
			ProjectileInstance.rigidbody2D.AddForce(new Vector2(25000,0) * Time.deltaTime);
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
