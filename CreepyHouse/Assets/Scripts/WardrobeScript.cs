using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WardrobeScript : MonoBehaviour
{
    [SerializeField]
    private Button forceButton;
    [SerializeField]
    private Button useItemButton;
    [SerializeField]
    private Text wardrobeInfoText;
    [SerializeField]
    private GameObject playerEquipment;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject wardrobeLeftDoor;
    [SerializeField]
    private GameObject wardrobeRightDoor;
    [SerializeField]
    private Transform rotationAxisLeft;
    [SerializeField]
    private Transform rotationAxisRight;

    private float doorOpeningSpeed = 50.0f;
    private Vector3 leftDoorRotationVector;
    private Vector3 rightDoorRotationVector;
    private bool canOpenDoors = false;

    private int healthDamage = 2;
    private int minimumRequiredPoints = 8;

    private int playerStrength = 0;
    private int playerDexterity = 0;

    private void Start()
    {
        playerStrength = player.transform.GetChild(1).gameObject.GetComponent<PlayerGuyScript>().GetPlayerStrength();
        playerDexterity = player.transform.GetChild(1).gameObject.GetComponent<PlayerGuyScript>().GetPlayerDexterity();

        leftDoorRotationVector = new Vector3(rotationAxisLeft.position.x, rotationAxisLeft.position.y, rotationAxisLeft.position.z);
        rightDoorRotationVector = new Vector3(rotationAxisRight.position.x, rotationAxisRight.position.y, rotationAxisRight.position.z);

        useItemButton.interactable = false;
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
        PlayerGuyScript.playerHealth = PlayerGuyScript.playerHealth - healthDamage;
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
        wardrobeInfoText.text = "Congratulations, you opened wardrobe without problems";
        StartCoroutine(DeactivateWardrobeInfoText());
    }

    private IEnumerator DisplayNegativeInfo()
    {
        yield return new WaitForSeconds(1);
        ActivateWardrobeInfoText();
        wardrobeInfoText.color = Color.red;
        wardrobeInfoText.text = "You hurt yourself by loose handle";
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

    private void CheckPlayerEquipment()
    {
        for (int i = 0; i < playerEquipment.transform.childCount; i++)
        {
            string nameOfItemInInventory = playerEquipment.transform.GetChild(i).name;
            if (nameOfItemInInventory.Equals("Screwdriver"))
            {
                useItemButton.interactable = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            forceButton.gameObject.SetActive(true);
            useItemButton.gameObject.SetActive(true);
            CheckPlayerEquipment();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
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
}
