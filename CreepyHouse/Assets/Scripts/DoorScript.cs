using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private float doorWidth = 0.0f;
    private Vector3 rotationVector;

    void Start()
    {
        doorWidth = gameObject.GetComponent<BoxCollider>().size.x;
        rotationVector = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
    }

    void Update()
    {
        if (transform.localEulerAngles.y < 90.0f)
        {
            transform.RotateAround(rotationVector, Vector3.up, 20 * Time.deltaTime);
        }
    }
}
