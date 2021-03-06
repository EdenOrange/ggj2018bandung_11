using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader2 : MonoBehaviour {

	private bool gameStarted = false;
	private bool gameOver = false;
	private bool inAnimation = false;

	public CanvasGroup start;
	public CanvasGroup game;
	public CanvasGroup end;

	public Text score;

	public float fadeSpeed = 1;

	// Use this for initialization
	void Start () {

		game.alpha = 1;
		game.interactable = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
