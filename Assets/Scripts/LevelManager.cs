using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
	/* Game loop & fungsi-fungsi yang dibutuhkan */

	public GameObject[] arraySpawn;
	public GameObject brankasPrefab;

	private List<int> brankasToSpawn = new List<int> ();
	private List<GameObject> brankasSpawned = new List<GameObject> ();
	private LevelData levelData;
	private Fader fader;

	public Text gameScore;
	public Text gameHighscore;
	public Text endScore;
	public Text endHighscore;

	private GameObject[] arrayBrankas;
	private float delay = 3.0f;
	// Use this for initialization
	void Start () {
		levelData = GetComponent<LevelData>();
		levelData.gameStatus = (int)LevelData.GameStatus.GAMESTATUS_MENU;
		//fader = GameObject.Find ("UI").GetComponent<Fader> ();

		StartGame ();
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
		int count = 0;
		while (count < levelData.nBrankas) {
			int idx = Random.Range (0, 7);
			if (!brankasToSpawn.Contains (idx)) {
				brankasToSpawn.Add (idx);
				count++;
			}
		}
	}

	void SpawnBrankas() {
		/* Spawn brankas sebanyak levelData.nBrankas */
		// Jangan lupa set id brankas yang dispawn
		foreach (int i in brankasToSpawn) {
			GameObject newBrankas = Instantiate (brankasPrefab, arraySpawn [i].transform.position, arraySpawn [i].transform.rotation);
			brankasSpawned.Add (newBrankas);
		}	

	}

	void DropBrankas(GameObject brankas) {
		/* Persiapan next level
		 * Brankas sekarang jatoh */
		//yield WaitForSecond(delay);
		//Destroy (brankas);
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
		foreach (GameObject brankas in brankasSpawned) {
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
			foreach (GameObject brankas in brankasSpawned) {
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
