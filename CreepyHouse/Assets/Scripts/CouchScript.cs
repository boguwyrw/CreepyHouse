using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouchScript : MonoBehaviour
{
    [SerializeField]
    private GameObject obstacleCouchSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            obstacleCouchSystem.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            obstacleCouchSystem.SetActive(false);
        }
    }
}
