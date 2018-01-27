using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brankas : MonoBehaviour {

	public int id;
	public float progress; // 0..openLimit..maxLimit
	public float openLimit;
	public float maxLimit;
	public int status;
	public enum Status {
		BRANKAS_CLOSED,
		BRANKAS_OPENING,
		BRANKAS_OPENED
	}

	private LevelData levelData;

	// Use this for initialization
	void Start () {
		levelData = GameObject.FindWithTag("GameController").GetComponent<LevelData>();
		// id = diatur spawner
		progress = 0;
		openLimit = 100;
		maxLimit = 150;
		status = (int)Status.BRANKAS_CLOSED;
	}
	
	// Update is called once per frame
	void Update () {

		if (progress < maxLimit && status != (int) Status.BRANKAS_CLOSED) {
			if (status == (int)Status.BRANKAS_OPENING && progress > openLimit) {
				status = (int)Status.BRANKAS_OPENED;
			}
			progress += Time.deltaTime * levelData.hackSpeed;
		} else {
			status = (int)Status.BRANKAS_CLOSED;
		}


	}


}
