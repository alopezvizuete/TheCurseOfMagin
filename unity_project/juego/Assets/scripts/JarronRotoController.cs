using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarronRotoController : MonoBehaviour {
    float timer = 0;
    public int tiempo = 4;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= tiempo) {
            Destroy(gameObject);
        }
    }
}
