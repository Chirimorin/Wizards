using UnityEngine;
using System.Collections;

public class AdeptAAProperties : MonoBehaviour {

	public int damage = 20;
	private Vector3 startPosition;
	private float deltaPosition;
	public float range = 5f;
	
	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		deltaPosition = Mathf.Abs(transform.position.x - startPosition.x);
		
		if(deltaPosition > range){
			DestroyObject (this.gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D c){
		if(c.tag == "Enemy"){
			(c.gameObject.GetComponent<Health>() as Health).Damage (damage);
			Debug.Log ("The weak projectile shoots towards the enemy, causing " + damage + " damage");
			DestroyObject(this.gameObject);
		}
	}
}
