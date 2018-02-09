using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalStoryController : MonoBehaviour {

    public Canvas canvas;

    public float[] timeReading = { 6.0f, 3.0f };
    public float timeBetweenTexts = 2.0f;
    public float fadeSpeed = 0.3f;

    int textIndex;

    string[] texts =
        {
            "This way Tiberius saved the Forest and his dear friend Magin.\n" +
            "Evil mosters and ghosts disappeared. Birds started singing and wild animals came back to the Forest."
            ,
            "But evil is always watching, waiting for the right moment."
        };

    float timer;
    float timerEnd;
    bool inBetween;

	// Use this for initialization
	void Start () {
        canvas.transform.Find("Text").GetComponent<Text>().text = texts[0];
        timer = -1;
        timerEnd = timeReading[0];
        textIndex = 0;
        FadeController fc = canvas.transform.Find("FadeImage").GetComponent<FadeController>();
        fc.state = FadeController.State.CLEARING;
        fc.fadingSpeed = fadeSpeed;
        inBetween = false;
    }
	
	// Update is called once per frame
	void Update () {

        float dt = Time.deltaTime;
        FadeController fc = canvas.transform.Find("FadeImage").GetComponent<FadeController>();

        if (textIndex >= texts.Length)
        {
            if(fc.state == FadeController.State.FADED)
            {
                // load credits
                SceneManager.LoadScene("credits");
            }
            timer += dt;
        }

        else
        {
            if (fc.state == FadeController.State.FADED)
            {
                if(!inBetween)
                {
                    inBetween = true;
                    timer = 0;
                    timerEnd = timeBetweenTexts;
                    canvas.transform.Find("Text").GetComponent<Text>().text = texts[textIndex];
                }
                else if(timer >= timerEnd)
                {
                    fc.state = FadeController.State.CLEARING;
                    timer = -1;
                    inBetween = false;
                }
                timer += dt;
            }
            else if(fc.state == FadeController.State.CLEARING)
            {
                // wait for clearing
            }
            else if(fc.state == FadeController.State.CLEAR)
            {
                if (timer < 0)
                {
                    timer = 0;
                    timerEnd = timeReading[textIndex];
                }
                else if (timer >= timerEnd)
                {
                    timer = 0;
                    textIndex++;
                    fc.state = FadeController.State.FADING;
                }
                else timer += dt;
            }
        }

    }
}
