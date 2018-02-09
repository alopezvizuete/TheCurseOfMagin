using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarronPreLoad : MonoBehaviour {

    public GameObject jarronRoto;

	// Use this for initialization
	void Start () {
        GameObject obj = GameObject.Instantiate(jarronRoto);
        if (obj == null) Debug.Log("bad");
        else
        {
            GameObject.Destroy(obj);
        }
    }
	
	// Update is called once per frame
	void Update () {
    }
}
