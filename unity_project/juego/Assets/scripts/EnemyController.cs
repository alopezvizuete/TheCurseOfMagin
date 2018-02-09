using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;

public class EnemyController : MonoBehaviour {

	public int hitPoint = 4;
	GameObject player;
	PlayerUI playerHealth;
	public int attackDamage = 10;
	AIRig tRig;
	float timer=0;
	public GameObject deadSound;
	public GameObject attackSound;
	private bool attack=false;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerUI> ();
		tRig = gameObject.GetComponentInChildren<AIRig>();
	}

	// Update is called once per frame
	void Update () {
		if (tRig.AI.Animator.IsStatePlaying ("attack")) {
			if (attack) {
				GameObject AttackSound = Instantiate (attackSound, transform.position, transform.rotation);
				Destroy (AttackSound,0.5f);
			}
			attack = false;
			timer += Time.deltaTime;
			if (timer >= 0.3f ) {//tiempo que transcurre desde que inicia la animacion hasta que ataca.
				playerHealth.TakeDamage (attackDamage);
				attack = true;
				timer = -0.183f;
			}
		}
		if (!tRig.AI.Animator.IsStatePlaying ("attack")) {
			timer = 0f;
		}
		
	}
	void OnTriggerEnter(Collider other)
	{
		if ((other.tag == "Magic") || (other.tag=="Player") || other.tag == "NormalAttack") {
			hitPoint--;
			if (hitPoint <= 0) {
				GameObject DeathSound = Instantiate(deadSound, transform.position, transform.rotation);
				tRig.AI.WorkingMemory.SetItem<bool>("dead", true);
				Destroy (gameObject,2.5f);//Tiempo que dura la animacion de muerte.
				Destroy (DeathSound,2.5f);
				SpawnPointManager.currentEnemies--;
			}
		}
	}
}
