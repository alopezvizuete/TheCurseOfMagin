using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackController : MonoBehaviour {

    public float LIFE_TIME = 0.2f;

    public GameObject dustPrefav;
    public float DUST_LIFE_TIME = 2.0f;
    bool dustEmitted;

	// Use this for initialization
	void Start () {
        dustEmitted = false;
        Destroy(gameObject, LIFE_TIME);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Detector" &&
           other.gameObject.tag != "InvisibleBlock" &&
           other.gameObject.tag != "Player")
        {
            if(!dustEmitted)
            {
                GameObject dust = Instantiate(dustPrefav, transform.position, transform.rotation);
                Destroy(dust, DUST_LIFE_TIME);
                dustEmitted = true;
            }
        }
    }
}
