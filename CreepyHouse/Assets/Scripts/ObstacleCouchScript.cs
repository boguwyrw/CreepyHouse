using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleCouchScript : MonoBehaviour
{
    [SerializeField]
    private Button jumpButton;
    [SerializeField]
    private Button moveButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            jumpButton.gameObject.SetActive(true);
            moveButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            jumpButton.gameObject.SetActive(false);
            moveButton.gameObject.SetActive(false);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
