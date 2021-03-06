using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour {

	private bool gameStarted = false;
	private bool gameOver = false;
	private bool inAnimation = false;

	public CanvasGroup start;
	public CanvasGroup game;
	public CanvasGroup end;

	public float fadeSpeed = 1;

	// Use this for initialization
	void Start () {

		start.alpha = 1;
		start.interactable = true;
		game.alpha = 0;
		game.interactable = false;
		end.alpha = 0;
		end.interactable = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		/*if (Input.touchCount > 0 && !inAnimation) {
			if (!gameStarted) {
				gameStarted = true;
				StartCoroutine (Fade (start, false));
				StartCoroutine (Fade (game, true));
			} else if (!gameOver) {
				gameOver = true;
				StartCoroutine (Fade (end, true));
				StartCoroutine (Fade (game, false));
			} else {
				gameOver = false;
				StartCoroutine (Fade (end, false));
				StartCoroutine (Fade (game, true));
			}
		}*/
		
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
