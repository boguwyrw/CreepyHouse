using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomDoorScript : MonoBehaviour
{
    [SerializeField]
    private Button actionButton;
    [SerializeField]
    private Text actionInfoText;

    private float doorWidth = 0.0f;
    private Vector3 rotationVector;
    private float doorOpeningSpeed = 50.0f;
    private bool madeAction = false;
    private bool doorAreOpen = false;

    private void Start()
    {
        doorWidth = gameObject.GetComponent<Renderer>().bounds.size.x;
        rotationVector = new Vector3(transform.position.x + doorWidth/2, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if (!doorAreOpen && madeAction)
        {
            transform.RotateAround(rotationVector, Vector3.up, doorOpeningSpeed * Time.deltaTime);
            if (transform.localEulerAngles.y >= 90.0f)
            {
                madeAction = false;
                doorAreOpen = true;
            }
        }

        if (doorAreOpen && madeAction)
        {
            transform.RotateAround(rotationVector, Vector3.down, doorOpeningSpeed * Time.deltaTime);
            if (transform.localEulerAngles.y <= 1.0f)
            {
                madeAction = false;
                doorAreOpen = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            actionButton.gameObject.SetActive(true);
            if (!doorAreOpen)
            {
                actionInfoText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            actionButton.gameObject.SetActive(false);
            actionInfoText.gameObject.SetActive(false);
        }
    }
    
    public void ActionButton()
    {
        madeAction = true;
    }
}
