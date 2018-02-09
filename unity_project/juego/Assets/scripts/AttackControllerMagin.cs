using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControllerMagin : MonoBehaviour
{

    enum AttackType
    {
        NONE,
        MAGIC
    };

    public GameObject magic;

    float timer;
    public float timerAtaque;
    public Animator animator;

    public float ATTACK_MAGIC_RECHARGE_TIME = 48.0f / 60.0f;

    public float ATTACK_MAGIC_DELAY = 24.0f / 60.0f;

    float timerAttack = -1;  // negative means not attacking
    AttackType attackType;

    // Use this for initialization
    void Start()
    {
        timerAtaque = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timerAtaque = timerAtaque + Time.deltaTime;

        float dt = Time.deltaTime;

        timer -= dt;

        if (attackType != AttackType.NONE)
        {
            timerAttack -= dt;

            if (timerAttack <= 0)
            {
                if (attackType == AttackType.MAGIC)
                {
                    Instantiate(magic, transform.position, transform.rotation);
                }

                animator.SetBool("shooted", false);
                animator.SetBool("attack", false);
                attackType = AttackType.NONE;
            }
        }

        else if (timer <= 0)
        {

            if (timerAtaque >= 3)
            {
                    this.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
                    timer = ATTACK_MAGIC_RECHARGE_TIME;
                    timerAttack = ATTACK_MAGIC_DELAY;   
                    attackType = AttackType.MAGIC;
                    animator.SetBool("shooted", true);
                    timerAtaque = 0;
            }
        }
    }
}
