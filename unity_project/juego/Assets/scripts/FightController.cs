using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour {

    public GameObject fightMagin;
    public GameObject deadMagin;
    public GameObject goodMagin;

    public GameObject followCamera;
    public GameObject cinematicCamera;

    public GameObject particleSystem;

	// Use this for initialization
	void Start () {

        EstadoJuego estadoJuego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
        Cinematic cinematic = transform.Find("final_cinematic").GetComponent<Cinematic>();

        particleSystem.SetActive(false);

        if (!estadoJuego.fightWinned)
        {
            // we haven't winned the battle yet
            deadMagin.SetActive(false);
            goodMagin.SetActive(false);
            cinematicCamera.SetActive(false);
        }
        else
        {
            // we have won the battle, then play the cinematic
            fightMagin.SetActive(false);
            goodMagin.SetActive(false);
            followCamera.SetActive(false);
            cinematic.state = Cinematic.State.START;
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
