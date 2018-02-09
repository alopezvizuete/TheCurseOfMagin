using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EstadoJuego : MonoBehaviour {

    public int vida = 100;
    public static EstadoJuego estadoJuego;
    //Sistema Vidas
    public int vidasMaximas = 6;
    public int vidasActuales = 3;
    public bool beginStoryShowed = false;
    public bool beginStoryShowedHouse = false;
    public bool beginStoryShowedToWest = false;

    public bool fistDungeonCompleted = false;
    public bool cauldronAnimationShowed = false;
    public bool pickTheRightIngredientShowed = false;

    public bool fightWinned = false;

    void Awake()
    {
        if (estadoJuego == null)
        {
            estadoJuego = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (estadoJuego != this) {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void TakeDamage(int amount)
    {
        vida -= amount;
    }
    public void QuitarVida()
    {
        vidasActuales--;
        vida = 100;
    }
    public void AumentarVida()
    {
        vidasActuales++;
    }
}
