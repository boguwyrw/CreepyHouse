using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private PlayerTakeOpenObjectScript playerOpenClosed;

    private string objectName = "";
    private bool canOpenClosed = false;

    private void Start()
    {
        playerOpenClosed = player.GetComponent<PlayerTakeOpenObjectScript>();
    }

    private void Update()
    {
        objectName = playerOpenClosed.GetObjectName();
        canOpenClosed = playerOpenClosed.GetCanOpenClosed();

        if (gameObject.name.Equals(objectName) && canOpenClosed && transform.localPosition.z <= 1.4f)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }
}
