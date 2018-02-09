using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestPickController : MonoBehaviour {

    public float MIN_TIME_YOU_HAVE_FOUND_INGREDIENT = 1.0f;
    public float MIN_TIME_SHOWING_INGREDIENT = 2.0f;
    public float MIN_TIME_YOU_HAVE_FOUND_NOTE = 1.0f;
    public float MIN_TIME_SHOWING_LETTER = 5.0f;
    public float MIN_TIME_SHOWING_OH_NO = 2.0f;

    public bool activated = false;
    public string text1 = "You have found the potion material!";
    public Sprite img1;
    public string text2 = "There is also a note...";
    public Sprite img2;
    public string text3 = "Oh, no! My old friend Magin is The Dark Wizard that has cursed the forest.";
    public Sprite img3;

    public GameObject boxDialogPrefav;
    public GameObject scrollPrefav;

    GameObject ui;

    public enum State
    {
        START,
        JUST_PICKED,
        YOU_HAVE_FOUND_INGREDIENT,
        SHOWING_INGREDIENT,
        YOU_HAVE_FOUND_NOTE,
        SHOWING_NOTE,
        OH_NO,
        FADING
    };

    public State state;

    float timer;
    State stateTimer;

    GameObject dialog;
    GameObject scroll;
    GameObject fadeImg;

    GameObject character;
    
    // Use this for initialization
    void Start () {
        ui = GameObject.Find("HUDCanvas");
        fadeImg = ui.transform.Find("FadeImage").gameObject;
        character = GameObject.FindGameObjectWithTag("Player");

        dialog = Instantiate(boxDialogPrefav);
        dialog.GetComponent<DialogBox>().state = DialogBox.State.CLOSED;

        timer = 0;
        stateTimer = State.START;
    }

	// Update is called once per frame
	void Update () {

        float dt = Time.deltaTime;
        timer += dt;
        
        if(state == State.START)
        {

        }

        else if(state == State.JUST_PICKED)
        {
            character.GetComponent<CharacterControl>().enableMovement(false);
            dialog.GetComponent<DialogBox>().text = text1;
            dialog.GetComponent<DialogBox>().image = img1;
            dialog.GetComponent<DialogBox>().state = DialogBox.State.OPENING;
            state++;
        }

        else if(state == State.YOU_HAVE_FOUND_INGREDIENT)
        {
            if(dialog.GetComponent<DialogBox>().state == DialogBox.State.OPENED &&
                endTimerState(MIN_TIME_YOU_HAVE_FOUND_INGREDIENT) &&
                (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
            {
                if (endTimerState(MIN_TIME_YOU_HAVE_FOUND_INGREDIENT))
                    dialog.GetComponent<DialogBox>().state = DialogBox.State.CLOSING;
            }
            else if(dialog.GetComponent<DialogBox>().state == DialogBox.State.CLOSED)
            {
                state++;
            }
        }

        else if(state == State.SHOWING_INGREDIENT)
        {
            // TODO?: NO
            state++;

            //dialog = Instantiate(boxDialogPrefav);
            dialog.GetComponent<DialogBox>().text = text2;
            dialog.GetComponent<DialogBox>().image = img2;
            dialog.GetComponent<DialogBox>().state = DialogBox.State.OPENING;
        }
        else if(state == State.YOU_HAVE_FOUND_NOTE)
        {
            if (dialog.GetComponent<DialogBox>().state == DialogBox.State.OPENED &&
                endTimerState(MIN_TIME_YOU_HAVE_FOUND_NOTE) &&
                (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
            {
                dialog.GetComponent<DialogBox>().state = DialogBox.State.CLOSING;
            }
            else if (dialog.GetComponent<DialogBox>().state == DialogBox.State.CLOSED)
            {
                scroll = Instantiate(scrollPrefav);
                scroll.transform.parent = ui.transform;
                scroll.transform.position = new Vector3(0, 0, 0);
                scroll.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                scroll.GetComponent<NoteDisplayController>().state = NoteDisplayController.State.OPENING;
                state++;
            }
        }
        else if(state == State.SHOWING_NOTE)
        {
            if (scroll.GetComponent<NoteDisplayController>().state == NoteDisplayController.State.OPENED &&
                endTimerState(MIN_TIME_SHOWING_LETTER) &&
                (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
            {
                scroll.GetComponent<NoteDisplayController>().state = NoteDisplayController.State.CLOSING;
            }
            else if (scroll.GetComponent<NoteDisplayController>().state == NoteDisplayController.State.CLOSED)
            {
                dialog.GetComponent<DialogBox>().text = text3;
                dialog.GetComponent<DialogBox>().image = img3;
                dialog.GetComponent<DialogBox>().state = DialogBox.State.OPENING;

                state++;
            }
        }
        else if (state == State.OH_NO)
        {
            if ((dialog.GetComponent<DialogBox>().state == DialogBox.State.OPENED) &&
                endTimerState(MIN_TIME_SHOWING_OH_NO) &&
                (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
            {
                dialog.GetComponent<DialogBox>().state = DialogBox.State.CLOSING;
            }
            else if (dialog.GetComponent<DialogBox>().state == DialogBox.State.CLOSED)
            {
                fadeImg.GetComponent<FadeController>().state = FadeController.State.FADING;

                state++;
            }
        }
        else if(state == State.FADING)
        {
            if(fadeImg.GetComponent<FadeController>().state == FadeController.State.FADED)
            {
                EstadoJuego estadoJuego =
                    GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
                estadoJuego.fistDungeonCompleted = true;
                // change level
                SceneManager.LoadScene("scenes/Tiberius_house");
            }
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if(activated && other.tag == "Player")
        {
            state = State.JUST_PICKED;
        }
    }

    bool endTimerState(float timerMax)
    {
        float dt = Time.deltaTime;

        if(stateTimer != state)
        {
            stateTimer = state;
            timer = 0;
            return false;
        }
        else
        {
            timer += dt;
            return timer >= timerMax;
        }
    }
}
