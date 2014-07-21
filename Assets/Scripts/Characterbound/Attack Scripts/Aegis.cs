using UnityEngine;
using System.Collections;

public class Aegis : MonoBehaviour {

	private GameObject parent;
	public float duration = 1;
	public int damage = 10;
	public float deltaTick = 1f;
	private float timer;

	private CircleCollider2D col;
	private Vector3 knockbackDirection;

	// Use this for initialization
	void Start () {
		col = this.GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		parent = GameObject.FindGameObjectWithTag ("Player");
		try{
			transform.position = parent.transform.position + new Vector3(0,0, 2);
		}catch(UnityException e){
			Debug.Log (e);
		}

		transform.localScale += new Vector3 (50f * Time.deltaTime, 50f * Time.deltaTime, 0);

		DestroyObject (this.gameObject, duration);
		//Debug.Log (timer);
		timer += Time.deltaTime;



	}

	void OnTriggerEnter2D(Collider2D c){

		if(c.tag == "Enemy"){
			(c.gameObject.GetComponent<Health>() as Health).Damage (damage);
			Debug.Log ("Hit");
			(c.gameObject.GetComponent<Health>() as Health).Knockback (800, ((transform.position - c.gameObject.transform.position)*-1).normalized);
		}


	}



}
