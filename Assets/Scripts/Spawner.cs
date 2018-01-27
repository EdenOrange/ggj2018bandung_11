using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	// Use this for initialization
	public GameObject[] spawnPlace;
	public GameObject[] spawned;
	public GameObject prefab;
	public int safeBoxNum;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			Spawn ();
		}
	}
	public void Spawn(){
		if (safeBoxNum <= 0)
			safeBoxNum = 0;
		else if (safeBoxNum > spawnPlace.Length)
			safeBoxNum = spawnPlace.Length;
		spawned = new GameObject[safeBoxNum];
		for (int i=0;i<safeBoxNum;i++){
			spawned[i] = Instantiate(prefab,spawnPlace[i].transform.position,spawnPlace[i].transform.rotation);
		}
	}
}
