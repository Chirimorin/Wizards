using UnityEngine;
using System.Collections;

public class MeleeBase : MonoBehaviour {
	public float width = 0;
	public float height = 0;
	public float offsetX = 0; // Higher is more to the front of the character
	public float offsetY = 0; 


	protected GameObject hitBox;
	protected GameObject parent;

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

		parent = gameObject;
		Debug.Log ("MeleeBase found parent: " + parent.name);

		hitBox = new GameObject (
			"swordHitbox", 
			typeof(BoxCollider2D));

		//hitBox.renderer.material.shader = Shader.Find ("Unlit/Transparent");

		hitBox.transform.position = new Vector2 (parent.transform.position.x, parent.transform.position.y);

		Debug.Log ("MeleeBase constructor end");
	}
	
	// Update is called once per frame
	protected void Update () {
		
	}
}
