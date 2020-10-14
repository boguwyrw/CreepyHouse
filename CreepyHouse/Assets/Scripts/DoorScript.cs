using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour, IEquipmentHolderScript
{
    [SerializeField]
    private Button wreckingBarButton;
    [SerializeField]
    private Button skeletonKeyButton;
    [SerializeField]
    private GameObject playerEquipment;
    [SerializeField]
    private GameObject player;

    private float doorWidth = 0.0f;
    private Vector3 rotationVector;
    private float doorOpeningSpeed = 100.0f;
    private DoorsScript doorsScript;
    private bool openingDoor = false;

    private string nameOfPointedObject = "";
    private PlayerTakeOpenObjectScript playerTakeOpen;

    private Dictionary<string, Button> interactableDictionary = new Dictionary<string, Button>();
    readonly string wreckingBarItemName = "WreckingBar";
    readonly string skeletonKeyItemName = "SkeletonKey";

    private void Start()
    {
        doorWidth = gameObject.GetComponent<Renderer>().bounds.size.x;
        rotationVector = new Vector3(transform.position.x + doorWidth / 2, transform.position.y, transform.position.z);

        doorsScript = transform.parent.gameObject.GetComponent<DoorsScript>();
        playerTakeOpen = player.GetComponent<PlayerTakeOpenObjectScript>();

        interactableDictionary.Add(wreckingBarItemName, wreckingBarButton);
        interactableDictionary.Add(skeletonKeyItemName, skeletonKeyButton);
    }

    private void Update()
    {
        openingDoor = doorsScript.GetCanOpenDoor();
        nameOfPointedObject = playerTakeOpen.GetObjectName();

        if (openingDoor && gameObject.name.Equals(nameOfPointedObject))
        {
            OpeningDoor();
        }
    }

    private void OpeningDoor()
    {
        transform.RotateAround(rotationVector, Vector3.up, doorOpeningSpeed * Time.deltaTime);
        if (transform.localEulerAngles.y >= 30)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        if (transform.localEulerAngles.y >= 90)
        {
            doorOpeningSpeed = 0.0f;
        }
    }

    private void TurnOffButtons()
    {
        wreckingBarButton.gameObject.SetActive(false);
        skeletonKeyButton.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            wreckingBarButton.gameObject.SetActive(true);
            skeletonKeyButton.gameObject.SetActive(true);
            EquipmentCheckerScript.CheckPlayerEquipment(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            TurnOffButtons();
        }
    }

    public GameObject getEquipment()
    {
        return playerEquipment;
    }

    public Dictionary<string, Button> getInteractables()
    {
        return interactableDictionary;
    }
}
