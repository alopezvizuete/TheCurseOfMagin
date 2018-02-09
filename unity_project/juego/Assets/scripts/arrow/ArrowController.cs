using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    public float speed = 3;
    public int damage = 60;
    public GameObject particleEmiBreak;

    private GameObject smokeEmitter;
    GameObject player;
    PlayerUI playerHealth;

    private float TIME_COLLI_FREE = 0.2f;
    float timeSinceCreation = 0;

    // Use this for initialization
    void Start () {
        smokeEmitter = transform.Find("smoke_emitter").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerUI>();
    }
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        Vector3 inc = speed * transform.forward * dt;
        transform.position += inc;
        timeSinceCreation += dt;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (timeSinceCreation > TIME_COLLI_FREE &&
            other.gameObject.tag != "Detector" &&
            other.gameObject.tag != "InvisibleBlock")
        {
            if (other.tag == "Player")
            {
                playerHealth.TakeDamage(damage);
            }

            smokeEmitter.GetComponent<ParticleSystem>().Stop();

            GameObject partEmi = Instantiate(particleEmiBreak);
            partEmi.transform.position = transform.position;

            Destroy(transform.Find("arrow_mesh").gameObject);
            Destroy(gameObject, 3);
            Destroy(partEmi, 3);
        }
    }

}
