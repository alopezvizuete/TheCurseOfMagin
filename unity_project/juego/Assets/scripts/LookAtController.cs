using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtController : MonoBehaviour {

	// Use this for initialization
	public static Vector3 currentPosition;


	void Start () {
	}

	// Update is called once per frame
	void Update () {
		
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		int mask = 1 << LayerMask.NameToLayer ("floor");
		if (Physics.Raycast(ray, out hit, 10000, mask)) {
			// Do something with the object that was hit by the raycast.
				transform.position = hit.point;
				currentPosition = transform.position;
		}
	}
}