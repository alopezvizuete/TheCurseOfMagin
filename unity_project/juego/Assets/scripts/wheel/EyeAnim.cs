using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAnim : MonoBehaviour {

    public enum State
    {
        NONE,
        START,
        BLINKING,
        FADING,
        END
    }

    public State state = State.NONE;
    public Texture2D[] textures;
    public float[] frameTimes;
    public int[] frameIndices;
    public float fadeTime = 3;
    public float moveSpeed = 0.3f;

    int curFrame = -1;
    float t;
    int numFrames;


	// Use this for initialization
	void Start () {
        numFrames = frameTimes.GetLength(0);
	}
	
	// Update is called once per frame
	void Update () {
		
        if(state == State.START)
        {
            state++;
        }
        else if(state == State.BLINKING)
        {
            blinking();
        }
        else if(state == State.FADING)
        {
            fading();
        }

	}

    void blinking()
    {
        float dt = Time.deltaTime;
        t += dt;

        if(curFrame < 0)
        {
            curFrame = 0;
            t = 0;
            GetComponent<Renderer>().material.mainTexture = textures[frameIndices[curFrame]];
        }
        else if(curFrame >= numFrames)
        {
            state = State.FADING;
            curFrame = -1;
            t = 0;
        }
        else
        {
            float T = frameTimes[curFrame];
            bool changedOfFrame = false;
            while(t >= T)
            {
                curFrame++;
                t -= T;
                if (curFrame >= numFrames) return;
                T = frameTimes[curFrame];
                changedOfFrame = true;
            }
            if(changedOfFrame && curFrame < numFrames)
            {
                GetComponent<Renderer>().material.mainTexture = textures[frameIndices[curFrame]];
            }
        }
    }

    void fading()
    {
        float dt = Time.deltaTime;
        t += dt;

        float fadePercent = t / fadeTime;

        if(fadePercent >= 1)
        {
            state = State.END;
            GetComponent<Renderer>().enabled = false;
        }

        // fading
        Color color = new Color(0.8f, 1, 1, 0);
        color.a = (1 - fadePercent);
        GetComponent<Renderer>().material.SetColor("_Color", color);
        //GetComponent<Renderer>().material.color = color;

        // moving
        Vector3 fwd = new Vector3(0, 0, 1);
        transform.Translate(fwd * moveSpeed * dt);
    }
}
