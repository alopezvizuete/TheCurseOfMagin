using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematic : MonoBehaviour {

    public bool loop = false;

    public Cinematic nextCinematic = null;

    public enum State
    {
        STOP,
        START,
        DOING
    }

    public State state;

    public enum CinematicStepType
    {
        WAIT,
        MOVE,
        TURN,
        ENABLE_INPUT,
        DISABLE_INPUT,
        SET_ANIMATION,
        SHOW_SCROLL,
        SHOW_DIALOG_BOX,
        FADE_IN,
        FADE_OUT,
        FADED,
        TELETRANSPORT,
        ENABLE_OBJECT,
        DISABLE_OBJECT,
        FILL_CAULDRON,
        TURN_ON_CAULDRON,
        MAKE_CAULDRON_GOOD,
        CHANGE_LEVEL,
        ENABLE_GAME_OBJECT,
        DISABLE_GAMEOBJECT,
        STOP_PARTICLE_SYSTEM,
        INSTANTIATE_OBJECT,
    };

    [System.Serializable]
    public class CinematicStep
    {
        public CinematicStepType type;
        public Transform obj;
        public float time;
        public float speed;     // will be used if time < 0
        public Transform target;
        public string text;
        public GameObject prefab;
        public Sprite img;
    };

    enum StepState
    {
        START,

        DOING,

        END
    }
    StepState curStepState = StepState.START;

    GameObject ui;
    Vector3 startPos;
    Quaternion startRot;
    Transform target;
    Quaternion targetRot;
    float targetTime;
    GameObject scroll;
    GameObject dialogBox;

    public CinematicStep[] steps;

    int curStep;
    float timer;

    // Use this for initialization
    void Start() {
        curStep = 0;
        ui = GameObject.Find("HUDCanvas");
    }

    // Update is called once per frame
    void Update() {

        float dt = Time.deltaTime;
        int numSteps = steps.Length;

        if (curStepState == StepState.END) advanceStep();

        if (state == State.START)
        {
            timer = 0;
            curStep = 0;
            state = State.DOING;
        }
        else if (state == State.STOP)
        {
            return;
        }

        CinematicStep step = steps[curStep];
        switch (step.type)
        {
            case CinematicStepType.WAIT:
                stepWait();
                break;
            case CinematicStepType.MOVE:
                stepMove();
                break;
            case CinematicStepType.TURN:
                stepTurn();
                break;
            case CinematicStepType.ENABLE_INPUT:
                enableInput(true);
                break;
            case CinematicStepType.DISABLE_INPUT:
                enableInput(false);
                break;
            case CinematicStepType.SET_ANIMATION:
                stepStartAnimation(step.text);
                break;
            case CinematicStepType.SHOW_SCROLL:
                stepShowScroll();
                break;
            case CinematicStepType.SHOW_DIALOG_BOX:
                stepShowDialogBox();
                break;
            case CinematicStepType.FADE_IN:
                stepFadeIn();
                break;
            case CinematicStepType.FADE_OUT:
                stepFadeOut();
                break;
            case CinematicStepType.FADED:
                stepFaded();
                break;
            case CinematicStepType.TELETRANSPORT:
                stepTeletransport();
                break;
            case CinematicStepType.ENABLE_OBJECT:
                stepEnableObject();
                break;
            case CinematicStepType.DISABLE_OBJECT:
                stepDisableObject();
                break;
            case CinematicStepType.FILL_CAULDRON:
                stepFillCauldron();
                break;
            case CinematicStepType.TURN_ON_CAULDRON:
                stepTurnOnCauldron();
                break;
            case CinematicStepType.MAKE_CAULDRON_GOOD:
                stepMakeCauldronGood();
                break;
            case CinematicStepType.CHANGE_LEVEL:
                stepChangeLevel();
                break;
            case CinematicStepType.ENABLE_GAME_OBJECT:
                stepEnableGameObject();
                break;
            case CinematicStepType.DISABLE_GAMEOBJECT:
                stepDisableGameObject();
                break;
            case CinematicStepType.STOP_PARTICLE_SYSTEM:
                stepStopParticleSystem();
                break;
            case CinematicStepType.INSTANTIATE_OBJECT:
                stepInstantiateObject();
                break;
        }

    }

    void advanceStep()
    {
        int numSteps = steps.Length;
        curStep = curStep + 1;
        if (!loop && curStep >= steps.Length)
        {
            if(nextCinematic != null)
            {
                nextCinematic.state = Cinematic.State.START;
            }
            state = State.STOP;
        }
        curStep = curStep % steps.Length;
        curStepState = StepState.START;
    }

    void stepWait()
    {
        if (curStepState == StepState.START) curStepState = StepState.DOING;
        float dt = Time.deltaTime;
        timer += dt;
        if (timer >= steps[curStep].time)
        {
            timer -= steps[curStep].time;
            curStepState = StepState.END;
        }
    }

    void stepMove()
    {
        if (curStepState == StepState.START)
        {
            startPos = steps[curStep].obj.transform.position;
            target = steps[curStep].target;
            if (steps[curStep].time >= 0) targetTime = steps[curStep].time;
            else
            {
                float dist = (startPos - target.position).magnitude;
                targetTime = dist / steps[curStep].speed;
            }
            timer = 0;
            curStepState = StepState.DOING;
        }
        else if (curStepState == StepState.DOING)
        {
            if (timer >= targetTime)
            {
                timer = targetTime;
                curStepState = StepState.END;
            }
            float dt = Time.deltaTime;
            float movePercent = 1;
            if(targetTime != 0) movePercent = timer / targetTime;
            Vector3 pos = Vector3.Lerp(startPos, target.position, movePercent);
            steps[curStep].obj.position = pos;
            timer += dt;
        }
    }

    void stepTurn()
    {

        if (curStepState == StepState.START)
        {
            startRot = steps[curStep].obj.transform.rotation;
            Vector3 startDir = startRot * new Vector3(0, 0, 1);
            Vector3 targetDir = steps[curStep].target.transform.position -
                                steps[curStep].obj.transform.position;
            targetRot = Quaternion.LookRotation(targetDir);

            if (steps[curStep].time >= 0) targetTime = steps[curStep].time;
            else
            {
                float dist = Vector3.Angle(startDir, targetDir);
                targetTime = dist / steps[curStep].speed;
            }
            timer = 0;
            curStepState = StepState.DOING;
        }
        else if (curStepState == StepState.DOING)
        {
            if (timer >= targetTime)
            {
                timer = targetTime;
                curStepState = StepState.END;
            }
            float dt = Time.deltaTime;
            float movePercent = timer / targetTime;
            Quaternion rot = Quaternion.Slerp(startRot, targetRot, movePercent);
            steps[curStep].obj.rotation = rot;
            timer += dt;
        }

    }

    void enableInput(bool yes)
    {
        GameObject tiberiusObj = GameObject.FindGameObjectWithTag("Player");
        CharacterControl characterCtrl =
            tiberiusObj.GetComponent<CharacterControl>();
        characterCtrl.enableInput(yes);
        curStepState = StepState.END;
    }

    void stepStartAnimation(string name)
    {
        Animator animator = steps[curStep].obj.gameObject.GetComponent<Animator>();
        switch (name)
        {
            case "attack":
                animator.SetBool("attack", true);
                break;
            case "shoot":
                animator.SetBool("shooted", true);
                break;
            case "walk":
                animator.SetFloat("speed", 1);
                break;
            case "idle":
            default:
                animator.SetFloat("speed", 0);
                break;
        }
        curStepState = StepState.END;
    }

    void stepShowScroll()
    {
        if (curStepState == StepState.START)
        {
            UnityEngine.Object prefab;
            if (steps[curStep].prefab == null)
            {
                prefab = Resources.Load("Assets/prefabs/UI/NoteDisplay");
            }
            else
            {
                prefab = steps[curStep].prefab;
            }
            scroll = (GameObject)Instantiate(prefab);
            scroll.transform.parent = ui.transform;
            scroll.transform.position = new Vector3(0, 0, 0);
            scroll.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            scroll.GetComponent<NoteDisplayController>().state = NoteDisplayController.State.OPENING;
            curStepState = StepState.DOING;
        }
        else if (curStepState == StepState.DOING)
        {
            NoteDisplayController.State noteState =
                scroll.GetComponent<NoteDisplayController>().state;
            if (noteState == NoteDisplayController.State.OPENED
                && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
            {
                scroll.GetComponent<NoteDisplayController>().state =
                    NoteDisplayController.State.CLOSING;
            }
            else if (noteState == NoteDisplayController.State.CLOSED)
            {
                curStepState = StepState.END;
            }
        }
    }

    void stepShowDialogBox()
    {
        if (curStepState == StepState.START)
        {
            UnityEngine.Object prefab;
            if (steps[curStep].prefab == null)
            {
                prefab = Resources.Load("Assets/prefabs/UI/DialogBox");
            }
            else
            {
                prefab = steps[curStep].prefab;
            }
            dialogBox = (GameObject)Instantiate(prefab);
            dialogBox.GetComponent<DialogBox>().state = DialogBox.State.OPENING;
            dialogBox.GetComponent<DialogBox>().text = steps[curStep].text;
            dialogBox.GetComponent<DialogBox>().image = steps[curStep].img;
            dialogBox.GetComponent<DialogBox>().showTime = steps[curStep].time;
            curStepState = StepState.DOING;
        }
        else if (curStepState == StepState.DOING)
        {
            DialogBox.State dboxState = dialogBox.GetComponent<DialogBox>().state;
            if (dboxState == DialogBox.State.OPENED
                && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                && dialogBox.GetComponent<DialogBox>().showTime <= 0)
            {
                dialogBox.GetComponent<DialogBox>().state = DialogBox.State.CLOSING;
            }
            if (dboxState == DialogBox.State.CLOSED)
            {
                curStepState = StepState.END;
            }
        }
    }

    void stepFadeIn()
    {
        FadeController fadeController =
            GameObject.Find("HUDCanvas/FadeImage").GetComponent<FadeController>();
        float speed = steps[curStep].speed;
        fadeController.fadingPercent = 1;
        if (speed > 0.00001)
        {
            fadeController.fadingSpeed = speed;
        }
        else
        {
            fadeController.fadingSpeed = 0.5f;
        }

        fadeController.state = FadeController.State.CLEARING;

        curStepState = StepState.END;
    }

    void stepFadeOut()
    {
        FadeController fadeController =
            GameObject.Find("HUDCanvas/FadeImage").GetComponent<FadeController>();
        float speed = steps[curStep].speed;
        fadeController.fadingPercent = 0;
        if (speed > 0.00001)
        {
            fadeController.fadingSpeed = speed;
        }
        else
        {
            fadeController.fadingSpeed = 0.5f;
        }

        fadeController.state = FadeController.State.FADING;

        curStepState = StepState.END;
    }

    void stepTeletransport()
    {
        Transform target = steps[curStep].target;

        steps[curStep].obj.position = target.position;

        curStepState = StepState.END;
    }

    void stepEnableObject()
    {
        steps[curStep].obj.gameObject.SetActive(true);
        curStepState = StepState.END;
    }

    void stepDisableObject()
    {
        steps[curStep].obj.gameObject.SetActive(false);
        curStepState = StepState.END;
    }

    void stepTurnOnCauldron()
    {

        Cauldron cauldron =
            steps[curStep].obj.gameObject.GetComponent<Cauldron>();
        cauldron.enableFire = true;
        cauldron.enableSmoke = true;

        curStepState = StepState.END;
    }


    void stepFillCauldron()
    {
        Cauldron cauldron =
            steps[curStep].obj.gameObject.GetComponent<Cauldron>();
        cauldron.enableLiquid = true;

        curStepState = StepState.END;
    }

    void stepMakeCauldronGood()
    {
        Cauldron cauldron =
            steps[curStep].obj.gameObject.GetComponent<Cauldron>();
        cauldron.enableGood = true;

        curStepState = StepState.END;
    }

    void stepChangeLevel()
    {
        FadeController fadeController =
            GameObject.Find("HUDCanvas/FadeImage").GetComponent<FadeController>();

        if(curStepState == StepState.START)
        {
            float time = steps[curStep].time;
            float speed = steps[curStep].speed;
            if(time > 0)
            {
                speed = 1 / time;
            }
            else if(speed <= 0)
            {
                speed = -1;
            }

            if(speed > 0)
            {
                fadeController.fadingSpeed = speed;
                fadeController.state = FadeController.State.FADING;

                curStepState = StepState.DOING;
            }
            else
            {
                SceneManager.LoadScene(steps[curStep].text);
                curStepState = StepState.END;
            }
        }
        else if(curStepState == StepState.DOING)
        {
            if(fadeController.state == FadeController.State.FADED)
            {
                SceneManager.LoadScene(steps[curStep].text);
                curStepState = StepState.END;
            }
        }
    }

    void stepEnableGameObject()
    {
        GameObject go = steps[curStep].obj.gameObject;
        go.SetActive(true);

        curStepState = StepState.END;
    }

    void stepDisableGameObject()
    {
        GameObject go = steps[curStep].obj.gameObject;
        go.SetActive(false);

        curStepState = StepState.END;
    }

    void stepFaded()
    {
        FadeController fadeController =
            GameObject.Find("HUDCanvas/FadeImage").GetComponent<FadeController>();

        fadeController.state = FadeController.State.FADED;

        curStepState = StepState.END;
    }

    void stepStopParticleSystem()
    {
        ParticleSystem ps = steps[curStep].obj.GetComponent<ParticleSystem>();
        ps.Stop();

        curStepState = StepState.END;
    }

    void stepInstantiateObject()
    {
        Instantiate(steps[curStep].obj);

        curStepState = StepState.END;
    }

}