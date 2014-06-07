using UnityEngine;
using System.Collections;

public class BasicButtons : MonoBehaviour {

	private string IP = "192.168.1.9";
	private int port = 25565;
	private string portholder = "2";
	public GameObject playerPrefab;
	private GameObject playerins;

	private bool initServerShow = false;
	private bool connectServerShow = false;
	// Use this for initialization
	void Start () {
		SpawnPlayer (-3,-2);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void SpawnPlayer(float x, float y){
		playerins = Instantiate (playerPrefab, new Vector3 (x, y, 0), Quaternion.Euler (0,0,0)) as GameObject;

	}


	void OnGUI()
	{
		/*
		if(Network.peerType == NetworkPeerType.Disconnected)
		{


			//INIT
			if(GUI.Button (new Rect(100, 100, 100, 35), "Init Server"))
			{
				initServerShow = true;
				connectServerShow = false;
			}

			if(initServerShow){


				if(GUI.Button (new Rect(300, 100, 100,35), "Host!")){
					Network.InitializeServer(2, port);
				}
			}


			//CONNECT
			if(GUI.Button (new Rect(100, 200, 100, 35), "Connect Server"))
			{
				connectServerShow = true;
				initServerShow = false;
			}

			if(connectServerShow){

				IP = GUI.TextField (new Rect(200,200,100,35), IP);
				if(GUI.Button ( new Rect (300, 200, 100, 35), "Connect!")){
					Network.Connect (IP, port);
				}
			}

		}else{
			if(Network.peerType == NetworkPeerType.Client)
			{
				GUI.Label(new Rect(100,200, 100, 35), "Client");

				if(GUI.Button (new Rect(100, 300, 100, 35), "Logout"))
				{
					Network.Disconnect ();
				}
			}

			if(Network.peerType == NetworkPeerType.Server)
			{
				GUI.Label(new Rect(100,100, 100, 35), "Host, connections: " + Network.connections.Length);

				if(GUI.Button (new Rect(100, 300, 100, 35), "Destroy"))
				{
					Network.Disconnect ();

				}


			}
		}*/
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
