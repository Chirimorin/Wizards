using UnityEngine;
using System.Collections;

public abstract class CharControlBase : MonoBehaviour {
	// Basic movement
	protected float speedx;
	protected float speedy;
	public float acceleration;
	public float brakeSpeed;
	public float maxSpeed;

	// Jumping
	public int jumpheight;
	public float baseGravity;
	public float floatyness; // How floaty the character is while the jump button is held. 0 = no difference. 1= no gravity. 
	private float offGroundTimer;
	
	private Vector3 extents;
	private Vector3 bottomLeft;
	private Vector3 bottomRight;

	private LayerMask groundLayers;

	//Platforming
	private float verticalVelocity;

	// Animations
	private Animator anim;

	// Use this for initialization
	protected void Start () {
		extents = this.GetComponent<BoxCollider2D>().size * 0.5f;
		bottomLeft = new Vector3(-extents.x * transform.lossyScale.x, -extents.y * transform.lossyScale.y, 0f);
		bottomRight = new Vector3(extents.x * transform.lossyScale.x, -extents.y * transform.lossyScale.y, 0f);

		groundLayers = (LayerMask)0;
		groundLayers |= (1 << LayerMask.NameToLayer("Default"));
		groundLayers |= (1 << LayerMask.NameToLayer("Platforms"));

		anim = gameObject.GetComponent<Animator> ();
	}

	protected void Update () {
		verticalVelocity = rigidbody2D.velocity.y;

		// anim.SetFloat ("H_Speed", Mathf.Abs (speedx));

		XAxisMovement ();
		Jump ();
	}

	protected void Jump()
	{
		//TODO: check jump logic
		//TODO: add float logic

		if (Input.GetButtonDown ("Jump") && /*!Aired ()*/offGroundTimer < 0.1f && verticalVelocity < 0.1f && Input.GetAxisRaw ("Vertical") != -1) {
			rigidbody2D.AddForce (new Vector3 (0, jumpheight, 0));
		} else if (Input.GetButtonDown ("Jump") && Input.GetAxisRaw ("Vertical") == -1) {
			return;
		}
	}

	protected void XAxisMovement()
	{
		Debug.Log ("TEST!");
		float axis = Input.GetAxisRaw ("Horizontal");

		Vector3 vec = new Vector3 (speedx * Time.deltaTime, speedy, 0);
		
		if (axis < -0.5) {
			speedx -= acceleration;
			Vector3 rotate = transform.localScale;
			rotate.x = -0.5f;
			transform.localScale = rotate;
			if (speedx < maxSpeed * -1) {
				speedx = -1 * maxSpeed;
				speedx += 0;
			}
			
		} else if (axis > 0.5) {
			speedx += acceleration;
			Vector3 rotate2 = transform.localScale;
			rotate2.x = 0.5f;
			transform.localScale = rotate2;
			if(speedx > maxSpeed){
				speedx = maxSpeed;
				speedx += 0;
			}
			
		} else {
			if (speedx <= maxSpeed && speedx > 0) {
				speedx -= brakeSpeed;
				if (speedx <= 0) {
					speedx = 0;
				}
			}
			if (speedx >= -maxSpeed && speedx < 0) {
				speedx += brakeSpeed;
				if (speedx >= 0) {
					speedx = 0;
				}
			}
		}

		transform.position += vec;
	}

	protected bool IsAired()
	{
		Vector2 pos1 = transform.position + bottomLeft;
		Vector2 pos2 = transform.position + bottomRight;
		
		Debug.DrawRay (pos1, -Vector2.up * 0.1f, Color.cyan);
		Debug.DrawRay (pos2, -Vector2.up * 0.1f, Color.cyan);
		return !(Physics2D.Raycast(pos1, -Vector2.up, 0.1f, groundLayers) || Physics2D.Raycast(pos1, -Vector2.up, 0.1f, groundLayers));
	}
}
