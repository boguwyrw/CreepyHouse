using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTakeOpenObjectScript : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private GameObject playerEquipment;
    [SerializeField]
    private Button takeButton;
    [SerializeField]
    private Button openClosedButton;

    private RaycastHit castHit;
    private Ray ray;

    private string objectName = "";
    private bool canOpen = false;
    private float distanceToObject = 2.5f;

    private void Update()
    {
        ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        
        if (Physics.Raycast(ray, out castHit))
        {
            PlayerTakeObject();

            PlayerOpenObject();
        }
    }

    private void PlayerTakeObject()
    {
        if (castHit.collider.gameObject.layer == 11 && castHit.distance <= distanceToObject)
        {
            takeButton.gameObject.SetActive(true);
        }
        else
        {
            takeButton.gameObject.SetActive(false);
        }
    }

    private void PlayerOpenObject()
    {
        objectName = castHit.collider.name;
        if (castHit.collider.gameObject.layer == 12 && castHit.distance <= distanceToObject)
        {
            openClosedButton.gameObject.SetActive(true);
        }
        else
        {
            openClosedButton.gameObject.SetActive(false);
            canOpen = false;
        }
    }

    public string GetObjectName()
    {
        return objectName;
    }

    public bool GetCanOpen()
    {
        return canOpen;
    }

    public void TakeObject()
    {
        castHit.transform.parent = playerEquipment.transform;
        castHit.collider.gameObject.SetActive(false);
        castHit.transform.position = playerEquipment.transform.position;
    }

    public void OpenClosedObject()
    {
        canOpen = true;
    }
}
