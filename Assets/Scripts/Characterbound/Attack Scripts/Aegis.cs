using UnityEngine;
using System.Collections;

public class Aegis : MonoBehaviour {

	private GameObject parent;
	public float duration = 5;
	public int damage = 10;
	public float deltaTick = 1f;
	private float timer;

	private Vector3 knockbackDirection;

	// Use this for initialization
	void Start () {
		collider2D.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		parent = GameObject.FindGameObjectWithTag ("Player");
		try{
			transform.position = parent.transform.position + new Vector3(0,0, 2);
		}catch(UnityException e){
			Debug.Log (e);
		}


		DestroyObject (this.gameObject, duration);
		//Debug.Log (timer);
		timer += Time.deltaTime;

		if(timer > deltaTick + 0.5f){
			timer = 0;
			collider2D.enabled = false;
		}

		if(timer > deltaTick){
			collider2D.enabled = true;
		}

	}

	void OnTriggerEnter2D(Collider2D c){

		if(c.tag == "Enemy"){
			(c.gameObject.GetComponent<Health>() as Health).Damage (damage);
			Debug.Log ("Hit");
			(c.gameObject.GetComponent<Health>() as Health).Knockback (100, ((transform.position - c.gameObject.transform.position)*-1).normalized);
		}


	}



}
