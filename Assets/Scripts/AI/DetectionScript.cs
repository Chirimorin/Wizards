using UnityEngine;
using System.Collections;

public class DetectionScript : MonoBehaviour {
	// Collider
	private BoxCollider2D coll;

	// Distance measurements
	private Vector3 extents;
	private Vector3 bottomLeft;
	private Vector3 bottomRight;

	// Layer mask
	private LayerMask groundLayers;

	// Use this for initialization
	void Start () {
		coll = GetComponent<BoxCollider2D>();
		extents = this.GetComponent<BoxCollider2D>().size * 0.5f;
		bottomLeft = new Vector3(-extents.x * transform.lossyScale.x, -extents.y * transform.lossyScale.y, 0f);
		bottomRight = new Vector3(extents.x * transform.lossyScale.x, -extents.y * transform.lossyScale.y, 0f);

		groundLayers = (LayerMask)0;
		groundLayers |= (1 << LayerMask.NameToLayer("Default"));
		groundLayers |= (1 << LayerMask.NameToLayer("Platforms"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Determines whether this instance is grounded.
	/// </summary>
	/// <returns><c>true</c> if this instance is grounded; otherwise, <c>false</c>.</returns>
	public bool IsGrounded() {
		Vector2 pos1 = transform.position + bottomLeft;
		Vector2 pos2 = transform.position + bottomRight;
		
		Debug.DrawRay (pos1, -Vector2.up * 0.1f, Color.cyan);
		Debug.DrawRay (pos2, -Vector2.up * 0.1f, Color.cyan);
		return Physics2D.Raycast(pos1, -Vector2.up, 0.1f, groundLayers) && (rigidbody2D.velocity.y < 0.1) && (rigidbody2D.velocity.y > -0.1);
	}
	
	/// <summary>
	/// Checks for ground in front of the target.
	/// </summary>
	/// <returns><c>true</c> if ground was detected, <c>false</c> otherwise.</returns>
	/// <param name="distance">The distance for the check</param>
	public bool SolidInFront(float distance) {
		Vector2 pos = new Vector2 (
			(transform.position.x + ((extents.x + distance) * transform.lossyScale.x)),
			(transform.position.y - (extents.y * transform.localScale.y)));
		Debug.DrawRay (pos, -Vector2.up * 0.1f, Color.white);
		
		return Physics2D.Raycast(pos, -Vector2.up, 0.1f, groundLayers);
	}
	
	public bool IsSolid(Vector2 pos, float height){
		Debug.DrawRay (pos, -Vector2.up * height, Color.green);
		return Physics2D.Raycast(pos, -Vector2.up, 1f, groundLayers);
	}
}
