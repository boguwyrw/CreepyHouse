using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private PlayerTakeOpenObjectScript playerTakeOpen;

    private string objectName = "";
    private bool canOpen = false;

    private void Start()
    {
        playerTakeOpen = player.GetComponent<PlayerTakeOpenObjectScript>();
    }

    private void Update()
    {
        objectName = playerTakeOpen.GetObjectName();
        canOpen = playerTakeOpen.GetCanOpen();

        if (gameObject.name.Equals(objectName) && canOpen && transform.localPosition.z <= 1.4f)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }
}
