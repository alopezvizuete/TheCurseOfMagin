using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	public float speed;
	public static bool inCastle=false;

	// Use this for initialization
	void Start () {
		offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!inCastle) {
			Vector3 moveCamera = new Vector3 (target.transform.position.x, target.transform.position.y, target.transform.position.z);
			transform.position = Vector3.Lerp (transform.position, moveCamera + offset, speed * Time.deltaTime);
			transform.LookAt (target.position);
		} 
		else {
			Vector3 moveCamera = new Vector3 (target.transform.position.x, target.transform.position.y, target.transform.position.z);
			transform.position = Vector3.Lerp (transform.position, moveCamera + offset, speed * Time.deltaTime);
			transform.LookAt (target.position);
			transform.position= Vector3.Lerp(transform.position,new Vector3 (transform.position.x,5.0f,-7.5f),speed*0.5f * Time.deltaTime);
			transform.rotation= Quaternion.Euler(45,20,0);
		}
	}
}
