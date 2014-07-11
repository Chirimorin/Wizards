using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int maxHealth = 0;
	public int health = 0;
	public int mana = 0;

	public bool showHealth = false;
	private bool draw = false;

	// Use this for initialization
	void Start () {
		if (maxHealth == 0)
			Debug.LogError ("Error! Entity '" + gameObject.name + "' spawned with 0 maxHealth!");
		if (health == 0)
			Debug.LogError ("Error! Entity '" + gameObject.name + "' spawned with 0 health!");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Damage(int damage) {
		health -= damage;

		if (health <= 0) {
			Destroy(gameObject);
		}
	}
}
