using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour {

	public float speed = 5f;
	public float range = 3f;
	public int damage = 10;
	public float rayPosY = 0.5f;
	public bool affectGravity;
	private float firstGravity;

	private CharControlSusannah CC;
	private bool dashNow;
	private Vector3 currentPosition;
	private float deltaPosition;
	private Vector3 rayPostLeft, rayPosRight;
	private LayerMask dashMask;

	private float timeStamp;
	public float cooldown;

	public bool WalCol{
		get{
			Debug.DrawRay(transform.position + new Vector3 (0, rayPosY, 0), -Vector2.right, Color.black);
			Debug.DrawRay(transform.position - new Vector3 (0, rayPosY, 0), -Vector2.right, Color.black);
			Debug.DrawRay(transform.position + new Vector3 (0, rayPosY, 0), Vector2.right, Color.black);
			Debug.DrawRay(transform.position - new Vector3 (0, rayPosY, 0), Vector2.right, Color.black);

			return (bool)(Physics2D.Raycast (transform.position + new Vector3 (0, rayPosY, 0), -Vector2.right,0.75f, dashMask) || 
			              Physics2D.Raycast (transform.position - new Vector3 (0, rayPosY, 0), -Vector2.right, 0.75f, dashMask) || 
			              Physics2D.Raycast(transform.position + new Vector3 (0, rayPosY, 0), Vector2.right, 0.75f, dashMask) ||
						  Physics2D.Raycast(transform.position - new Vector3 (0, rayPosY, 0), Vector2.right, 0.75f, dashMask));
		}
	}

	// Use this for initialization
	void Start () {
		CC = GetComponent<CharControlSusannah>();
		dashNow = false;
		firstGravity = rigidbody2D.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
		
		//TODO collision handling

		dashMask = (LayerMask)0;
		dashMask |= 1 << LayerMask.NameToLayer ("Wall");
		dashMask |= 1 << LayerMask.NameToLayer ("Floor");

		if(WalCol){
			StopCoroutine ("HorizontalDash");
			//collider2D.sharedMaterial = material;
			collider2D.isTrigger = false;
			CC.enabled = true;
			//collider2D.isTrigger = false;
		}

		if(Input.GetButtonDown("Fire2") && transform.localScale.x > 0 && Time.time > timeStamp){
			timeStamp = Time.time + cooldown;
			currentPosition = transform.position;
			StartCoroutine ("HorizontalDash", 1f);
			CC.enabled = false;
			dashNow = true;
		}else if(Input.GetButtonDown("Fire2") && transform.localScale.x < 0 && Time.time > timeStamp){
			timeStamp = Time.time + cooldown;
			currentPosition = transform.position;
			StartCoroutine ("HorizontalDash", -1f);
			CC.enabled = false;
			dashNow = true;
		}

		deltaPosition = Mathf.Abs (transform.position.x - currentPosition.x);

		if(deltaPosition > 3f){
			CC.enabled = true;
			//StopCoroutine ("HorizontalDash");
			//rigidbody2D.velocity.x = 0;
			dashNow = false;
		}

	}

	IEnumerator HorizontalDash(float direction){
		while (Mathf.Abs(transform.position.x - currentPosition.x) < range) {
			if(!affectGravity){
				rigidbody2D.gravityScale = 0;
			}
			//(GetComponent<BoxCollider2D>() as BoxCollider2D).size = new Vector2(2,1);
			Physics2D.IgnoreLayerCollision (8, 11, false);
			rigidbody2D.AddForce(new Vector2(direction * speed, 0));
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
			collider2D.isTrigger = true;
			yield return null;
		}
		(GetComponent<BoxCollider2D>() as BoxCollider2D).size = new Vector2(1,1);
		Physics2D.IgnoreLayerCollision (8, 11);
		collider2D.isTrigger = false;
		rigidbody2D.velocity = new Vector2 (0,0);
		rigidbody2D.gravityScale = firstGravity;
		yield break;
	}

	void OnCollisionEnter2D(Collision2D c){
		if(c.collider.tag == "Wall" && dashNow){
			StopCoroutine("HorizontalDash");
			//rigidbody2D.AddForce(new Vector2(1, 0));
			Debug.Log ("Stopped");
		}
	}

	void OnTriggerEnter2D(Collider2D c){
		if(c.tag == "Wall"){
			rigidbody2D.velocity = new Vector2(0,0);
			collider2D.isTrigger = false;
			CC.enabled = true;
			StopCoroutine("HorizontalDash");
			Debug.Log ("Detected");
		}

		if(c.tag == "Enemy"){
			(c.gameObject.GetComponent<Health>() as Health).Damage (damage);
			Debug.Log ("Dash collides with enemy");
		}

	}

}
