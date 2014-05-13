using UnityEngine;
using System.Collections;

public class ControlSheetRows : MonoBehaviour {

	private AnimatedSpriteSheet ASS;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		ASS = GetComponent<AnimatedSpriteSheet> ();
		if (Input.GetAxisRaw("Horizontal") == 0) {
			ASS.rowNumber = 0;

		}else{
			ASS.rowNumber = 1;
		}
	}
}
