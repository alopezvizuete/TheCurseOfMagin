using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportPlatforms: MonoBehaviour {

	public Transform Target;
	GameObject player;
	GameObject camera;
	public GameObject teleport;
	public GameObject sound;
	public bool Activated;
	private bool entered;
	private float timer=0;
	private Vector3 distance;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		camera= GameObject.FindGameObjectWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if (entered) {
			timer += Time.deltaTime;
			if (timer > 1.3) {
				distance = Target.position - player.transform.position;
				player.transform.position = Target.position;
				camera.transform.position += distance;
				entered = false;
				CharacterControl.movementEnabled = true;
				timer = 0;
			}
		}
		
	}
	void OnTriggerEnter(Collider other)
	{
		if (Activated == true && other.tag=="Player") {
			entered = true;
			CharacterControl.movementEnabled = false;
			GameObject teleportParticles = Instantiate(teleport, transform.position, transform.rotation);
			GameObject teleportSound = Instantiate(sound, transform.position, transform.rotation);
			Destroy(teleportParticles, 1.5f);
			Destroy(teleportSound, 2.0f);
			if(Target!=null&& Target.GetComponent<TransportPlatforms>()!=null)
			Target.GetComponent<TransportPlatforms>().Activated = false;
		}
	}
	void OnTriggerExit(Collider other){
			Activated = true;
	}
}
