using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour {
	/* Isi variabel-variabel yang berhubungan dengan game */

	public int level;
	public float sendHackSpeed; // Speed dari hacker ke brankas tujuan
	public float sendHackSpeedMult;
	public float hackSpeed; // Speed dari brankas ke money bag
	public float hackSpeedMult;
	public float hackDefaultDelay; // Delay antar hack
	public float money; // Score
	public float highScore;
	public int hacksLeft; // Banyak hacks tersisa sebelum next level
	public int gameStatus;
	public enum GameStatus {
		GAMESTATUS_MENU,
		GAMESTATUS_PLAY,
		GAMESTATUS_END
	}
	public int nBrankas; // Banyaknya brankas di level saat ini

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
