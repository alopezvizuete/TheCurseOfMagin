using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveEnter : MonoBehaviour {

	public string targetLevel;

    GameObject player;

    bool fading = false;
    GameObject fadeImg;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        fadeImg = canvas.transform.Find("FadeImage").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(fading && fadeImg.GetComponent<FadeController>().state == FadeController.State.FADED)
        {
            SceneManager.LoadScene(targetLevel);
			CameraControl.inCastle = false;
        }
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
            player.GetComponent<CharacterControl>().enableMovement(false);
            fading = true;
            fadeImg.GetComponent<FadeController>().fadingSpeed = 1;
            fadeImg.GetComponent<FadeController>().state = FadeController.State.FADING;
		}
	}
}
