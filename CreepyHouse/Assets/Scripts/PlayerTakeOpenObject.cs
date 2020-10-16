using UnityEngine;
using UnityEngine.UI;

public class PlayerTakeOpenObject : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera = null;
    [SerializeField]
    private GameObject playerEquipment = null;
    [SerializeField]
    private Button takeButton = null;
    [SerializeField]
    private Button openClosedButton = null;

    private RaycastHit castHit;
    private Ray ray;

    private string objectName = "";
    private bool canOpen = false;
    private float distanceToObject = 3.0f;
    private int forTakeNumber = 11;
    private int interactiveNumber = 12;

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
        if (castHit.collider.gameObject.layer == forTakeNumber && castHit.distance <= distanceToObject)
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
        if (castHit.collider.gameObject.layer == interactiveNumber && castHit.distance <= distanceToObject)
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
