using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class About : MonoBehaviour {
	public GameObject content;
	public Transform target;
	public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, target.position, step);

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel (1);
		}
	}

	public void MainMenu() {
		SceneManager.LoadScene ("sc1");
	}
}
