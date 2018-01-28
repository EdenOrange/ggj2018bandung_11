using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class About : MonoBehaviour {
	public GameObject content;
	public GameObject image;

	private Vector2 endContent;
	private Vector2 endImage;
	private float speed;
	// Use this for initialization
	void Start () {
		endContent = new Vector2(Screen.width/2, Screen.height * 3);
		endImage = new Vector2(Screen.width/2, Screen.height / 2);
		speed = 80f;
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		content.transform.position = Vector3.MoveTowards (content.transform.position, endContent, step);
		image.transform.position = Vector3.MoveTowards (image.transform.position, endImage, step);

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel (1);
		}
	}

	public void MainMenu() {
		SceneManager.LoadScene ("sc1");
	}
}
