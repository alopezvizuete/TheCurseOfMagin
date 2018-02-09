using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour {

	public GameObject enemy;
	public int maxNumEnemies=3;
	public static int currentEnemies=0;
	public float spawnTime = 3f;
	public Transform[] spawnPoints;

	EstadoJuego estadoJuego;

	// Use this for initialization
	void Start () {
		estadoJuego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
		InvokeRepeating ("spawnFunc", spawnTime,spawnTime);
	}
	void spawnFunc(){
        Cinematic c1 = GameObject.Find("ForestController/cinematic_beginning").GetComponent<Cinematic>();
        Cinematic c2= GameObject.Find("ForestController/cinematic_pick_ingredients").GetComponent<Cinematic>();
        Cinematic c3 = GameObject.Find("ForestController/cinematic_to_house").GetComponent<Cinematic>();
        Cinematic c4 = GameObject.Find("ForestController/cinematic_to_west").GetComponent<Cinematic>();
        Cinematic c5 = GameObject.Find("ForestController/cinematic_pick_the_right_ingredient").GetComponent<Cinematic>();
        if (!(c1.state == Cinematic.State.DOING ||
            c2.state == Cinematic.State.DOING ||
            c3.state == Cinematic.State.DOING ||
            c4.state == Cinematic.State.DOING ||
            c5.state == Cinematic.State.DOING)
        )
        {
			if (currentEnemies < maxNumEnemies) {
				int spawnPointIndex = Random.Range (0, spawnPoints.Length);
				Instantiate (enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
				currentEnemies++;
			}
		}
	}
	// Update is called once per frame
	void Update () {
	}

}
