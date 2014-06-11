using UnityEngine;
using System.Collections;

public class Healthbars : MonoBehaviour {

	public float curHealth = 100;
	private float maxHealth = 100;

	private float maxMana = 80;
	private float curMana = 80;

	public float healthbarlength;
	public float negativeHealthLength;
	private float manaBarLength;
	public int width = 1024;

	public float timer;

	private Texture2D HUD;
	private Texture2D FlareEffects;
	private Texture2D NecromancerHud;
	private Texture2D emptyHealth;
	private Texture2D emptyMana;
	private Texture2D emptyXP;
	private Texture2D health;
	private Texture2D negativeHealth;
	private Texture2D mana;
	// Use this for initialization
	void Start () {

		timer = 0;

		healthbarlength = 1285f;
		negativeHealthLength = 1285f;

		manaBarLength = 1268f;

		HUD = Resources.Load("GUI/HUD/HUD") as Texture2D;
		FlareEffects = Resources.Load ("GUI/HUD/HUD flare effects") as Texture2D;
		NecromancerHud = Resources.Load ("GUI/HUD/Necromancer_hud") as Texture2D;

		emptyXP = Resources.Load ("GUI/HUD/empty xp bar") as Texture2D;
		emptyHealth = Resources.Load ("GUI/HUD/empty hp bar") as Texture2D;
		emptyMana = Resources.Load ("GUI/HUD/empty mana bar") as Texture2D;

		health = Resources.Load ("GUI/HUD/hp bar") as Texture2D;
		negativeHealth = Resources.Load ("GUI/HUD/negative hp bar") as Texture2D;

		mana = Resources.Load ("GUI/HUD/mana bar") as Texture2D;
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;

		ChangeBars ();

		if (timer > 1f && negativeHealthLength >= healthbarlength) {
			negativeHealthLength -= 10f;
		}

		if (Input.GetButtonDown ("Fire1")) {
			InflictDamage(20);
			DrainMana (10);
		}

	}

	void OnGUI(){

		//resolution scaling
		AutoResize (1920, 1080);

		//HEALTH
		GUI.DrawTexture (new Rect(171,78, (float)(1285 * 0.375), (float)(49 * 0.375)), emptyHealth, ScaleMode.ScaleAndCrop , true, 0);
		GUI.DrawTexture (new Rect(171,78, (float)(negativeHealthLength * 0.375), (float)(49 * 0.375)), negativeHealth, ScaleMode.ScaleAndCrop, true, 0);
		GUI.DrawTexture (new Rect(171,78, (float)(healthbarlength * 0.375), (float)(49 * 0.375)), health, ScaleMode.ScaleAndCrop, true, 0);

		//MANA
		GUI.DrawTexture (new Rect(168,103, (float)(1268 * 0.375), (float)(56 * 0.375)), emptyMana, ScaleMode.ScaleAndCrop , true, 0);
		GUI.DrawTexture (new Rect(168,103, (float)(manaBarLength * 0.375), (float)(56 * 0.375)), mana, ScaleMode.ScaleAndCrop , true, 0);

		//MAINFRAME
		GUI.DrawTexture (new Rect(10,10,(float)(1824 * 0.375), (float)(578* 0.375)), HUD);

		//LEFT PART
		GUI.DrawTexture (new Rect(50,40,(float)(257*0.375), (float)(299 *0.375)), NecromancerHud);
		GUI.DrawTexture (new Rect(10,10,(float)(1824*0.375), (float)(289 *0.75)), FlareEffects);
	}

	void ChangeBars(){

		healthbarlength = (float)((curHealth / maxHealth) * 1285);
		manaBarLength = (float)((curMana / maxMana) * 1268);

		if(healthbarlength <= 0){
			healthbarlength = 0;
		}

		if(manaBarLength <= 0){
			healthbarlength = 0;
		}
	}

	public void InflictDamage(float dmg){

		timer = 0;
		timer += Time.deltaTime;


		curHealth -= dmg;
	}

	void DrainMana(float drain){

		curMana -= drain;

	}

	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}

}
