using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarronController : MonoBehaviour {

    public GameObject Llave;
    public GameObject Enemy;
    public GameObject Corazon3D;
    public GameObject JarronRoto;

    private Vector3 down;
    private Quaternion rot;
    private Vector3 upCor;
    public bool CheckLlave;
    public bool CheckEnemy;
    public bool CheckCorazon;

    public GameObject sfxBreakPrefav;

    void Start () {
        down = new Vector3(0.0f,0.2f,0.0f);
        upCor = new Vector3(0.0f, 0.5f, 0.0f);
        rot = new Quaternion(0.0f,0.0f,0.0f,0.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Magic") || (other.tag == "Player") || (other.tag == "NormalAttack"))
        {
            if (CheckLlave == true)
            {
                GameObject ObjLlave = Instantiate(Llave, transform.position - down, rot);
            }
            if (CheckEnemy == true)
            {
                GameObject ObjEnemy = Instantiate(Enemy, transform.position, rot);
            }
            if (CheckCorazon == true)
            {
                GameObject ObjEnemy = Instantiate(Corazon3D, transform.position+upCor, Corazon3D.transform.rotation);
            }
            Destroy(gameObject);
            GameObject JarronRot = Instantiate(JarronRoto, transform.position - down, rot);
            Instantiate(sfxBreakPrefav);
        }
    }
}
