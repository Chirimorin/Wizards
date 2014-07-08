using UnityEngine;
using System.Collections;

public class BasicButtons : MonoBehaviour {

	private string IP = "192.168.1.9";
	private int port = 25565;
	private string portholder = "2";
	public GameObject Phil;
	public GameObject Mya;
	public GameObject Susannah;
	private GameObject playerins;

	private bool initServerShow = false;
	private bool connectServerShow = false;
	private bool chosen;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void SpawnPlayer(float x, float y, GameObject prefab){
		playerins = Instantiate (prefab, new Vector3 (x, y, 0), Quaternion.Euler (0,0,0)) as GameObject;

	}


	void OnGUI()
	{


		if(!chosen){
			//PHIL
			if(GUI.Button (new Rect(100, 100, 100, 35), "Phil"))
			{
				SpawnPlayer(-3, -2, Phil);
				chosen = true;
			}


			//MYA
			if(GUI.Button (new Rect(100, 200, 100, 35), "Mya"))
			{
				SpawnPlayer (-3,-2, Mya);
				chosen = true;
			}

			//SUSANNAH
			if(GUI.Button (new Rect(100, 300, 100, 35), "Susannah"))
			{
				SpawnPlayer (10,13, Susannah);
				chosen = true;
			}

		}


	}
	/*
	void OnServerInitialized(){
		SpawnPlayer (-3, -2);

		//Camera.current.enabled = false;

	}

	void OnConnectedToServer(){
		SpawnPlayer (-4, -2);
	}
	*/
}
