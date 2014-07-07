using UnityEngine;
using System.Collections;

public class DestroySingleEnergy : MonoBehaviour {

	//public GameObject particle;
	private Animator ProjAnim;
	private float timer;
	private GameObject wisp;
	private WispCoAI wispAI;
	// Use this for initialization
	void Start () {
		ProjAnim = GetComponent<Animator>() as Animator;
		timer = 0f;
	}
	
	// Update is called once per frame
	void Update () {

		try{
		//	wisp = GameObject.Find ("Wisp_");
		//	wispAI = wisp.GetComponent<WispCoAI>();
		}catch(UnityException e){

		}
		timer += Time.deltaTime;
		ProjAnim.SetFloat ("Timer", timer);
		Destroy (this.gameObject, 1f);
		//Destroy (particle, 3f);
	}
	void OnCollisionEnter2D(Collision2D col){
		if (col.collider.name == "Cube") {
			DestroyObject (this.gameObject);
			//Instantiate(particle, this.gameObject.transform.position, Quaternion.Euler (0,0,0));  
		} else if (col.collider.name == "Floor") {
			DestroyObject (this.gameObject);
		} else if (col.collider.name == "MoveTowards") {
			DestroyObject (this.gameObject);
		//	Instantiate(particle, this.gameObject.transform.position, Quaternion.Euler (0,0,0));
		} else if (col.collider.name == "Wisp_"){
				Debug.Log ("Hit");
		//		wispAI.inflictDamage (ProjectileProperties.GetSkullDamage());

		}
			


	}


}
