using UnityEngine;
using System.Collections;

public abstract class CharControlBase : MonoBehaviour {
	// Basic movement
	protected float speedx;
	protected float speedy;
	public float acceleration = 0f;
	public float brakeSpeed = 0f;
	public float maxSpeed = 0f;

	// Jumping
	public int jumpheight = 0;
	public float baseGravity = 0f;
	public float floatyness = 0f; // How floaty the character is while the jump button is held. 0 = no difference. 1= no gravity. 
	public int numJumps = 0;
	private int jumpsDone = 0;
	private float jumpTimer = 0f;
	private bool jumpTimerOver = false;
	private float offGroundTimer;
	
	private Vector3 extents;
	private Vector3 bottomLeft;
	private Vector3 bottomRight;

	private LayerMask groundLayers;

	// Platforming
	private float verticalVelocity;

	// Animations
	private Animator anim;

	// Stuff
	public float Speedx { get { return speedx; } }
	public float Speedy { get { return speedy; } }

	public bool Aired { 
		get {
			Vector2 pos1 = transform.position + bottomLeft;
			Vector2 pos2 = transform.position + bottomRight;
			
			Debug.DrawRay (pos1, -Vector2.up * 0.1f, Color.cyan);
			Debug.DrawRay (pos2, -Vector2.up * 0.1f, Color.cyan);

			return !((bool)Physics2D.Raycast(pos1, -Vector2.up, 0.1f, groundLayers) || (bool)Physics2D.Raycast(pos2, -Vector2.up, 0.1f, groundLayers));
		}
	}

	// Use this for initialization
	protected void Start () {
		Debug.Log ("CharControlBase constructor start");
		Debug.Log ("CharControlBase found parent: " + gameObject.name);

		if (acceleration == 0f)
			Debug.LogWarning ("Warning: acceleration not set!");
		if (brakeSpeed == 0f)
			Debug.LogWarning ("Warning: brakeSpeed not set!");
		if (maxSpeed == 0f)
			Debug.LogWarning ("Warning: maxSpeed not set!");
		if (jumpheight == 0)
			Debug.LogWarning ("Warning: jumpheight not set!");
		if (baseGravity == 0f)
			Debug.LogWarning ("Warning: baseGravity not set!");
		if (floatyness == 0f)
			Debug.LogWarning ("Warning: floatyness not set!");
		if (numJumps == 0)
			Debug.LogWarning ("Warning: numJumps not set!");

		rigidbody2D.gravityScale = baseGravity;

		extents = this.GetComponent<BoxCollider2D>().size * 0.5f;
		bottomLeft = new Vector3(-extents.x * transform.lossyScale.x, -extents.y * transform.lossyScale.y, 0f);
		bottomRight = new Vector3(extents.x * transform.lossyScale.x, -extents.y * transform.lossyScale.y, 0f);

		groundLayers = (LayerMask)0;
		//groundLayers |= (1 << LayerMask.NameToLayer("Default"));
		groundLayers |= (1 << LayerMask.NameToLayer("Platforms"));
		groundLayers |= (1 << LayerMask.NameToLayer("Floor"));

		anim = gameObject.GetComponent<Animator> ();

		Debug.Log ("CharControlBase constructor end");
	}

	protected void Update () {
		verticalVelocity = rigidbody2D.velocity.y;
		// anim.SetFloat ("H_Speed", Mathf.Abs (speedx));
		Jump ();
	}

	protected void FixedUpdate(){
		XAxisMovement ();
	}

	protected void Jump()
	{
		if (Aired) {
			if (!jumpTimerOver) {
				jumpTimer += Time.deltaTime;
				if (jumpTimer > 0.1)
				{
					jumpsDone = 1;
					jumpTimerOver = true;
				}
			}
		} else { // On ground
			// Technically this can run after a jump if FPS is high enough.
			// It would give you 0.1s for an extra jump, jump resets vertical speed so I think it won't be noticed. 
			jumpsDone = 0;
			jumpTimer = 0f;
			jumpTimerOver = false;
		}
		
		if (Input.GetButtonDown ("Jump") && (jumpsDone < numJumps)) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
			rigidbody2D.AddForce (new Vector3 (0, jumpheight, 0));

			jumpsDone++;
			jumpTimerOver = true;
		}

		// Float (gravity is lager als jump word vastgehouden)
		if (Input.GetButton ("Jump")) {
			rigidbody2D.gravityScale = baseGravity - (baseGravity * floatyness);
		} else {
			rigidbody2D.gravityScale = baseGravity;
		}
	}

	protected void XAxisMovement()
	{
		float axis = Input.GetAxisRaw ("Horizontal");

		Vector3 vec = new Vector3 (speedx * Time.deltaTime, speedy, 0);
		
		if (axis < -0.5) {
			speedx -= acceleration;
			Vector3 rotate = transform.localScale;
			rotate.x = -1f;
			transform.localScale = rotate;
			if (speedx < maxSpeed * -1) {
				speedx = -1 * maxSpeed;
				speedx += 0;
			}
			
		} else if (axis > 0.5) {
			speedx += acceleration;
			Vector3 rotate2 = transform.localScale;
			rotate2.x = 1f;
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

}
