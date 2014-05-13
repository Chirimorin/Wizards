using UnityEngine;
using System.Collections;

public class MoveTowardsTest : MonoBehaviour {

	public Transform target;
	public float speed ;
	private float thisx,thisy,targetx,targety, difx, dify, firstangle, secondangle, difx2, dify2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		//rotation
		thisx = transform.position.x;
		thisy = transform.position.y;
		targetx = target.transform.position.x;
		targety = target.transform.position.y;

		difx = Mathf.Abs (thisx - targetx);
		dify = Mathf.Abs (thisy - targety);
		difx2 = Mathf.Abs (targetx - thisx);
		dify2 = Mathf.Abs (targety - thisy);

		firstangle = (int) Mathf.Rad2Deg * Mathf.Atan (difx / dify);
		secondangle = (int)Mathf.Rad2Deg * Mathf.Atan (difx2 / dify2);
		/*
		if (thisx - targetx > 0) {
			transform.rotation = Quaternion.Euler (0, 0, firstangle);
		} else if( targetx - thisx > 0){
			transform.rotation = Quaternion.Euler (0,0, 180 - secondangle);		
		}*/


		Debug.Log (firstangle);

		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, target.position, step); 
	}
}
