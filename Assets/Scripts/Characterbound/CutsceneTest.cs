using UnityEngine;
using System.Collections;

public class CutsceneTest : MonoBehaviour {


	public GameObject necro;
	public GameObject blackbars;
	private GameObject blackbar1, blackbar2;

	// Use this for initialization
	void Start () {
		blackbar1 = Instantiate (blackbars, transform.position + new Vector3 (0, -6f, 1f), Quaternion.Euler (90, 180, 0)) as GameObject;
		necro.GetComponent<PlayerController> ().enabled = false;
		necro.GetComponent<ShootEnergy> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown ("Fire1")){

			Instantiate (blackbars, transform.position + new Vector3 (0, +4.5f, 1f), Quaternion.Euler (90, 180, 0));

		}
		if (blackbar1.transform.position != transform.position + new Vector3 (0, -4.5f, 1f)) {
			blackbar1.transform.position = Vector3.Lerp (transform.position + new Vector3 (0, -6f, 1f), blackbar1.transform.position + new Vector3 (0, 1.5f, 1f), 2000f);
		}
	}

}
