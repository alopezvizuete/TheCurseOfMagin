using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controlls what happens when loading the forest scene

public class ForestController : MonoBehaviour {

    EstadoJuego estadoJuego;

	// Use this for initialization
	void Start () {

        estadoJuego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();

        // the letter is only active in the first scene
        GameObject.Find("Escenario/letter").SetActive(
            !estadoJuego.beginStoryShowed);

        // open the gate if first dungeon completed
        GameObject closedGate = GameObject.Find("Escenario/Gate");
        GameObject openedGate = GameObject.Find("Escenario/OpenGate");
        closedGate.SetActive(!estadoJuego.fistDungeonCompleted);
        openedGate.SetActive(estadoJuego.fistDungeonCompleted);

        if (!estadoJuego.beginStoryShowed)
        {
            // play the cinematic of the begining
            Cinematic cinematic =
                transform.Find("cinematic_beginning").GetComponent<Cinematic>();
            cinematic.state = Cinematic.State.START;

            estadoJuego.beginStoryShowed = true;
        }
        else if(!estadoJuego.beginStoryShowedToWest)
        {
            // direct player towrds west
            Cinematic cinematic =
                transform.Find("cinematic_to_west").GetComponent<Cinematic>();
            cinematic.state = Cinematic.State.START;

            estadoJuego.beginStoryShowedToWest = true;
        }
        else if(estadoJuego.fistDungeonCompleted)
        {
            // the fist dungeon has been completed

            if (!estadoJuego.pickTheRightIngredientShowed)
            {
                // play pick the right ingredient cinematic
                Cinematic cinematic =
                transform.Find("cinematic_pick_the_right_ingredient").GetComponent<Cinematic>();
                cinematic.state = Cinematic.State.START;

                estadoJuego.pickTheRightIngredientShowed = true;
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
