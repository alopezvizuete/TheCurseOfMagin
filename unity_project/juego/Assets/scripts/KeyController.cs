using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour {

   private GameObject puerta;

    // Use this for initialization
    void Start () {
		puerta = GameObject.FindGameObjectWithTag("Door");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            puerta.GetComponent<DoorController>().state = DoorController.State.OPENING;
            Destroy(gameObject);
        }
    }

}
