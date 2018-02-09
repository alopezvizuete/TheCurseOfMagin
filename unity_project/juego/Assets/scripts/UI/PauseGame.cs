using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
	public  GameObject player;
	public Transform canvas;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Pause();
		}
	}

	public void Pause()
	{
		Transform child = canvas.gameObject.transform.FindChild ("PauseChild");
		if (child.gameObject.activeSelf== false)
		{
			child.gameObject.SetActive(true);
			Time.timeScale = 0;
			CharacterControl.movementEnabled = false;
		}
		else
		{
			child.gameObject.SetActive(false);
			Time.timeScale = 1;
			CharacterControl.movementEnabled = true;
		}
	}
}
