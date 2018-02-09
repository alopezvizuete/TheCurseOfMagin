using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphereControlMagin : MonoBehaviour
{

    public float Speed = 10.0f;
    public float lifeTime = 3.0f;
    public GameObject explosionPrefav;

    private float explosionDelay = 1.0f;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Detector" &&
           other.gameObject.tag != "InvisibleBlock")
        {
            GameObject explosionObject = Instantiate(explosionPrefav, transform.position, transform.rotation);
            Destroy(explosionObject, explosionDelay);
            Destroy(gameObject);
        }
    }
}
