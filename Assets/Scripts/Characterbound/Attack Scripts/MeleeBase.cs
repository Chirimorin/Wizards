using UnityEngine;
using System.Collections;

public class MeleeBase : MonoBehaviour {
	public float width = 0;
	public float height = 0;
	public float offsetX = 0; // Higher is more to the front of the character
	public float offsetY = 0; 

	public float damage = 0;
	public float knockback = -1;
	public float attackTime = 0;
	public float attackCooldown = 0;

	public string attackButton = "";

	protected BoxCollider2D hitBox;
	protected GameObject parent;

	private float attackTimer = 0;
	private float cooldownTimer = 0;

	// Use this for initialization
	protected void Start () {
		Debug.Log ("MeleeBase constructor start");

		if (width == 0) 
			Debug.LogWarning ("Warning: width not set!");
		if (height == 0) 
			Debug.LogWarning ("Warning: height not set!");
		if (offsetX == 0)
			Debug.LogWarning ("Warning: offsetX not set or 0!");
		if (offsetY == 0)
			Debug.LogWarning ("Warning: offsetY not set or 0!");
		if (damage == 0)
			Debug.LogWarning ("Warning: damage not set!");
		if (knockback == -1) {
			Debug.LogWarning ("Warning: knockback not set!");
			knockback = 0;
		}
		if (attackTime == 0)
			Debug.LogWarning ("Warning: attackTime not set!");
		if (attackCooldown == 0)
			Debug.LogWarning ("Warning: attackCooldown not set!");
		if (attackButton == "")
			Debug.LogWarning ("Warning: Attack button not set!");


		parent = gameObject;
		Debug.Log ("MeleeBase found parent: " + parent.name);

		hitBox = (BoxCollider2D)parent.AddComponent ("BoxCollider2D");
		hitBox.size = new Vector2 (width, height);
		hitBox.center = new Vector2 (offsetX, offsetY);
		hitBox.isTrigger = true;
		hitBox.enabled = false;

		Debug.Log ("MeleeBase constructor end");
	}
	
	// Update is called once per frame
	protected void Update () {
		// Attack has started
		if (hitBox.enabled) {
			attackTimer -= Time.deltaTime;
			if (attackTimer <= 0){
				hitBox.enabled = false;
				cooldownTimer = attackCooldown;
			}
		} else { // Attack is not running
			if (cooldownTimer <= 0) // Cooldown over, attack ready for use
			{
				if (Input.GetButtonDown (attackButton)) {
					Debug.Log("Swoosh!");
					hitBox.enabled = true;
					attackTimer = attackTime;
				}
			}
			else { // Cooldown not over, increase the timer. 
				cooldownTimer -= Time.deltaTime;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D c) {
		if (c.gameObject.tag == "Enemy") {
			// TODO: deal damage and knockback to enemy.
			Debug.Log("Enemy hit!");
		}
	}
}
