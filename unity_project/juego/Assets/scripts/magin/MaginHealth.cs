using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaginHealth : MonoBehaviour {

    public int ataquesRecibidos = 4;
    public GameObject forceFieldPrefav;
    public float forceFieldRegenTime = 5;

    Transform forceFieldCenter;

    float t;
    bool forceFieldActiveInPrevFrame;
    
	// Use this for initialization
	void Start () {
        forceFieldCenter = GameObject.Find("ForceFieldCenter").transform;
        forceFieldActiveInPrevFrame = true;
    }
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;

        // force field regeneration
        // detect force field broken
        if(forceFieldActiveInPrevFrame && !isForceFieldActive())
        {
            t = 0;
            forceFieldActiveInPrevFrame = false;
        }
        else if(!isForceFieldActive())
        {
            t += dt;
            if(t >= forceFieldRegenTime)
            {
                // regenerate
                GameObject forceField = GameObject.Instantiate(forceFieldPrefav);
                forceField.transform.SetParent(forceFieldCenter);
                forceField.transform.localPosition = new Vector3(0, 0, 0);
                forceFieldActiveInPrevFrame = true;
            }
        }

        if (ataquesRecibidos == 0)
        {
            EstadoJuego estadoJuego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
            estadoJuego.fightWinned = true;
            SceneManager.LoadScene("fight");
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Magic")
        {
            ataquesRecibidos--;

        }
    }

    bool isForceFieldActive()
    {
        return forceFieldCenter.childCount > 0;
    }
}
