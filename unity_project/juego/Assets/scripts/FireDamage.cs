using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour {
	PlayerUI player;
	int damageToDead=101;
	// Use this for initialization
	void Start () {
		player =GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerUI> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
			player.TakeDamage (damageToDead);
	}
}
