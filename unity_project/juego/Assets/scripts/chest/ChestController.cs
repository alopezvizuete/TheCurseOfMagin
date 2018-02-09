using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {

    Transform hinge;

    public enum State
    {
        CLOSED,
        OPENED,
        OPENING,
        CLOSING
    }

    public float SPEED = 45;    // degrees persecond
    public float MAX_OPEN_DEGREES = 200;
    float openedPercent;

    public State state;

    // Use this for initialization
    void Start () {
        hinge = transform.Find("hinge");

        if (state == State.CLOSED || state == State.OPENING)
        {
            openedPercent = 0;
        }
        else
        {
            openedPercent = 1;
        }
    }
	
	// Update is called once per frame
	void Update () {

        float inc = SPEED / MAX_OPEN_DEGREES;
        float dt = Time.deltaTime;

        if (state == State.OPENING)
        {
            openedPercent += inc * dt;
            if (openedPercent > 1)
            {
                openedPercent = 1;
                state = State.OPENED;
            }
        }
        else if (state == State.CLOSING)
        {
            openedPercent -= inc * dt;
            if (openedPercent < 0)
            {
                openedPercent = 0;
                state = State.CLOSED;
            }
        }
        else if (state == State.CLOSED)
        {
            openedPercent = 0;
        }
        else if (state == State.OPENED)
        {
            openedPercent = 1;
        }

        float angle = openedPercent * MAX_OPEN_DEGREES;
        hinge.localRotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
    }
}
