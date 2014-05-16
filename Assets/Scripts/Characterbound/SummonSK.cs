using UnityEngine;
using System.Collections;

public class SummonSK : MonoBehaviour {

	public GameObject skeleton;

	// Use this for initialization
	void Start () {



		for (int i = 0; i < 1; i++) {
			Instantiate (skeleton, new Vector3 (0, 0, 0), Quaternion.Euler (0, 0, 0));
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ((skeleton.transform.position.x-transform.position.x));
	}
}
