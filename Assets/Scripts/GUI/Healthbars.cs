using UnityEngine;
using System.Collections;

public class Healthbars : MonoBehaviour {

	public int curHealth = 100;
	private int maxHealth = 100;

	private float healthbarlength;
	private float manaBarLenght;



	// Use this for initialization
	void Start () {
		healthbarlength = Screen.width / 2;
		manaBarLenght = Screen.width / 4;
	}
	
	// Update is called once per frame
	void Update () {
		ChangeHealthBar (0);
	}

	void OnGUI(){
		GUI.Box (new Rect (50, 20, healthbarlength, 20), "");

		GUI.Box (new Rect (50, 50, manaBarLenght, 20), "");

	}

	void ChangeHealthBar(int deltahealth){

		curHealth += deltahealth;

		healthbarlength = (Screen.width/2) * (curHealth/(float)maxHealth);

	}
}
