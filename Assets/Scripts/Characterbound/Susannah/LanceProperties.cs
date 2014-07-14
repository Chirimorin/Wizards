using UnityEngine;
using System.Collections;

public class LanceProperties : MonoBehaviour {

	public int damage = 20;
	private Vector3 startPosition;
	private float deltaPosition;
	public float range = 15f;

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
			(c.gameObject.GetComponent<Health>() as Health).Knockback (100, ((transform.position - c.gameObject.transform.position)*-1).normalized);
			Debug.Log ("The magical lance penetrates the enemy, slowly but painfully, causing " + damage + " damage");
		}
	}
}
