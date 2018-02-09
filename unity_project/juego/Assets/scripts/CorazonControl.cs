using UnityEngine;
using System.Collections;

public class CorazonControl : MonoBehaviour
{
    float variable = 0.0f;
    float variableRotation = 0.0f;
    void Update()
    {
        variable += 0.1f;
        transform.localPosition = new Vector3(transform.position.x, transform.position.y + (Mathf.Cos(variable) / 100), transform.position.z);
        transform.Rotate(1.0f, 0.0f, 0.0f);
    }
}