using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMechanismPattern : MonoBehaviour {

    public GameObject arrowType;
    public float arrowSpeed = 3;
    public float[] pattern;

    float t;
    int frame;

    // Use this for initialization
    void Start () {
        t = 0;
        frame = 0;
	}
	
	// Update is called once per frame
	void Update () {

        ArrowsGlobalSpeed speedFactorObject = GameObject.FindObjectOfType<ArrowsGlobalSpeed>();
        float factor = speedFactorObject ? speedFactorObject.factor : 1;
        int n = pattern.Length;

        if (n <= 0) return;

        float dt = Time.deltaTime * factor;

        t += dt;
        float frameTime = pattern[frame];
        if (t >= frameTime)
        {
            shootArrow();
            t -= frameTime;
            frame = (frame + 1) % n;
        }

	}

    public void shootArrow()
    {
        GameObject arrow = Instantiate(arrowType);
        arrow.transform.position = this.transform.position;
        arrow.transform.rotation = this.transform.rotation;
        arrow.GetComponent<ArrowController>().speed = arrowSpeed;
    }
}
