using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Safebox : MonoBehaviour {

	// Use this for initialization
	public float openTime;
	float t = 0;
	public float opennes = 0; // 0-150, 50 = ok-ish,100 = perfect, 150 = miss;
	int ok = 50;
	int perfect = 100;
	int miss = 150;
	bool collected = false;
	public int money = 100;
	public Text stats;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (t < openTime && !collected) {
			t += Time.deltaTime;
			opennes = Mathf.RoundToInt (t / openTime * 150);
			stats.text = "" + opennes;
		}
	}
	public int collect(){
	//	if (collected)
	//		return 0;
		collected = true;
		if (opennes <= perfect) {
			Debug.Log ((opennes / perfect) * money);
			Debug.Log (opennes / perfect);
			return Mathf.FloorToInt ((opennes/perfect) * money);
		} else if (opennes <= miss) {
			Debug.Log (((miss - opennes)/perfect) * money);
			Debug.Log (miss - opennes);
			return Mathf.FloorToInt (((miss - opennes)/perfect) * money);
		} else {
			return 0;
		}

	}
}
