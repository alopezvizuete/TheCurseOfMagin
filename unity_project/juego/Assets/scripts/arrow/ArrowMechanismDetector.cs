using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMechanismDetector : MonoBehaviour {

    public float delay = 0.2f;
    public ArrowMechanism arrowMechanism;

    private float timeToShoot;

	// Use this for initialization
	void Start () {
        timeToShoot = -1;   // -1 means not triggered
	}
	
	// Update is called once per frame
	void Update () {
		if(timeToShoot >= 0)
        {
            timeToShoot += Time.deltaTime;
            if(timeToShoot > delay)
            {
                arrowMechanism.activate();
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            timeToShoot = 0;
        }
    }
}
