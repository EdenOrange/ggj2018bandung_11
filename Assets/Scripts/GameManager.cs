using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	//public Level level
	public float hackSpeed;
	public float hackSpeedHalt;
	public int money;
	public List<int> highScores;
	public int hackLeft;
	public int gameState;
	public static int WIN = 1;
	public static int LOSE = 0;
	public static GameManager instance;
	void Start () {
		if (instance == null)
			instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
