using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, IEquipmentHolder
{
    [SerializeField]
    private Button wreckingBarButton = null;
    [SerializeField]
    private Button skeletonKeyButton = null;
    [SerializeField]
    private GameObject playerEquipment = null;
    [SerializeField]
    private GameObject player = null;

    private float doorWidth = 0.0f;
    private Vector3 rotationVector;
    private float doorOpeningSpeed = 100.0f;
    private Doors doors;
    private bool openingDoor = false;
    private int playerNumber = 9;

    private string nameOfPointedObject = "";
    private PlayerTakeOpenObject playerTakeOpen;

    private Dictionary<string, Button> interactableDictionary = new Dictionary<string, Button>();

    private void Start()
    {
        doorWidth = gameObject.GetComponent<Renderer>().bounds.size.x;
        rotationVector = new Vector3(transform.position.x + doorWidth / 2, transform.position.y, transform.position.z);

        doors = transform.parent.gameObject.GetComponent<Doors>();
        playerTakeOpen = player.GetComponent<PlayerTakeOpenObject>();

        interactableDictionary.Add(ItemsNames.wreckingBarItemName, wreckingBarButton);
        interactableDictionary.Add(ItemsNames.skeletonKeyItemName, skeletonKeyButton);
    }

    private void Update()
    {
        openingDoor = doors.GetCanOpenDoor();
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
        if (other.gameObject.layer == playerNumber)
        {
            wreckingBarButton.gameObject.SetActive(true);
            skeletonKeyButton.gameObject.SetActive(true);
            EquipmentChecker.CheckPlayerEquipment(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerNumber)
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
