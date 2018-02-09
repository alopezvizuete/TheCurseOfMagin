using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {

    Transform hinge;

    public enum State
    {
        CLOSED,
        OPENED,
        OPENING,
        CLOSING
    }

    public float SPEED = 45;    // degrees persecond
    float openedPercent;

    public GameObject sfxPrefab;


    public State state;
    State prevState;

	// Use this for initialization
	void Start () {
        hinge = transform.Find("mechanism/hinge");

        if(state==State.CLOSED || state==State.OPENING)
        {
            openedPercent = 0;
        }
        else
        {
            openedPercent = 1;
        }

        if(state == State.OPENING || state == State.CLOSING)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            //if(sceneName != "sample_scene")
            if(sfxPrefab != null)
            {
                Instantiate(sfxPrefab);
            }
        }

        prevState = state;
	}
	
	// Update is called once per frame
	void Update () {

        float inc = SPEED / 90;
        float dt = Time.deltaTime;

		if(state == State.OPENING)
        {
            if (prevState != state && sfxPrefab != null) Instantiate(sfxPrefab);

            openedPercent += inc * dt;
            if(openedPercent > 1)
            {
                openedPercent = 1;
                state = State.OPENED;
            }
        }
        else if(state == State.CLOSING)
        {
            if (prevState != state && sfxPrefab != null) Instantiate(sfxPrefab);

            openedPercent -= inc * dt;
            if (openedPercent < 0)
            {
                openedPercent = 0;
                state = State.CLOSED;
            }
        }
        else if(state == State.CLOSED)
        {
            openedPercent = 0;
        }
        else if (state == State.OPENED)
        {
            openedPercent = 1;
        }

        Quaternion closedRot = Quaternion.identity;
        Quaternion openedRot = Quaternion.AngleAxis(-90, new Vector3(0, 1, 0));
        hinge.localRotation = Quaternion.Lerp(closedRot, openedRot, openedPercent);

        prevState = state;
    }

    void open(float time)
    {
        state = State.OPENING;
        SPEED = 90 / time;
    }

}
