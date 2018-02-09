using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour {

    public Canvas canvas;
    public float timeBetween = 1.0f;

    private int state;

    float timer;
    float endTimer;

    bool waitFade, waitClear;

    // Use this for initialization
    void Start () {
        timer = 0;
        waitFade = waitClear = false;
        FadeController fc = canvas.transform.Find("FadeImage").GetComponent<FadeController>();
        fc.state = FadeController.State.FADED;
        changeState(0);
    }
	
	// Update is called once per frame
	void Update () {

        float dt = Time.deltaTime;
        FadeController fc = canvas.transform.Find("FadeImage").GetComponent<FadeController>();

        
        if (waitClear)
        {
            if (fc.state == FadeController.State.CLEAR)
            {
                waitClear = false;
            }
        }
        else if (timer < endTimer)
        {
            timer += dt;
            if(timer >= endTimer)
            {
                fc.state = FadeController.State.FADING;
            }
        }
        else if (waitFade)
        {

            if (fc.state == FadeController.State.FADED)
            {
                waitFade = false;
            }
        }
        else
        {
            changeState(state + 1);
            if(waitFade)
            {
                fc.state = FadeController.State.CLEARING;
            }
            timer = 0;
        }

    }

    void changeState(int newState)
    {
        state = newState;
        if(state == 0)
        {
            waitClear = waitFade = true;
            canvas.transform.Find("Text").GetComponent<Text>().text = "CREDITS";
            canvas.transform.Find("Text").GetComponent<Text>().fontSize = 50;
            endTimer = 1.5f;
        }
        else if(state == 1)
        {
            waitClear = waitFade = true;
            canvas.transform.Find("Text").GetComponent<Text>().text = "This game has been made by a team of students of the IGJRV Master's degree at URJC";
            canvas.transform.Find("Text").GetComponent<Text>().fontSize = 36;
            endTimer = 3.0f;
        }
        else if(state == 2)
        {
            waitClear = waitFade = true;
            canvas.transform.Find("Text").GetComponent<Text>().text = "Alberto Cadahía Subiñas\n\nAlejandro Juan Pérez\n\nAlejandro López Vizuete\n\nClaudia Ochagavías Recio";
            canvas.transform.Find("Text").GetComponent<Text>().fontSize = 32;
            endTimer = 4.0f;
        }
        else if (state == 3)
        {
            waitClear = waitFade = true;
            canvas.transform.Find("Text").GetComponent<Text>().text = "Special thanks to...";
            canvas.transform.Find("Text").GetComponent<Text>().fontSize = 40;
            endTimer = 1.5f;
        }
        else if (state == 4)
        {
            waitClear = waitFade = true;
            canvas.transform.Find("Text").GetComponent<Text>().text = "Our teacher:\nMarcos García Lorenzo\n\nOur classmates";
            canvas.transform.Find("Text").GetComponent<Text>().fontSize = 40;
            endTimer = 2.5f;
        }
        else if (state == 5)
        {
            waitClear = waitFade = true;
            canvas.transform.Find("Text").GetComponent<Text>().text = "Thanks for playing this game!\nWe hope you enjoyed it!";
            canvas.transform.Find("Text").GetComponent<Text>().fontSize = 36;
            endTimer = 5.0f;
        }
        else if (state == 6)
        {
            SceneManager.LoadScene("MainMenu");
        }

        FadeController fc = canvas.transform.Find("FadeImage").GetComponent<FadeController>();
        if (waitClear)
        {
            fc.state = FadeController.State.CLEARING;
        }
    }

}
