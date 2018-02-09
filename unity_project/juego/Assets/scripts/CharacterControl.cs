using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {

	public float playerSpeed = 5.0f;
	public float rotationSpeed = 5.0f;
	private Vector3 lookAt;
	private CharacterController controller;
    private Animator animator;

	public static bool movementEnabled;
    public static bool inputEnabled;

	// Use this for initialization
	void Start () {
        movementEnabled = true;
        inputEnabled = true;
		controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!inputEnabled) return;

        float dt = Time.deltaTime;
		lookAt = LookAtController.currentPosition;
		Vector3 lookDir = lookAt - transform.position;
		Quaternion targetRotation = Quaternion.LookRotation (lookDir);
		targetRotation.x = 0.0f;
		targetRotation.z = 0.0f;
		Quaternion rot= controller.transform.rotation;
		controller.transform.rotation = Quaternion.Lerp(rot, targetRotation, rotationSpeed * dt);
		//controller.transform.rotation = targetRotation;
		if (movementEnabled) {
			Vector3 moveVec = Vector3.zero;
			if (controller.isGrounded) {
				float x = Input.GetAxis ("Horizontal") * playerSpeed * Time.deltaTime;
				float z = Input.GetAxis ("Vertical") * playerSpeed * Time.deltaTime;
				moveVec = new Vector3 (x, 0, z);
				animator.SetFloat ("speed", Mathf.Abs (x) + Mathf.Abs (z));
			}
			moveVec += Physics.gravity * Time.deltaTime;

			controller.Move (moveVec);
		} else {
			animator.SetFloat ("speed", 0);
		}
	}

    public void enableMovement(bool yes)
    {
        movementEnabled = yes;
    }

    public void enableInput(bool yes)
    {
        inputEnabled = yes;
    }

    public bool isInputEnabled()
    {
        return inputEnabled;
    }
}