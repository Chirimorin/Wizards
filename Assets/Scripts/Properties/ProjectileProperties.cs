using UnityEngine;
using System.Collections;

public class ProjectileProperties : MonoBehaviour {

	private static float skullDamage = 10f;

	// Use this for initialization
	void Start () {
	}
	
	public static float GetSkullDamage(){
		return skullDamage; 
	}
}
