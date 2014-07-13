using UnityEngine;
using System.Collections;

public class SpawnAegis : MonoBehaviour {

	public string Button;
	public GameObject aegis;
	public float cooldown = 10;
	private float timeStamp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButton(Button) && Time.time > timeStamp){
			Instantiate (aegis, transform.position, Quaternion.Euler (new Vector3(0,0,0)));
			timeStamp = Time.time + cooldown;
		}
	}
}
