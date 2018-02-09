using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRoomController : MonoBehaviour {

    public enum State
    {
        IDDLE,
        START,
        SHINE,
        SHAKE,
        ROTATING
    }

    public State state;

    public int NUM_TURNS = 7;
    public float AVERAGE_DEGREES_PER_TURN = 180;
    public float VARIANCE_DEGREES = 0.5f;   // must be in (0, 1)
    public float SPEED = 100;               // average speed
    public float SPEED_INC = 2;             // faster each turn
    public float SHAKE_RAD = 0.1f;
    public float SHAKE_FREQ = 0.2f;
    public int SHAKE_TIMES = 12;

    int turn = -1;
    float curAngle = 0;
    float targetAngle = 0;
    float speed;

    GameObject eye;

    // SHAKE
    float t;
    int shakeCount;
    Vector3 oriPos;
    Vector3 prePos;
    Vector3 targetPos;

	// Use this for initialization
	void Start () {
        eye = GameObject.Find("eye");
        speed = SPEED;
        GameObject.FindWithTag("Player")
                .GetComponent<CharacterControl>()
                .enableInput(false);
    }
	
	// Update is called once per frame
	void Update () {

        if(state == State.START)
        {
            state = State.SHINE;
            eye.GetComponent<EyeAnim>().state = EyeAnim.State.START;
        }
        else if(state == State.SHINE)
        {
            shine();
        }
        else if(state == State.SHAKE)
        {
            shake();
        }
        else if(state == State.ROTATING)
        {
            rotating();
        }

	}

    void rotating()
    {
        float dt = Time.deltaTime;

        if(turn < 0)
        {
            turn = 0;
            targetAngle = generateRandomTarget();
            curAngle = 0;
        }
        else if(turn >= NUM_TURNS)
        {
            state = State.IDDLE;
            GameObject.FindWithTag("Player")
                .GetComponent<CharacterControl>()
                .enableInput(true);
            turn = -1;
        }
        else
        {
            Vector3 euler = transform.rotation.eulerAngles;

            float sign = targetAngle < 0 ? -1 : +1;
            float inc = sign * speed * dt;
            euler.y += inc;
            curAngle += inc;
            if(sign * curAngle >= sign * targetAngle)
            {
                // reached control point
                float surpassAmount = curAngle - targetAngle;
                euler.y -= surpassAmount;
                turn++;
                speed += SPEED_INC;
                curAngle = 0;
                targetAngle = generateRandomTarget();
            }

            transform.rotation = Quaternion.Euler(euler);
        }
        
    }

    void shine()
    {
        EyeAnim eyeAnim = eye.GetComponent<EyeAnim>();
        if(eyeAnim.state == EyeAnim.State.END)
        {
            state = State.SHAKE;
            shakeCount = -1;
        }
    }

    void shake()
    {
        float dt = Time.deltaTime;

        t += dt;
        float perc = t / SHAKE_FREQ;

        if(shakeCount < 0)
        {
            t = 0;
            shakeCount = 0;
            oriPos = transform.position;
            prePos = oriPos;
            float randAngle = Random.Range(0, 360);
            targetPos = new Vector3(Mathf.Sin(TJ.toRad(randAngle)), oriPos.y, Mathf.Cos(TJ.toRad(randAngle)));
            targetPos *= SHAKE_RAD;
        }
        else if(shakeCount == SHAKE_TIMES)
        {
            if (perc > 1)
            {
                perc = 1;
                shakeCount++;
            }
            Vector3 pos = TJ.linearInterp(prePos, targetPos, perc);
            transform.position = pos;
        }
        else if(shakeCount > SHAKE_TIMES)
        {
            state = State.ROTATING;
        }
        else
        {
            Vector3 pos;
            if (perc > 1)
            {
                perc = 1;
                shakeCount++;
                pos = targetPos;
                // get new target
                prePos = targetPos;
                float randAngle = Random.Range(0, 360);
                targetPos = new Vector3(Mathf.Sin(TJ.toRad(randAngle)), oriPos.y, Mathf.Cos(TJ.toRad(randAngle)));
                targetPos *= SHAKE_RAD;
                t = 0;
            }
            else
            {
                pos = TJ.linearInterp(prePos, targetPos, perc);
            }
            transform.position = pos;
        }

}

    float generateRandomTarget()
    {
        float res = AVERAGE_DEGREES_PER_TURN;
        float rnd = Random.Range(-VARIANCE_DEGREES, +VARIANCE_DEGREES);
        res += rnd * res;
        res = TJ.angleNorm180(res); // just in case, but I think can be removed
        return res;
    }
}

