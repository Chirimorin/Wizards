using UnityEngine;
using System.Collections;

public class Platforming : MonoBehaviour {

	private GameObject Player;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("NecroFT(Clone)");
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Player.transform.position);
	}
}
