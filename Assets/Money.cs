using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Money : MonoBehaviour {

	// Use this for initialization
	public GameObject finalPos;
	public bool animate;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Animate(){
		if (animate)
			return;
		finalPos = GameObject.Find ("MoneyStash");
		animate = true;
		gameObject.transform.DOMove (finalPos.transform.position, 1f).OnComplete (() => {
			Vector3 lastPos = finalPos.transform.position;
			lastPos.y += 5f;
			gameObject.transform.DOMove(lastPos,0.5f).OnComplete(Reset);
		});
	}
	void Reset(){
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.SetActive (false);
		animate = false;
	}
}
