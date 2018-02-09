using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class YesButton : MonoBehaviour {

    public GameObject estado;
    public int vidas;
    // Use this for initialization

    void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void LoadScene()
    {
        estado = GameObject.FindGameObjectWithTag("Estado");
        vidas =  estado.GetComponent<EstadoJuego>().vidasActuales;
        estado.GetComponent<EstadoJuego>().vida = 100;
        if (vidas == 0) {
            estado.GetComponent<EstadoJuego>().vidasActuales = 3;
            estado.GetComponent<EstadoJuego>().beginStoryShowed = false;
            estado.GetComponent<EstadoJuego>().beginStoryShowedHouse = false;
            estado.GetComponent<EstadoJuego>().beginStoryShowedToWest = false;
            estado.GetComponent<EstadoJuego>().fistDungeonCompleted = false;
            estado.GetComponent<EstadoJuego>().cauldronAnimationShowed = false;
            estado.GetComponent<EstadoJuego>().pickTheRightIngredientShowed = false;
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene(Application.loadedLevel);
        }
    }

}

