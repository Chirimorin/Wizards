using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour {

	public float speed = 5f;
	public float range = 3f;
	public float rayPosY = 0.5f;

	private CharControlSusannah CC;
	private bool dashNow;
	private Vector3 currentPosition;
	private float deltaPosition;
	private Vector3 rayPostLeft, rayPosRight;
	private LayerMask dashMask;

	public bool WalCol{
		get{
			Debug.DrawRay(transform.position + new Vector3 (0, rayPosY, 0), -Vector2.right, Color.black);

			return (bool)(Physics2D.Raycast (transform.position + new Vector3 (0, rayPosY, 0), -Vector2.right, 1f, dashMask) || Physics2D.Raycast(transform.position, Vector2.right, 1f, dashMask));
		}
	}

	// Use this for initialization
	void Start () {
		CC = GetComponent<CharControlSusannah>();
		dashNow = false;
	}
	
	// Update is called once per frame
	void Update () {

		dashMask = (LayerMask)0;
		dashMask |= 1 << LayerMask.NameToLayer ("Wall");

		if(WalCol){
			StopCoroutine ("HorizontalDash");
			//collider2D.sharedMaterial = material;
			CC.enabled = true;
		}

		if(Input.GetButtonDown("Fire1") && transform.localScale.x > 0){
			currentPosition = transform.position;
			StartCoroutine ("HorizontalDash", 1f);
			CC.enabled = false;
			dashNow = true;
		}else if(Input.GetButtonDown("Fire1") && transform.localScale.x < 0){
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
			//transform.position += new Vector3 (direction * speed * Time.deltaTime, 0, 0);
			//rigidbody2D.velocity = new Vector2(direction * speed, 0);
			rigidbody2D.AddForce(new Vector2(direction * speed, 0));
			yield return null;
		}
		rigidbody2D.velocity = new Vector2 (0,0);
		yield break;
	}

	void OnCollisionEnter2D(Collision2D c){
		if(c.collider.tag == "Wall" && dashNow){
			StopCoroutine("HorizontalDash");
			//rigidbody2D.AddForce(new Vector2(1, 0));
			Debug.Log ("Stopped");
		}
	}
}
