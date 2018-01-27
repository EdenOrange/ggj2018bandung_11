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
	public float money;
	public float highScore;
	public int hacksLeft;
	public enum GameStatus {
		GAMESTATUS_PLAY,
		GAMESTATUS_END
	}
	public int nBrankas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
