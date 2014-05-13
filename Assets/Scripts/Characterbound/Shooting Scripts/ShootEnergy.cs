using UnityEngine;
using System.Collections;

public class ShootEnergy : MonoBehaviour {

	// Instantiating rigidbody
	public GameObject energyBallInstance;
	public Transform sourceTarget;


	// Movement energyball
	private GameObject bulIns;
	private float xDisMid1, xDisMid2;
	private float yDisMid;
	private Vector3 leftdis, rightdis;
	private Vector3 mousePos;
	private Vector3 inv, dir;


	//Velocity energyball
	public int speed = 50;
	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {

		leftdis = Vector3.Normalize (((sourceTarget.transform.position - new Vector3(1, 0, 0)) - mousePos));
		rightdis = (sourceTarget.transform.position - new Vector3 (-1, 0, 0)) - mousePos;


		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);


		if(Input.GetButtonDown ("Fire1")){
			SetSource();
		}


	}

	void SetSource(){
		if (Input.mousePosition.x <= Screen.width / 2) {
			bulIns = Instantiate (energyBallInstance, sourceTarget.transform.position + new Vector3 (-1f, 0, 0f), Quaternion.Euler (0,0,20)) as GameObject;
			bulIns.rigidbody2D.AddForce(/*this.setDir (xDisMid2, yDisMid) * -50000 * Time.deltaTime*/ setDir (leftdis.x, leftdis.y) * (speed * -1000f) * Time.deltaTime);
		} else if (Input.mousePosition.x > Screen.width / 2){
			GameObject bullIns = Instantiate (energyBallInstance, sourceTarget.transform.position + new Vector3 (1f, 0, 0f), Quaternion.Euler (0,0,0)) as GameObject;
			bullIns.rigidbody2D.AddForce(/*this.setDir (xDisMid2, yDisMid) * -50000 * Time.deltaTime*/ setDir (rightdis.x, rightdis.y) * (speed * -1000f) * Time.deltaTime);
		}

	}

	Vector3 setDir(float xDis, float yDis){
		
		Vector3 inv = new Vector3 (xDis, yDis, 0);
		Vector3 dir = inv / inv.magnitude;
		
		return dir;
	}

}
