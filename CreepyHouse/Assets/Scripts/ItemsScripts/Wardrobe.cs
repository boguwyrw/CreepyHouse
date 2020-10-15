using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wardrobe : MonoBehaviour, IEquipmentHolder
{
    [SerializeField]
    private Button forceButton = null;
    [SerializeField]
    private Button useItemButton = null;
    [SerializeField]
    private Text wardrobeInfoText = null;
    [SerializeField]
    private GameObject playerEquipment = null;
    [SerializeField]
    private GameObject player = null;

    [SerializeField]
    private GameObject wardrobeLeftDoor = null;
    [SerializeField]
    private GameObject wardrobeRightDoor = null;
    [SerializeField]
    private Transform rotationAxisLeft = null;
    [SerializeField]
    private Transform rotationAxisRight = null;

    private float doorOpeningSpeed = 50.0f;
    private Vector3 leftDoorRotationVector;
    private Vector3 rightDoorRotationVector;
    private bool canOpenDoors = false;
    private int playerNumber = 9;

    private int healthDamage = 2;
    private int minimumRequiredPoints = 8;

    private int playerStrength = 0;
    private int playerDexterity = 0;

    private string positiveInfo = "Congratulations, you opened wardrobe without problems";
    private string negativeInfo = "You hurt yourself by loose handle";

    private Dictionary<string, Button> interactableDictionary = new Dictionary<string, Button>();

    private void Start()
    {
        playerStrength = player.GetComponent<Player>().GetPlayerStrength();
        playerDexterity = player.GetComponent<Player>().GetPlayerDexterity();

        leftDoorRotationVector = new Vector3(rotationAxisLeft.position.x, rotationAxisLeft.position.y, rotationAxisLeft.position.z);
        rightDoorRotationVector = new Vector3(rotationAxisRight.position.x, rotationAxisRight.position.y, rotationAxisRight.position.z);

        useItemButton.interactable = false;

        interactableDictionary.Add(ItemsNames.screwdriverItemName, useItemButton);
    }

    private void Update()
    {
        if (canOpenDoors)
        {
            OpeningLeftDoor();
            OpeningRightDoor();
        }
    }

    private void PlayerUseForce()
    {
        if (playerStrength < minimumRequiredPoints)
        {
            PlayerHealthDamage();
            StartCoroutine(DisplayNegativeInfo());
        }
        else
        {
            StartCoroutine(DisplayPositiveInfo());
        }
    }

    private void PlayerUseItem()
    {
        if (playerDexterity < minimumRequiredPoints)
        {
            PlayerHealthDamage();
            StartCoroutine(DisplayNegativeInfo());
        }
        else
        {
            StartCoroutine(DisplayPositiveInfo());
        }
    }

    private void PlayerHealthDamage()
    {
        Player.playerHealth = Player.playerHealth - healthDamage;
    }

    private void OpeningLeftDoor()
    {
        wardrobeLeftDoor.transform.RotateAround(leftDoorRotationVector, Vector3.up, doorOpeningSpeed * Time.deltaTime);
        if (wardrobeLeftDoor.transform.localEulerAngles.y >= 90)
        {
            canOpenDoors = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OpeningRightDoor()
    {
        wardrobeRightDoor.transform.RotateAround(rightDoorRotationVector, Vector3.down, doorOpeningSpeed * Time.deltaTime);
    }

    private void DeactivateButtons()
    {
        forceButton.gameObject.SetActive(false);
        useItemButton.gameObject.SetActive(false);
    }

    private IEnumerator DisplayPositiveInfo()
    {
        yield return new WaitForSeconds(1);
        ActivateWardrobeInfoText();
        wardrobeInfoText.color = Color.green;
        wardrobeInfoText.text = positiveInfo;
        StartCoroutine(DeactivateWardrobeInfoText());
    }

    private IEnumerator DisplayNegativeInfo()
    {
        yield return new WaitForSeconds(1);
        ActivateWardrobeInfoText();
        wardrobeInfoText.color = Color.red;
        wardrobeInfoText.text = negativeInfo;
        StartCoroutine(DeactivateWardrobeInfoText());
    }

    private void ActivateWardrobeInfoText()
    {
        wardrobeInfoText.gameObject.SetActive(true);
    }

    private IEnumerator DeactivateWardrobeInfoText()
    {
        yield return new WaitForSeconds(2.2f);
        wardrobeInfoText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerNumber)
        {
            forceButton.gameObject.SetActive(true);
            useItemButton.gameObject.SetActive(true);
            EquipmentChecker.CheckPlayerEquipment(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerNumber)
        {
            DeactivateButtons();
        }
    }

    public void ForceDoorButton()
    {
        PlayerUseForce();
        canOpenDoors = true;
        DeactivateButtons();
    }

    public void UseItemButton()
    {
        PlayerUseItem();
        canOpenDoors = true;
        DeactivateButtons();
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
