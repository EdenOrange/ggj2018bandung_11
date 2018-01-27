using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	/* Game loop & fungsi-fungsi yang dibutuhkan */

	private LevelData levelData;
	private GameObject[] arrayBrankas;

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
		levelData.level++;
		levelData.sendHackSpeedMult += 0.2f;
		levelData.hackSpeedMult += 0.2f;
		levelData.hacksLeft++;
		levelData.nBrankas++;
	}

	void SendHack(GameObject brankas) {
		/* Kirim hack ke brankas ke-brankasId */

	}

	void StartHack(GameObject brankas) {
		/* Brankas ke-brankasId mulai kebuka */
		Brankas brankasScript = brankas.GetComponent<Brankas>();
		brankasScript.status = (int)Brankas.Status.BRANKAS_OPENING;

	}

	void Collect(GameObject brankas) {
		/* Collect money dari brankas */
		// Brankas pasti sudah bisa dicollect
		Brankas brankasScript = brankas.GetComponent<Brankas>();
		brankasScript.status = (int)Brankas.Status.BRANKAS_CLOSED;
		SendMoney(brankasScript.money * Mathf.Max(brankasScript.progress, 100) / 100);
	}

	void CollectAll() {
		/* Collect money dari seluruh brankas */
		// Cek apakah ada brankas yang kebuka
		bool anyBrankasOpen = false;
		foreach (GameObject brankas in arrayBrankas) {
			Brankas brankasScript = brankas.GetComponent<Brankas>();
			if (brankasScript.status == (int)Brankas.Status.BRANKAS_OPENING || brankasScript.status == (int)Brankas.Status.BRANKAS_OPENED) {
				anyBrankasOpen = true;
				break;
			}
		}
		// Kalau tidak ada brankas terbuka, game over
		if (!anyBrankasOpen) {
			GameOver();
		}
		else {
			foreach (GameObject brankas in arrayBrankas) {
				Brankas brankasScript = brankas.GetComponent<Brankas>();
				// Kalau brankas opening/opened (bisa di collect), collect
				if (brankasScript.status != (int)Brankas.Status.BRANKAS_CLOSED) {
					Collect(brankas);
				}
			}
		}
	}

	void SendMoney(float money) {
		/* Kirim money dari brankas ke-brankasId ke tas */
	}
}
