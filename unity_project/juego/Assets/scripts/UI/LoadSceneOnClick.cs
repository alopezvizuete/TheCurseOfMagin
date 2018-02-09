using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadSceneOnClick : MonoBehaviour {

    public GameObject estado;


    public void LoadSceneById(int sceneId)
	{
        if (sceneId == 0 )
        {
            estado = GameObject.FindGameObjectWithTag("Estado");
            estado.GetComponent<EstadoJuego>().vida = 100;
            estado.GetComponent<EstadoJuego>().vidasActuales = 3;
            estado.GetComponent<EstadoJuego>().beginStoryShowed = false;
            estado.GetComponent<EstadoJuego>().beginStoryShowedHouse = false;
            estado.GetComponent<EstadoJuego>().beginStoryShowedToWest = false;
            estado.GetComponent<EstadoJuego>().fistDungeonCompleted = false;
            estado.GetComponent<EstadoJuego>().cauldronAnimationShowed = false;
            estado.GetComponent<EstadoJuego>().pickTheRightIngredientShowed = false;
            SceneManager.LoadScene("MainMenu");
        }
        SceneManager.LoadScene(sceneId);
	}
}
