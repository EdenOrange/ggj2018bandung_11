using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour {
	/* Isi variabel-variabel yang berhubungan dengan game */

	public int level;
	public float sendHackSpeed;
	public float sendHackSpeedMult;
	public float hackSpeed;
	public float hackSpeedMult;
	public int money;
	public int highScore;
	public int hacksLeft;
	public enum GameStatus {
		GAMESTATUS_PLAY,
		GAMESTATUS_END
	}
	//public Brankas brankasList[];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
