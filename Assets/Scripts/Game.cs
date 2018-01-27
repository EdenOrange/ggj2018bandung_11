using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	// Use this for initialization
	public Safebox[] boxes;
	public int money;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			foreach (Safebox box in boxes) {
				money += box.collect ();
				Debug.Log ("Money : "+money);
			}
		}
		//Debug.Log (money);
	}
}
