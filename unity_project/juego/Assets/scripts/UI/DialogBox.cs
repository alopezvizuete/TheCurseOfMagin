using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour {

    public enum State
    {
        CLOSED,
        OPENED,
        OPENING,
        CLOSING
    }

    public float MIN_SCALE = 0.1f;
    public float MAX_SCALE = 1.0f;
    public State state = State.CLOSED;
    public float speed = 1.0f;

    public float openedPercent = 0;

    public float showTime = -1;     // non positive numbers means close with left-click
    float timer = 0;

    public string text = "deafult text";
    public Sprite image;

    Image img;
    Text txt;
    RectTransform frame;
    State prevState = State.CLOSED;

    // Use this for initialization
    void Start () {
        frame = transform.Find("frame").GetComponent<RectTransform>();
        img = transform.Find("frame/Image").GetComponent<Image>();
        txt = transform.Find("frame/Text").GetComponent<Text>();
        GetComponent<Canvas>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        prevState = state;

        GetComponent<Canvas>().enabled = true;

        img.sprite = image;
        txt.text = text;

        float inc = speed;
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
            if(prevState != state) timer = 0;

            if(showTime > 0)
            {
                if(timer >= showTime)
                {
                    state = State.CLOSING;
                }
                timer += dt;
            }

            openedPercent = 1;
        }

        GetComponent<CanvasGroup>().alpha = Mathf.Sqrt(openedPercent);

        float scale = TJ.linearInterp(0, 1, openedPercent, MIN_SCALE, MAX_SCALE);
        frame.localScale = new Vector3(scale, scale, scale);

    }
}
