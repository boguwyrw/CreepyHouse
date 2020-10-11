using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour
{
    void Start()
    {
        Debug.Log(transform.parent.name);
    }
}
