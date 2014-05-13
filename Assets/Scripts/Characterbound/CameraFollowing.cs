using UnityEngine;
using System.Collections;

public class CameraFollowing : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = target.position;
		transform.position = new Vector3 (target.position.x, target.position.y, -10.0f);
	}
}
