using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    private Button wreckingBarButton;
    [SerializeField]
    private Button skeletonKeyButton;
    [SerializeField]
    private Text doorInfoText;
    [SerializeField]
    private GameObject playerEquipment;
    [SerializeField]
    private GameObject player;

    private float doorOpeningSpeed = 50.0f;
    private float doorWidth = 0.0f;
    private Vector3 rotationVector;
    private bool canOpenDoor = false;
    private string nameOfPointedObject = "";
    private PlayerTakeOpenObjectScript playerTakeOpen;

    private int healthDamage = 2;
    private int minimumRequiredPoints = 6;

    private int playerStamina = 0;
    private int playerArtifice = 0;

    private void Start()
    {
        playerStamina = player.transform.GetChild(1).gameObject.GetComponent<PlayerGuyScript>().GetPlayerStamina();
        playerArtifice = player.transform.GetChild(1).gameObject.GetComponent<PlayerGuyScript>().GetPlayerArtifice();

        doorWidth = gameObject.GetComponent<Renderer>().bounds.size.x;
        rotationVector = new Vector3(transform.position.x + doorWidth / 2, transform.position.y, transform.position.z);

        playerTakeOpen = player.GetComponent<PlayerTakeOpenObjectScript>();
    }

    private void Update()
    {
        nameOfPointedObject = playerTakeOpen.GetObjectName();

        if (canOpenDoor && gameObject.name.Equals(nameOfPointedObject))
        {
            OpeningDoor();
        }
    }

    private void PlayerUseWreckingBar()
    {
        if (playerStamina < minimumRequiredPoints)
        {
            PlayerHealthDamage();
            StartCoroutine(DisplayNegativeInfo());
        }
        else
        {
            StartCoroutine(DisplayPositiveInfo());
        }
    }

    private void PlayerUseSkeletonKey()
    {
        if (playerArtifice < minimumRequiredPoints)
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

    private void OpeningDoor()
    {
        transform.RotateAround(rotationVector, Vector3.up, doorOpeningSpeed * Time.deltaTime);
        if (transform.localEulerAngles.y >= 90)
        {
            canOpenDoor = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void DeactivateButtons()
    {
        wreckingBarButton.gameObject.SetActive(false);
        skeletonKeyButton.gameObject.SetActive(false);
    }

    private IEnumerator DisplayPositiveInfo()
    {
        yield return new WaitForSeconds(1);
        ActivateCouchInfoText();
        doorInfoText.color = Color.green;
        doorInfoText.text = "Congratulations, you opened door without problems";
        StartCoroutine(DeactivateCouchInfoText());
    }

    private IEnumerator DisplayNegativeInfo()
    {
        yield return new WaitForSeconds(1);
        ActivateCouchInfoText();
        doorInfoText.color = Color.red;
        doorInfoText.text = "You hurt yourself by protruding board";
        StartCoroutine(DeactivateCouchInfoText());
    }

    private void ActivateCouchInfoText()
    {
        doorInfoText.gameObject.SetActive(true);
    }

    private IEnumerator DeactivateCouchInfoText()
    {
        yield return new WaitForSeconds(2.2f);
        doorInfoText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            wreckingBarButton.gameObject.SetActive(true);
            skeletonKeyButton.gameObject.SetActive(true);
            CheckPlayerEquipment();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            wreckingBarButton.gameObject.SetActive(false);
            skeletonKeyButton.gameObject.SetActive(false);
        }
    }

    private void CheckPlayerEquipment()
    {
        if (playerEquipment.transform.childCount > 0)
        {
            for (int i = 0; i < playerEquipment.transform.childCount; i++)
            {
                string nameOfItemInInventory = playerEquipment.transform.GetChild(i).name;
                if (nameOfItemInInventory.Equals("WreckingBar"))
                {
                    wreckingBarButton.interactable = true;
                }
                else if (nameOfItemInInventory.Equals("SkeletonKey"))
                {
                    skeletonKeyButton.interactable = true;
                }
            }
        }
        else
        {
            wreckingBarButton.interactable = false;
            skeletonKeyButton.interactable = false;
        }
    }

    public void WreckingBarButton()
    {
        PlayerUseWreckingBar();
        canOpenDoor = true;
        DeactivateButtons();
    }

    public void SkeletonKeyButton()
    {
        PlayerUseSkeletonKey();
        canOpenDoor = true;
        DeactivateButtons();
    }
}
