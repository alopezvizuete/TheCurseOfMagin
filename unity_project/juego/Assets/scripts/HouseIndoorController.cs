using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseIndoorController : MonoBehaviour {

    EstadoJuego estadoJuego;

    // Use this for initialization
    void Start () {
        estadoJuego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();

        // the cauldron is not active at the begining
        Cauldron cauldron = GameObject.Find("Room/cauldron")
            .GetComponent<Cauldron>();
        cauldron.enableLiquid = estadoJuego.beginStoryShowedHouse;
        cauldron.enableFire = estadoJuego.beginStoryShowedHouse;
        cauldron.enableSmoke = estadoJuego.beginStoryShowedHouse;

        if(!estadoJuego.beginStoryShowedHouse)
        {
            // play the first cinematic inside the house
            Cinematic cinematic =
                transform.Find("cinematic_1").GetComponent<Cinematic>();
            cinematic.state = Cinematic.State.START;

            estadoJuego.beginStoryShowedHouse = true;
        }
        else if (estadoJuego.fistDungeonCompleted)
        {
            // the fist dungeon has been completed

            if(!estadoJuego.pickTheRightIngredientShowed)
            {
                // moral thoughts cinematic
                Cinematic cinematic =
                    transform.Find("moral_thoughts").GetComponent<Cinematic>();
                cinematic.state = Cinematic.State.START;
            }

            else if (!estadoJuego.cauldronAnimationShowed)
            {
                // good cauldron cinematic
                Cinematic cinematic =
                    transform.Find("good_cauldron").GetComponent<Cinematic>();
                cinematic.state = Cinematic.State.START;

                estadoJuego.cauldronAnimationShowed = true;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
