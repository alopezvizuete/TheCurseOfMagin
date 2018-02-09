using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour {

    public Color fadeColour = new Color(0f, 0f, 0f, 1f);

    public enum State
    {
        CLEAR,
        FADED,
        CLEARING,
        FADING
    };
    public State state;
    public float fadingPercent = 0;
    public float fadingSpeed = 0.5f;

    private Image fadeImage;

    // Use this for initialization
    void Start () {
        fadeImage = GetComponent<Image>();

        switch (state)
        {
            case State.CLEAR:
            case State.FADING:
                fadingPercent = 0;
                break;
            case State.FADED:
            case State.CLEARING:
                fadingPercent = 1;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;

        switch (state)
        {
            case State.CLEAR:
                fadingPercent = 0;
                break;
            case State.FADED:
                fadingPercent = 1;
                break;
            case State.CLEARING:
                fadingPercent -= fadingSpeed * dt;
                if (fadingPercent <= 0)
                {
                    fadingPercent = 0;
                    state = State.CLEAR;
                }
                break;
            case State.FADING:
                fadingPercent += fadingSpeed * dt;
                if (fadingPercent >= 1)
                {
                    fadingPercent = 1;
                    state = State.FADED;
                }
                break;
        }

        Color colorClear = fadeColour; colorClear.a = 0;
        Color colorFaded = fadeColour; colorFaded.a = 1;
        fadeImage.color = Color.Lerp(colorClear, colorFaded, fadingPercent);

    }

}
