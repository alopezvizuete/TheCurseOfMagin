using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMechanism : MonoBehaviour {

    public GameObject arrowType;
    public float arrowSpeed = 3;
    public int numberOfCharges = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void activate()
    {
        if(numberOfCharges > 0)
        {
            numberOfCharges--;
            shootArrow();
        }
    }

    public void shootArrow()
    {
        GameObject arrow = Instantiate(arrowType);
        arrow.transform.position = this.transform.position;
        arrow.transform.rotation = this.transform.rotation;
        arrow.GetComponent<ArrowController>().speed = arrowSpeed;
    }
}
