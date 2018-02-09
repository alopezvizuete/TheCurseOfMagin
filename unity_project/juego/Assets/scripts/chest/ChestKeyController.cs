using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestKeyController : MonoBehaviour {

    GameObject chest;
    GameObject chestPickController;

    public GameObject sfxPrefab;

    // Use this for initialization
    void Start()
    {
        chest = GameObject.FindGameObjectWithTag("Chest");
        chestPickController = GameObject.FindGameObjectWithTag("ChestPickController");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            chest.GetComponent<ChestController>().state = ChestController.State.OPENING;
            chestPickController.GetComponent<ChestPickController>().activated = true;
            Instantiate(sfxPrefab);
            Destroy(gameObject);
        }
    }
}
