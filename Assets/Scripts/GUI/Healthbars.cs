using UnityEngine;
using System.Collections;

public class Healthbars : MonoBehaviour {

	public int curHealth = 100;
	private int maxHealth = 100;

	private float healthbarlength;
	private float manaBarLenght;

	private Texture2D HUD;
	private Texture2D FlareEffects;
	private Texture2D NecromancerHud;
	private Texture2D emptyHealth;
	private Texture2D emptyMana;
	private Texture2D emptyXP;
	// Use this for initialization
	void Start () {

		HUD = Resources.Load("GUI/HUD/HUD") as Texture2D;
		FlareEffects = Resources.Load ("GUI/HUD/HUD flare effects") as Texture2D;
		NecromancerHud = Resources.Load ("GUI/HUD/Necromancer_hud") as Texture2D;

		emptyXP = Resources.Load ("GUI/HUD/empty xp bar") as Texture2D;
		emptyHealth = Resources.Load ("GUI/HUD/empty hp bar") as Texture2D;
		emptyMana = Resources.Load ("GUI/HUD/empty mana bar") as Texture2D;

		healthbarlength = Screen.width / 2;
		manaBarLenght = Screen.width / 4;
	}
	
	// Update is called once per frame
	void Update () {
		ChangeHealthBar (0);
	}

	void OnGUI(){

		//resolution scaling
		AutoResize (1920, 1080);

		//GUI.Box (new Rect (50, 20, healthbarlength, 20), "");
		//GUI.Box (new Rect (50, 50, manaBarLenght, 20), "");
		GUI.DrawTexture (new Rect(142,78,(float)(1024*0.5), (float)(39 *0.375)), emptyHealth);
		GUI.DrawTexture (new Rect(10,10,(float)(1824*0.375), (float)(289 *0.75)), HUD);

		//GUI.DrawTexture (new Rect(150,78.5,(float)(1024*0.5), (float)(39 *0.375)), emptyHealth);

		GUI.DrawTexture (new Rect(50,40,(float)(257*0.375), (float)(299 *0.375)), NecromancerHud);
		GUI.DrawTexture (new Rect(10,10,(float)(1824*0.375), (float)(289 *0.75)), FlareEffects);
	}

	void ChangeHealthBar(int deltahealth){

		curHealth += deltahealth;

		healthbarlength = (Screen.width/2) * (curHealth/(float)maxHealth);

	}

	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}

}
