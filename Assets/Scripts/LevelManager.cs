using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	/* Game loop & fungsi-fungsi yang dibutuhkan */

	private LevelData levelData;

	// Use this for initialization
	void Start () {
		levelData = GetComponent<LevelData>();
		levelData.gameStatus = (int)LevelData.GameStatus.GAMESTATUS_MENU;
	}
	
	// Update is called once per frame
	void Update () {
		/* Game Loop
		 * Randomize kapan mulai dan boleh SendHack() */
	}

	void StartGame() {
		/* Mulai gamenya */
		InitLevelData();
		GenerateLevel();
		SpawnBrankas();
	}

	void GameOver() {
		/* Game over
		 * Tampilin end game UI */
	}

	void InitLevelData() {
		/* Init level data parameters */
		levelData.level = 1;
		levelData.sendHackSpeed = 10;
		levelData.sendHackSpeedMult = 1;
		levelData.hackSpeed = 10;
		levelData.hackSpeedMult = 1;
		levelData.money = 0;
		levelData.hacksLeft = 2;
		levelData.gameStatus = (int)LevelData.GameStatus.GAMESTATUS_PLAY;
		levelData.nBrankas = 1;
	}

	void GenerateLevel() {
		/* Generate lokasi-lokasi brankas */
	}

	void SpawnBrankas() {
		/* Spawn brankas sebanyak levelData.nBrankas */
		// Jangan lupa set id brankas yang dispawn
	}

	void DropBrankas() {
		/* Persiapan next level
		 * Brankas sekarang jatoh */
	}

	void NextLevel() {
		/* Update parameter untuk next level */
	}

	void SendHack(int brankasId) {
		/* Kirim hack ke brankas ke-brankasId */
	}

	void StartHack(int brankasId) {
		/* Brankas ke-brankasId mulai kebuka */
	}

	void Collect(int brankasId) {
		/* Collect money dari brankas ke-brankasId */
	}

	void CollectAll() {
		/* Collect money dari seluruh brankas */
	}

	void SendMoney(int brankasId) {
		/* Kirim money dari brankas ke-brankasId ke tas */
	}
}
