using UnityEngine;
using System.Collections;

public class BaseAI : MonoBehaviour {

	private float stunDuration;
	protected bool stunned;
	private float stunTimer;

	// Use this for initialization
	protected void Start () {
		stunned = false;
	}

	// Update is called once per frame
	protected void Update () {
		if(stunned){
			stunTimer += Time.deltaTime;
			if(stunTimer > stunDuration){
				stunTimer = 0;
				stunned = false;
			}
		}

	}

	public void Stun(float stunDuration){
		this.stunDuration = stunDuration;
		stunned = true;
	}

}
