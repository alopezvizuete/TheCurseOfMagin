using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {

    enum AttackType
    {
        NONE,
        NORMAL,
        MAGIC
    };

    public GameObject magic;
    public GameObject attackCollider;
	public Vector3 shootDir;

    public GameObject sfxFire;

	PlayerUI playerUI;
	float timer;
	public int costMagic = 	10;
    public Animator animator;

    public float ATTACK_NORMAL_RECHARGE_TIME = 48.0f / 60.0f;
    public float ATTACK_MAGIC_RECHARGE_TIME = 48.0f / 60.0f;

    public float ATTACK_NORMAL_DELAY = 24.0f / 60.0f;
    public float ATTACK_MAGIC_DELAY = 24.0f / 60.0f;

    float timerAttack = -1;  // negative means not attacking
    AttackType attackType;

    CharacterControl player;

    int resetAttackCinematics = 10;
    int resetShootCinematics = 10;

    // Use this for initialization
    void Start () {
		playerUI = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerUI>();
        player = transform.parent.gameObject.GetComponent<CharacterControl>();
    }
	
	// Update is called once per frame
	void Update () {

        float dt = Time.deltaTime;

        timer -= dt;

        if(!player.isInputEnabled())
        {
            if(resetAttackCinematics < 0 && animator.GetBool("attack"))
            {
                resetAttackCinematics = 10;
            }
            else if(resetAttackCinematics >= 0)
            {
                if (resetAttackCinematics == 0)
                {
                    animator.SetBool("attack", false); // necesacy for cinematics
                }
                resetAttackCinematics--;
            }

            if (resetShootCinematics < 0 && animator.GetBool("shooted"))
            {
                resetShootCinematics = 10;
            }
            else if (resetShootCinematics >= 0)
            {
                if (resetShootCinematics == 0)
                {
                    animator.SetBool("shooted", false); // necesacy for cinematics
                }
                resetShootCinematics--;
            }
        }

        if (attackType != AttackType.NONE)
        {
            timerAttack -= dt;

            if(timerAttack <= 0)
            {
                if(attackType == AttackType.NORMAL)
                {
                    GameObject obj = Instantiate(attackCollider, transform.position, transform.rotation);
                }
                else if(attackType == AttackType.MAGIC)
                {
                    GameObject obj = Instantiate(magic, transform.position, transform.rotation);
                }

                animator.SetBool("shooted", false);
                animator.SetBool("attack", false);
                attackType = AttackType.NONE;
            }
        }

        else if(timer <= 0)
        {
            if (!player.isInputEnabled())
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                timer = ATTACK_NORMAL_RECHARGE_TIME;
                timerAttack = ATTACK_NORMAL_DELAY;
                attackType = AttackType.NORMAL;
                animator.SetBool("attack", true);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (playerUI.currentMagic < costMagic)
                {
                    // play beep sound
                }
                else
                {
					shootDir = LookAtController.currentPosition;
					animator.SetBool("shooted", true);
                    timer = ATTACK_MAGIC_RECHARGE_TIME;
                    timerAttack = ATTACK_MAGIC_DELAY;
                    playerUI.TakeMagic(costMagic);
                    attackType = AttackType.MAGIC;
                    Instantiate(sfxFire);
                }
            }
        }
	}

}
