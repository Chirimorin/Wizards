using UnityEngine;
using System.Collections;

public class MoveObstacle : MonoBehaviour {

	public float speedx = 1.0f;
	public float speedy = 0f;
	public float startPositionX;
	public float deltaPositionX;
	public float startPositionY;
	public float deltaPositionY;
	public float bounds = 0.5f;

	// Use this for initialization
	void Start () {
		startPositionX = transform.position.x;
		
		startPositionY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {

		deltaPositionX = startPositionX - transform.position.x;
		deltaPositionY = startPositionY - transform.position.y;

		transform.position += new Vector3(speedx * Time.deltaTime, speedy * Time.deltaTime, 0);

		if(deltaPositionX > bounds && speedx < 0){
			speedx *= -1;
		}else if(deltaPositionX < (bounds * -1) && speedx > 0){
			speedx *= -1;
		}

		if(deltaPositionY > bounds && speedy < 0){
			speedy *= -1;
		}else if(deltaPositionY < (bounds * -1) && speedy > 0){
			speedy *= -1;
		}
	}
}
