using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	// Use this for initialization
	public static SoundManager instance;
	public List<AudioClip> bgmClips;
	public List<AudioClip> sfxClips;
	public static int COINHACK = 0;
	public static int COINCOLLECT = 3;
	public static int BRANKASSPAWN = 2;
	public static int BRANKASDROP= 1;

	void Start () {
		if (instance == null) {
			instance = this;
			playBGM ();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void playBGM(){
		GetComponents<AudioSource> () [0].clip = bgmClips [0];
		GetComponents<AudioSource> () [0].Play ();
	}
	public void playSFX(int id){
	//	GetComponents<AudioSource> () [1].Stop ();
		if (id == COINCOLLECT) {
			GetComponents<AudioSource> () [2].clip = sfxClips [id];
			GetComponents<AudioSource> () [2].Play ();
		} else {
			GetComponents<AudioSource> () [1].clip = sfxClips [id];
			GetComponents<AudioSource> () [1].Play ();
		}
	}
}
