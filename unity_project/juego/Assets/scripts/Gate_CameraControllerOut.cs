﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_CameraControllerOut : MonoBehaviour {

	public static bool inside=false;
	int health;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		health = GameObject.Find ("Tiberius").GetComponent<PlayerUI> ().currentHealth;
		if (health<=0) {
			inside = false;
			CameraControl.inCastle = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" && inside == true) {
			CameraControl.inCastle = false;
			inside = false;
			Gate_CameraControllerIn.inside = false;

		}
	}
}
