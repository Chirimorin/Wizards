using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

	private GameObject player;
	private GameObject skelly;
	public GameObject playerPrefab;
	private GameObject playerins;

	private float x = -3;
	private float y = -2;

	// Update is called once per frame
	void Update () {
		try{
			player = GameObject.Find ("NecroFT(Clone)");
			skelly = GameObject.Find ("SkellyFT(Clone)");
		}catch(UnityException e){

		}
	}

	void SpawnPlayer(float x, float y){
		playerins = Instantiate (playerPrefab, new Vector3 (x, y, 0), Quaternion.Euler (0,0,0)) as GameObject;
		
	}

	void OnTriggerEnter2D(Collider2D c){
		if(c.name == "NecroFT(Clone)"){
			DestroyObject (player);
			DestroyObject (skelly);
			SpawnPlayer (x,y);
		}
	}
}
