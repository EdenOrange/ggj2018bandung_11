using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	/* Game loop & fungsi-fungsi yang dibutuhkan */

	public GameObject[] arraySpawn;
	public GameObject brankasPrefab;
	public GameObject[] aliranData;
	public float fadeSpeed = 5;
	public List<GameObject> brankasSpawned = new List<GameObject> ();

	public CanvasGroup start;
	public CanvasGroup game;
	public CanvasGroup end;

	private List<int> brankasToSpawn = new List<int> ();
	private bool[] sendingHack;
	private bool[] collecting;
	public LevelData levelData;
	private bool inAnimation = false;
	private float[] progressSendHack;
	public static bool ready = false;
	//public static 
	public Text startMsg;
	public Text endMsg;
	public Text gameScore;
	public Text gameHighscore;
	public Text endScore;
	public Text endHighscore;
	public Text startHighscore;
	public static LevelManager instance;
	private bool gameStarted = false;
	private int maxBrankas;
	private float hackDelay;
	private int hacksLeft;
	private int goingToCollect;
	private Animator sendHackAnim;
	private int highscore;

	private int platform; // 0 = Windows, 1 = Android, 2 = Other

	void Awake() {
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
			platform = 0;
		} else if (Application.platform == RuntimePlatform.Android) {
			platform = 1;
		}
	}
	
	// Use this for initialization
	void Start () {
		
		foreach (GameObject alirandata in aliranData) {
			alirandata.GetComponent<Animator> ().speed = 0;
			//alirandata.SetActive (false);
		}

		levelData = GetComponent<LevelData>();
		//levelData.gameStatus = (int)LevelData.GameStatus.GAMESTATUS_MENU;
		//fader = GameObject.Find ("UI").GetComponent<Fader> ();
		maxBrankas = 7;
		sendingHack = new bool[maxBrankas];
		collecting = new bool[maxBrankas];
		progressSendHack = new float[maxBrankas];
		highscore = PlayerPrefs.GetInt ("highscore", 0);
		StartUI ();
		if (instance == null)
			instance = this;
		//StartGame ();
	
	}

	// Update is called once per frame
	void Update () {
		/* Game Loop
		 * SendHack() akan dipanggil setiap levelData.hackDefaultDelay
		 * Hanya akan dicek ketika game sedang play
		 * Tidak boleh mengirim ke brankas yang sedang SendHack(), namun belum StartHack()
		 * Tidak boleh mengirim ke brankas yang sedang dihack (StartHack() sedang jalan) */
		if (!ready)
			return;
		if (levelData.gameStatus == (int)LevelData.GameStatus.GAMESTATUS_PLAY) {
			hackDelay -= Time.deltaTime;
			if (hackDelay <= 0 && hacksLeft > 0) {
				float doubleChance = Random.Range (0f, 1f);
				bool repeat = false;
				if (doubleChance <= 0.15f && hacksLeft > 2 && levelData.level >= 4) {
					repeat = true;
				}
				int id = Random.Range (0, brankasSpawned.Count);
				int idx = brankasSpawned [id].GetComponent<Brankas>().id;
				if (!sendingHack[idx] && brankasSpawned[id].GetComponent<Brankas>().status == (int)Brankas.Status.BRANKAS_CLOSED) {
					SendHack(id);
					sendingHack[idx] = true;
					collecting [idx] = true;
					if (repeat) {
						hackDelay = Random.Range (0.05f, 0.2f);
					} else {
						hackDelay = levelData.hackDefaultDelay * Random.Range(0.7f, 1.0f);
					}
					hacksLeft--;
				}
			}
			// Hacking left 0, sudah tidak ada hacking berlangsung -> Next level
			if (hacksLeft <= 0) {
				bool stillHacking = false;
				for (int i=0; i<maxBrankas; i++) {
					if (sendingHack[i]) {
						//Debug.Log ("SENDINGHACK");
						stillHacking = true;
						break;
					}
				}
				foreach (GameObject brankas in brankasSpawned) {
					Brankas brankasScript = brankas.GetComponent<Brankas>();
					if (brankasScript.status == (int)Brankas.Status.BRANKAS_OPENING || brankasScript.status == (int)Brankas.Status.BRANKAS_OPENED) {

						//Debug.Log ("STILLHACKING");
						stillHacking = true;
						break;
					}
				}
				if (!stillHacking) {
					//Debug.Log("!STILLHACKING");
					NextLevel();
				}
			}
		}

		if (levelData.gameStatus == (int)LevelData.GameStatus.GAMESTATUS_PLAY) {
			
			for (int i = 0; i < maxBrankas; i++) {
				if (sendingHack [i] && progressSendHack[i] <= 100) {
					progressSendHack [i] += Time.deltaTime * levelData.sendHackSpeed * levelData.sendHackSpeedMult;
					//Debug.Log ("PROGRESSING" + progressSendHack[i]);
					sendHackAnim = aliranData [i].GetComponent<Animator> ();
					sendHackAnim.Play ("alirandata" + (i + 1), -1, progressSendHack [i] / 100);
				}
				//Debug.Log (progressSendHack[i]+" "+i);
				if (sendingHack[i] && progressSendHack [i] > 100) {

					Debug.Log ("Starting Hack " + i);
					sendingHack [i] = false;
					StartHack (i);
				}
			}
		}
		/*
		if (platform == 1) {
			// Android
			Debug.Log("Android");
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
				if (levelData.gameStatus == (int)LevelData.GameStatus.GAMESTATUS_PLAY) {
					Debug.Log ("COLLECT ALL");
					CollectAll ();
				} else if (levelData.gameStatus == (int)LevelData.GameStatus.GAMESTATUS_MENU) {
					Debug.Log ("SPACE PRESSED");
					StartCoroutine (Fade (start, false));
					StartCoroutine (Fade (game, true));
					StartGame ();
					Debug.Log (levelData.gameStatus);
				} else if (Input.GetKeyDown (KeyCode.Space) && levelData.gameStatus == (int)LevelData.GameStatus.GAMESTATUS_END && !inAnimation) {
					Debug.Log ("SPACE PRESSED, RETRY");
					StartCoroutine (Fade (end, false));
					StartCoroutine (Fade (game, true));
					StartGame ();
					Debug.Log (levelData.gameStatus);
				} 
			}
		} 
		else {*/
			// Windows or others

			if ((Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) && levelData.gameStatus == (int) LevelData.GameStatus.GAMESTATUS_PLAY) {
				Debug.Log ("COLLECT ALL");
				CollectAll();
			}

			else if ((Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) && levelData.gameStatus == (int) LevelData.GameStatus.GAMESTATUS_MENU) {
				Debug.Log ("SPACE PRESSED");
				StartCoroutine (Fade (start, false));
				StartCoroutine (Fade (game, true));
				StartGame ();
				Debug.Log (levelData.gameStatus);
			}

			else if ((Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) && levelData.gameStatus == (int) LevelData.GameStatus.GAMESTATUS_END && !inAnimation) {
				Debug.Log ("SPACE PRESSED, RETRY");
				StartCoroutine (Fade (end, false));
				StartCoroutine (Fade (game, true));
				StartGame ();
				Debug.Log (levelData.gameStatus);
			} 
		//}

	}

	void StartUI() {
		start.alpha = 1;
		start.interactable = true;
		startHighscore.text = highscore.ToString ();
		game.alpha = 0;
		game.interactable = false;
		gameHighscore.text = highscore.ToString ();
		end.alpha = 0;
		end.interactable = false;
		endHighscore.text = highscore.ToString ();
		if (platform == 1) {
			startMsg.text = "Tap to Play!\nCollect money by tapping\nwhen vault is open!";
			endMsg.text = "Tap to retry!";
		} else {
			startMsg.text = "Press space to Play!\nCollect money by pressing space\nwhen vault is open!";
			endMsg.text = "Press space to retry!";
		}
	}

	void StartGame() {
		/* Mulai gamenya */
		//gameScore.text = levelData.money.ToString();
		//foreach (GameObject alirandata in aliranData) {
		//	alirandata.SetActive (false);
		//}
		ClearBrankas (true);
		InitLevelData();
		hackDelay = levelData.hackDefaultDelay;
		hacksLeft = levelData.hacksLeft;
		GenerateLevel();
		for (int i = 0; i < maxBrankas; i++) {
			sendingHack [i] = false;
			collecting [i] = false;
			progressSendHack[i] = 0;
		}
		SpawnBrankas();

	}

	public void GameOver() {
		// Game over
		Debug.Log("GAME OVER");
		endScore.text = levelData.money.ToString();
		if (levelData.money > highscore) {
			highscore = Mathf.RoundToInt (levelData.money);
			PlayerPrefs.SetInt ("highscore", highscore);
			PlayerPrefs.Save();
		}
		startHighscore.text = highscore.ToString();
		gameHighscore.text = highscore.ToString();
		endHighscore.text = highscore.ToString();
		levelData.gameStatus = (int)LevelData.GameStatus.GAMESTATUS_END;
		// Tampilin UI game over
		StartCoroutine(Fade(game,false)); 
		StartCoroutine (Fade (end, true));
	}

	void InitLevelData() {
		/* Init level data parameters */
		levelData.level = 1;
		levelData.sendHackSpeed = 75;
		levelData.sendHackSpeedMult = 1;
		levelData.hackSpeed = 100;
		levelData.hackSpeedMult = 1;
		levelData.hackDefaultDelay = 1.5f;
		levelData.money = 0;
		levelData.hacksLeft = 3;
		levelData.gameStatus = (int)LevelData.GameStatus.GAMESTATUS_PLAY;
		levelData.nBrankas = 1;
		gameScore.text = levelData.money.ToString();
	}

	void GenerateLevel() {
		/* Generate lokasi-lokasi brankas */
		int count = 0;
		while (count < levelData.nBrankas) {
			int idx = Random.Range (0, maxBrankas);
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
			newBrankas.GetComponent<Brankas> ().id = i;
			brankasSpawned.Add (newBrankas);
			sendingHack[i] = false;

		}	
	}

	void ClearBrankas(bool instant) {
		foreach (GameObject brankas in brankasSpawned) {
			brankas.GetComponent<BoxCollider>().enabled = false;
			if (!instant) {
				Destroy (brankas, 1.0f); 
			} else {
				Destroy(brankas);
			}
		}
		brankasSpawned = new List<GameObject>();
		brankasToSpawn = new List<int> ();

		for (int i = 0; i < maxBrankas; i++) {
			progressSendHack[i] = 0;
			sendHackAnim = aliranData [i].GetComponent<Animator> ();
			sendHackAnim.Play ("alirandata" + (i + 1), -1, progressSendHack [i] / 100);
		}

	}

	void NextLevel() {
		ClearBrankas(false);
		// Update parameter untuk next level
		if (levelData.level >= 7) {
			levelData.level++;
			levelData.sendHackSpeedMult += 0.1f;
			levelData.hackSpeedMult += 0.1f;
			levelData.hackDefaultDelay -= 0f;
			levelData.hacksLeft += 2;
		} 
		else {
			levelData.level++;
			levelData.sendHackSpeedMult += 0.2f;
			levelData.hackSpeedMult += 0.2f;
			levelData.hackDefaultDelay -= 0.2f;
			levelData.hacksLeft += 2;
			levelData.nBrankas++;
		}
		hacksLeft = levelData.hacksLeft;
		GenerateLevel();
		for (int i=0; i<maxBrankas; i++) sendingHack[i] = false;
		SpawnBrankas();

	}

	void SendHack(int brankasId) {
		/* Kirim hack ke brankas ke-brankasId */
		int idx = brankasSpawned [brankasId].GetComponent<Brankas>().id;
		//sendingHack[idx] = true;
		// Animasi SendHack
		/*aliranData[idx].SetActive(true);
		Animator anim = aliranData[idx].GetComponent<Animator>();
		int animIdx = idx + 1;
		string animationName = "alirandata" + animIdx;
		Debug.Log (animationName);
		Debug.Log (aliranData [idx].name);
		anim.Play (animationName);
		StartCoroutine (Wait (1,brankasId));*/
	}

	void StartHack(int brankasId) {
		/* Brankas ke-brankasId mulai kebuka */
		//int idx = brankasSpawned [brankasId].GetComponent<Brankas>().id;
		int i = 0;
		SoundManager.instance.playSFX (SoundManager.COINHACK);
		while (brankasId != brankasSpawned [i].GetComponent<Brankas> ().id) {
			i++;
		}

		Debug.Log ("HAcking");
		Brankas brankasScript = brankasSpawned[i].GetComponent<Brankas>();
		brankasScript.status = (int)Brankas.Status.BRANKAS_OPENING;

	}

	void Collect(GameObject brankas) {
		Brankas brankasScript = brankas.GetComponent<Brankas>();
		brankasScript.moneyGraphic.SetActive (true);
		brankasScript.moneyGraphic.GetComponent<Money> ().Animate();

		/* Collect money dari brankas */
		// Brankas pasti sudah bisa dicollect

		brankasScript.status = (int)Brankas.Status.BRANKAS_CLOSED;
		SendMoney(Mathf.Round(brankasScript.money * Mathf.Min(brankasScript.progress, 100f) / 100f * goingToCollect));
		Debug.Log ("Collect from brankas " + brankas);
		//Animator anim = brankas.GetComponent<Animator> ();
		//ffanim.SetBool ("isOpen", false);
		Debug.Log ("BRANKAS COLLECTED " + brankas); 
		//aliranData [brankas.GetComponent<Brankas> ().id].GetComponent<Animator>().speed = 1;
		//aliranData [brankas.GetComponent<Brankas> ().id].SetActive(false);
		brankasScript.progress = 0;

		progressSendHack [brankasScript.id] = 0;
		sendHackAnim = aliranData [brankasScript.id].GetComponent<Animator> ();
		sendHackAnim.Play ("alirandata" + (brankasScript.id + 1), -1, progressSendHack [brankasScript.id] / 100);
		collecting [brankasScript.id] = false;
	}

	void CollectAll() {
		/* Collect money dari seluruh brankas */
		// Cek apakah ada brankas yang kebuka
		bool anyBrankasOpen = false;
		goingToCollect = 0;
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
					goingToCollect++;
					Collect(brankas);
				}
			}
		}

	}

	void SendMoney(float money) {
		/* Kirim money dari brankas ke-brankasId ke tas */
		levelData.money += money;
		Debug.Log (levelData.money);
		gameScore.text = levelData.money.ToString();
	}

	IEnumerator Wait(float sec, int hackId) {
		//yield return new WaitForSeconds(sec);
		//Animator anim = brankasSpawned [hackId].GetComponent<Animator> ();
		//anim.SetBool ("isOpen", true);
		yield return new WaitForSeconds(sec);
		StartHack (hackId);
	}

	IEnumerator Fade(CanvasGroup group, bool fadeIn) {
		inAnimation = true;
		if (fadeIn) {
			while (group.alpha < 1) {
				group.alpha += Time.deltaTime / 2 * fadeSpeed;
				yield return null;
			}
			group.interactable = true;
		} else {
			while (group.alpha > 0) {
				group.alpha -= Time.deltaTime / 2 * fadeSpeed;
				yield return null;
			}
			group.interactable = false;
		}
		inAnimation = false;
	}

	public void Credits() {
		SceneManager.LoadScene ("scAboutus");
	}

}
