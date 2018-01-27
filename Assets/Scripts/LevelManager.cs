using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	/* Game loop & fungsi-fungsi yang dibutuhkan */

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		/* Game Loop
		 * Randomize kapan mulai dan boleh SendHack() */
	}

	void StartGame() {
		/* Mulai gamenya
		 * Inisialisasi parameter-parameter level */
	}

	void GameOver() {
		/* Game over
		 * Tampilin end game UI */
	}

	void GenerateLevel() {
		/* Generate lokasi-lokasi brankas */
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
