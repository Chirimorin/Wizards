using UnityEngine;
using System.Collections;

public class WispAI : MonoBehaviour {

	private GameObject player;
	private float absDeltaDistanceX;

	private float idleTimer;

	public float Health;

	// Use this for initialization
	void Start () {
		Health = 100f;
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.Find ("NecroFT(Clone)");

		try{
			absDeltaDistanceX = Mathf.Abs(transform.position.x - player.transform.position.x);
		}catch(UnityException e){
			Debug.Log (e);
		}

		if (absDeltaDistanceX > 7f) {
			MoveIdleState ();
		} else if (absDeltaDistanceX < 7f && absDeltaDistanceX > 3f) {
			MoveChasingState ();
		} else {
			MoveAttackState (); 
		}
	}

	void MoveIdleState(){
		idleTimer += Time.deltaTime;
		if(idleTimer > Random.Range (3f, 5f)){
			Debug.Log (idleTimer);
			transform.position = Vector2.Lerp (transform.position, transform.position + new Vector3(-0.5f, 0, 0), 2f);
		}
	}

	void MoveChasingState(){

	}

	void MoveAttackState(){

	}

	void AddDamage(float damage){
		Health -= damage;
	}

}
