using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaginMovement : MonoBehaviour {

    public Transform[] points;
    public float[] stopTime;
    public float normalSpeed = 2.5f;
    public float rampageSpeed = 6.0f;

    float speed;

    int curPoint;
    float t;
    bool moving;
    float timeToDest;

	// Use this for initialization
	void Start () {

        if(points.Length != stopTime.Length)
        {
            Debug.Log("Error length of points and stopTime should be equal!");
        }
        else
        {
            speed = normalSpeed;
            moving = false;
            t = 0;
            curPoint = 0;
            float y = transform.position.y;
            int n = points.Length;
            for (int i=0; i<n; i++)
            {
                Vector3 pos = points[i].transform.position;
                pos.y = y;
                points[i].transform.position = pos;
            }
            transform.position = points[curPoint].transform.position;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (isForceFieldActive()) speed = normalSpeed;
        else                      speed = rampageSpeed;

        int n = points.Length;
        if (n < 2) return;
        float dt = Time.deltaTime;

        t += dt;

        if(!moving)
        {
            if(t >= stopTime[curPoint])
            {
                t -= stopTime[curPoint];
                moving = true;
                int nextPoint = (curPoint + 1) % n;
                Vector3 curPos = points[curPoint].transform.position;
                Vector3 nextPos = points[nextPoint].transform.position;
                float dist = (nextPos - curPos).magnitude;
                timeToDest = dist / speed;
            }
        }

        else
        {

            if(t >= timeToDest)
            {
                t -= timeToDest;
                moving = false;
                curPoint = (curPoint + 1) % n;
            }
            else
            {
                float percent = t / timeToDest;     // percentage of distance travelled to the next point
                int nextPoint = (curPoint + 1) % n;
                Vector3 prevPos = points[curPoint].transform.position;
                Vector3 nextPos = points[nextPoint].transform.position;
                Vector3 curPos = Vector3.Slerp(prevPos, nextPos, percent);
                transform.position = curPos;
            }

        }

	}

    bool isForceFieldActive()
    {
        Transform forceFieldCenter = GameObject.Find("ForceFieldCenter").transform;
        return forceFieldCenter.childCount > 0;
    }
}
