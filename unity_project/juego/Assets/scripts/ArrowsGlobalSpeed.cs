using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsGlobalSpeed : MonoBehaviour {

    public float factor = 1.0f;

	void Start () {}
	void Update () {
        const float inc = 0.05f;
        if (Input.GetKeyDown("o")) factor -= inc;
        if (Input.GetKeyDown("p")) factor += inc;
    }
}
