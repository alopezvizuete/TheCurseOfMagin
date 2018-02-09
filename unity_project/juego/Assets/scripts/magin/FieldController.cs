using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour {
    Material field;
    bool tocado;
    bool repeat;

	// Use this for initialization
	void Start () {
	field = gameObject.GetComponent<Renderer>().material;
        tocado = false;
        repeat = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (tocado) {
            Color newColor = new Color(field.GetColor("_Color").r-0.05f, field.GetColor("_Color").g-0.05f, field.GetColor("_Color").b-0.05f);
            field.SetColor("_Color", newColor);

            if ((repeat == false) && (field.GetColor("_Color").r <= 0.4))
            {
                field.SetColor("_Color", new Color(1.0f,1.0f,1.0f,1.0f));
                repeat = true;
            }

            if (field.GetColor("_Color").r <= 0)
                Destroy(gameObject);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Magic")
        {
            tocado = true;

        }
    }
}
