using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphereControl : MonoBehaviour {
	
    public float Speed = 10.0f;
    public float lifeTime = 3.0f;
	public GameObject explosionPrefav;
	private Vector3 dir;
	private GameObject tiberius;


	private float explosionDelay = 1.0f;

    // Use this for initialization
    void Start () {
        Destroy(gameObject, lifeTime);
		tiberius = GameObject.FindGameObjectWithTag ("Player");
		dir=tiberius.gameObject.GetComponentInChildren<AttackController>().shootDir;
		dir.y = 0.4f;
		transform.position = new Vector3 (transform.position.x,0.4f,transform.position.z);
		transform.LookAt(dir);
	}

	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Speed * Time.deltaTime;

	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Detector" &&
           other.gameObject.tag != "InvisibleBlock" &&
           other.gameObject.tag != "Player")
        {
            GameObject explosionObject = Instantiate(explosionPrefav, transform.position, transform.rotation);
            Destroy(explosionObject, explosionDelay);
            Destroy(gameObject);
        }
    }
}
