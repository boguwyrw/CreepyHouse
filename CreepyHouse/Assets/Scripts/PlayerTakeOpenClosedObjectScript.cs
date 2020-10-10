using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTakeOpenClosedObjectScript : MonoBehaviour
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
    private bool canOpenClosed = false;
    private float distanceToObject = 1.75f;

    private void Update()
    {
        ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        
        if (Physics.Raycast(ray, out castHit))
        {
            PlayerTakeObject();

            PlayerOpenClosedObject();
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

    private void PlayerOpenClosedObject()
    {
        if (castHit.collider.gameObject.layer == 12 && castHit.distance <= distanceToObject)
        {
            objectName = castHit.collider.name;
            openClosedButton.gameObject.SetActive(true);
        }
        else
        {
            openClosedButton.gameObject.SetActive(false);
            canOpenClosed = false;
        }
    }

    public string GetObjectName()
    {
        return objectName;
    }

    public bool GetCanOpenClosed()
    {
        return canOpenClosed;
    }

    public void TakeObject()
    {
        castHit.transform.parent = playerEquipment.transform;
    }

    public void OpenClosedObject()
    {
        canOpenClosed = true;
    }
}
