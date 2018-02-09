using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
	public int startingHealth = 100; //VIDA MAXIMA                          
	public int currentHealth = 100;        //VIDA ACTUAL              
	public Slider healthSlider;      //SLIDER DE VIDA       
	public int startingMagic = 100; //MAGIA MAXIMA                          
	public float currentMagic= 100;        //MAGIA ACTUAL  
	public int costMagic = 10;        //MAGIA QUE SE REDUCE
	public Slider magicSlider;      //SLIDER DE MAGIA    
	public Image damageImage;        //IMAGEN PARA FLASH DE ATAQUE   
                      
	//public AudioClip deathClip;                            
	public float flashSpeed = 5f;                               
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    private GameObject estado;
    bool fading = false;
    public  GameObject fadeImg;
    public  GameObject player;
    public GameObject deathUI;
    public GameObject fakeTib;
    public GameObject mesh;
    int vidas;
    public GameObject[] corazonesEnteros;
    public GameObject[] corazonesTransparentes;
    Animator anim;                                             

	public GameObject heartSound;
	//CharacterControl playerMovement;                              
    //ShootMagicSphere playerShooting;                             
    bool isDead;                                               
	bool damaged;                                               

	void Awake ()
	{
        anim = GetComponent <Animator> ();
        //playerAudio = GetComponent <AudioSource> ();
        //playerMovement = GetComponent <CharacterControl> ();
        //playerShooting = GetComponent <ShootMagicSphere> ();
        estado = GameObject.FindGameObjectWithTag("Estado");
        vidas = estado.GetComponent<EstadoJuego>().vidasActuales;
        pintarCorazones();

    }
    private void Start()
    {
        currentHealth = estado.GetComponent<EstadoJuego>().vida;

    }


    void Update ()
	{
        float dt = Time.deltaTime;
		if(damaged) //Si ha sufrido daños
		{
			damageImage.color = flashColour;
		}
		else //Si no..
		{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		damaged = false;

        currentHealth = estado.GetComponent<EstadoJuego>().vida;
        healthSlider.value = currentHealth;

        if (currentMagic < startingMagic) {
			currentMagic += 2.0f * Time.deltaTime;
			magicSlider.value = currentMagic;
		}

    }


	public void TakeDamage (int amount)
	{
		damaged = true;

		currentHealth -= amount;

		healthSlider.value = currentHealth;

        //playerAudio.Play ();

        estado.GetComponent<EstadoJuego>().TakeDamage(amount);



        if (currentHealth <= 0 && !isDead)
		{
            estado.GetComponent<EstadoJuego>().QuitarVida();
            vidas--;
            Death();
        }

	}
	public void TakeMagic (int MagicAmount)
	{
		currentMagic -= MagicAmount;
		magicSlider.value = currentMagic;

	}

    void pintarCorazones() {
        int vidaMax = estado.GetComponent<EstadoJuego>().vidasMaximas;
        int vidaactual = estado.GetComponent<EstadoJuego>().vidasActuales;

        for (int i = 0; i < vidaactual; i++) {
            corazonesEnteros[i].SetActive(true);
        }
        for (int i = vidaactual; i < vidaMax; i++)
        {
            corazonesTransparentes[i].SetActive(true);
        }

    }

    void Death ()
	{
		isDead = true;

        //anim.SetTrigger ("death");

        Destroy(mesh);
        Vector3 abajo = new Vector3(0.0f, 0.22f, 0.0f);
        GameObject DeathTiberius = Instantiate(fakeTib, transform.position-abajo, transform.rotation);

        //playerAudio.clip = deathClip;
        //playerAudio.Play ();

        //playerMovement.enable = false;
        //playerShooting.enabled = false;

        player.GetComponent<CharacterControl>().enableMovement(false);

        //Fading appear
        //fadeImg.GetComponent<FadeController>().state = FadeController.State.FADING;
        
        estado.GetComponent<EstadoJuego>().vida = 100;

        //Death menu appear
        GameObject.Instantiate(deathUI);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DarkMagic")
        {
            this.TakeDamage(30);
        }
        else if (other.gameObject.tag == "Corazon")
        {
            int vidaAct = estado.GetComponent<EstadoJuego>().vidasActuales;
            int vidMax = estado.GetComponent<EstadoJuego>().vidasMaximas;
            if (vidaAct < vidMax)
            {
				GameObject lifeSound=Instantiate(heartSound, transform.position,transform.rotation);
                other.gameObject.SetActive(false);
                estado.GetComponent<EstadoJuego>().AumentarVida();
                pintarCorazones();
				Destroy (lifeSound,1.5f);
            }
        }
    }

}