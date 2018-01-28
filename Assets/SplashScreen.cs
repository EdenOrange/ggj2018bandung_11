using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SplashScreen : MonoBehaviour {

	// Use this for initialization
	public GameObject[] toHide;
	public Image splashScreen;
	void Start () {
		foreach (GameObject go in toHide)
			go.SetActive (false);
		Color c = splashScreen.color;
		c.a = 255;
		splashScreen.DOColor(c,1f).OnComplete(() =>{
			splashScreen.DOColor(Color.clear,1f).OnComplete(() =>
				{
					foreach (GameObject go in toHide)
						go.SetActive (true);
					LevelManager.ready = true;

				});
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
