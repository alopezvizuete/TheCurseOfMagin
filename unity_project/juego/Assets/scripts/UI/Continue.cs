using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour {
	public Transform canvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void continueGame()
	{
		Transform child = canvas.gameObject.transform.FindChild ("PauseChild");
		child.gameObject.SetActive(false);
		Time.timeScale = 1;
		CharacterControl.movementEnabled = true;
	}
}
