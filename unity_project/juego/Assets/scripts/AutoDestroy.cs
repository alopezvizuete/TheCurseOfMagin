using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {

    public float timeToDestroy = 8.0f;

    float timer;

	// Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        if(timer >= timeToDestroy)
        {
            Destroy(this);
        }
        timer += dt;
	}
}
