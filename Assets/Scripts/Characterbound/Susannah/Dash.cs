using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour {

	public float speed = 100f;
	private CharControlSusannah CC;
	private bool dashNow;
	private Vector3 currentPosition;
	private float deltaPosition;
	// Use this for initialization
	void Start () {
		CC = GetComponent<CharControlSusannah>();
		dashNow = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1") && transform.localScale.x > 0){
			currentPosition = transform.position;
			StartCoroutine ("HorizontalDash", 1f);
			CC.enabled = false;
			dashNow = true;
		}else if(Input.GetButtonDown("Fire1") && transform.localScale.x < 0){
			currentPosition = transform.position;
			StartCoroutine ("HorizontalDash", -1f);
			CC.enabled = false;
			dashNow = true;
		}

		deltaPosition = Mathf.Abs (transform.position.x - currentPosition.x);

		if(deltaPosition > 3f){
			CC.enabled = true;
			StopCoroutine ("HorizontalDash");
			dashNow = false;
		}

	}

	IEnumerator HorizontalDash(float direction){
		while (true) {
			transform.position += new Vector3 (direction * speed * Time.deltaTime, 0, 0);
			yield return null;
		}
	}

	void OnCollisionEnter2D(Collision2D c){
		if(c.collider.tag == "Wall" && dashNow){
			StopCoroutine("HorizontalDash");
			Debug.Log ("Stopped");
		}
	}
}
