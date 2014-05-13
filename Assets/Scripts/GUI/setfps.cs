using UnityEngine;
using System.Collections;

public class setfps : MonoBehaviour {

	// Use this for initialization
	void Awake(){
		Application.targetFrameRate = 60;
	}
}
