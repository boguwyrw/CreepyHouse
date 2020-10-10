using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private PlayerTakeOpenClosedObjectScript playerOpenClosed;

    private string objectName = "";
    private bool canOpenClosed = false;

    private void Start()
    {
        playerOpenClosed = player.GetComponent<PlayerTakeOpenClosedObjectScript>();
    }

    private void Update()
    {
        objectName = playerOpenClosed.GetObjectName();
        canOpenClosed = playerOpenClosed.GetCanOpenClosed();
        Debug.Log("transform.localPosition.z: " + transform.localPosition.z);
        if (gameObject.name.Equals(objectName) && canOpenClosed && transform.localPosition.z <= 1.4f)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }
}
