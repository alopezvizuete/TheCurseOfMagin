using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomControl : MonoBehaviour {
	PlayerUI playerHealth;
	int attackDamage=10;
	public float Speed = 8.0f;
	public float lifeTime = 3.0f;
	//public GameObject VenomExplosionPrefav;
	//private float explosionDelay = 1.0f;

	// Use this for initialization
	void Start () {
		playerHealth =GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerUI> ();
		Destroy(gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Speed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			playerHealth.TakeDamage (attackDamage);
			Destroy (gameObject);
		}

		if (other.tag != "Player" && other.tag != "InvisibleBlock") {
			Destroy (gameObject);
		}
	}
}
