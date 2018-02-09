using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NoteDisplayController : MonoBehaviour {

    public enum State
    {
        CLOSED,
        OPENED,
        OPENING,
        CLOSING
    }

    public float MIN_SCALE = 0.1f;
    public float MAX_SCALE = 0.95f;
    public State state = State.CLOSED;
    public float speed = 1.0f;

    public float openedPercent;

    RectTransform rectTransform;
    Image image;
    int texWidth, texHeight;
    float imgAspectRatio;

    int screenWidth, screenHeight;

    // Use this for initialization
    void Start () {

        screenWidth = Screen.width;
        screenHeight = Screen.height;

        image = GetComponent<Image>();
        texWidth = image.sprite.texture.width;
        texHeight = image.sprite.texture.height;
        imgAspectRatio = (float)texWidth / texHeight;

        rectTransform = GetComponent<RectTransform>();

        if(state == State.CLOSED || state == State.OPENING)
        {
            openedPercent = 0.0f;
        }
        else
        {
            openedPercent = 1.0f;
        }

	}
	
	// Update is called once per frame
	void Update () {

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
            openedPercent = 1;
        }

        Color imgColor = image.color;
        imgColor.a = Mathf.Sqrt(openedPercent);
        image.color = imgColor;

        screenWidth = Screen.width;
        screenHeight = Screen.height;
        float screenAspectRatio = screenWidth / screenHeight;

        float imgMaxWidth = Mathf.Min(screenWidth, screenHeight * imgAspectRatio);
        float imgMaxHeight = Mathf.Min(screenHeight, screenWidth / imgAspectRatio);

        float scale = TJ.linearInterp(0, 1, openedPercent, MIN_SCALE, MAX_SCALE);
        //float scale = openedPercent;
        float width = scale * imgMaxWidth;
        float height = scale * imgMaxHeight;
        rectTransform.sizeDelta = new Vector2(width, height);

    }
}
