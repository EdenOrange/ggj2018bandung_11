using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brankas : MonoBehaviour {

	public int id;
	public float money;
	public float progress; // 0..openLimit..maxLimit
	public float openLimit;
	public float maxLimit;
	public int status;
	public enum Status {
		BRANKAS_CLOSED,
		BRANKAS_OPENING,
		BRANKAS_OPENED
	}
	public float offset = 0;

	private LevelData levelData;
	private LevelManager levelManager;
	private Animator anim;
	private Animator brankas;
	public GameObject moneyGraphic;

	// Use this for initialization
	void Start () {
		SoundManager.instance.playSFX (SoundManager.BRANKASSPAWN);
		levelData = GameObject.FindWithTag("GameController").GetComponent<LevelData>();
		levelManager = GameObject.FindWithTag("GameController").GetComponent<LevelManager>();
		// id = diatur spawner
		progress = 0;
		openLimit = 100;
		maxLimit = 150;
		status = (int)Status.BRANKAS_CLOSED;
		money = 100;
		anim = levelManager.aliranData [id].GetComponent<Animator> ();

		brankas = GetComponent<Animator> ();
		brankas.speed = 0;
		anim.speed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (levelData.gameStatus == (int)LevelData.GameStatus.GAMESTATUS_PLAY) {
			
			if (progress <= maxLimit && status != (int)Status.BRANKAS_CLOSED) {
				if (status == (int)Status.BRANKAS_OPENING && progress > openLimit) {
					status = (int)Status.BRANKAS_OPENED;
				}
				//Debug.Log(progress/maxLimit);
				anim.Play("aliranhacked" + (id + 1) , -1, progress/maxLimit);
				//Debug.Log ("PLAYING ARMATURE");
				brankas.Play("Armature|open 0" , -1, offset + (1- offset) * progress/maxLimit);
				progress += Time.deltaTime * levelData.hackSpeed * levelData.hackSpeedMult;
			} else if (progress > maxLimit) {
				Debug.Log ("TOOLATE " + progress);
				status = (int)Status.BRANKAS_CLOSED;
				levelManager.GameOver ();
			}

		}


	}
	void OnCollisionEnter(Collision col){
		Debug.Log ("Collide");
		Debug.Log (col.gameObject.name);
		if(col.gameObject.CompareTag("ground")){
			
			SoundManager.instance.playSFX (SoundManager.BRANKASDROP);
		}
	}

}
