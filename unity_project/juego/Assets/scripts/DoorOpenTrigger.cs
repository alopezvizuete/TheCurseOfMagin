using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour {

    GameObject player;
    DoorController houseDoor;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        houseDoor = transform.GetComponentInParent<DoorController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (houseDoor.state == DoorController.State.CLOSED)
                houseDoor.state = DoorController.State.OPENING;
        }
    }
}
