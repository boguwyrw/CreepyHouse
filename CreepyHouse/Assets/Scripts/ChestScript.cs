using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{
    [SerializeField]
    private Button wreckingBarButton;
    [SerializeField]
    private Button axButton;
    [SerializeField]
    private Text chestInfoText;
    [SerializeField]
    private GameObject playerEquipment;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject chectWing;
    [SerializeField]
    private Transform chectBolt;

    private bool canOpenChest = false;
    private Vector3 rotationVector;
    private float chestWingSpeed = 150.0f;

    private int healthDamage = 2;
    private int minimumRequiredPoints = 8;

    private int playerStrength = 0;
    private int playerDexterity = 0;

    private void Start()
    {
        playerStrength = player.transform.GetChild(1).gameObject.GetComponent<PlayerGuyScript>().GetPlayerStrength();
        playerDexterity = player.transform.GetChild(1).gameObject.GetComponent<PlayerGuyScript>().GetPlayerDexterity();

        rotationVector = new Vector3(chectBolt.position.x, chectBolt.position.y, chectBolt.position.z);

        wreckingBarButton.interactable = false;
        axButton.interactable = false;
    }

    private void Update()
    {
        if (canOpenChest)
        {
            chectWing.transform.RotateAround(rotationVector, Vector3.forward, chestWingSpeed * Time.deltaTime);
            if (chectWing.transform.localEulerAngles.x <= 272.5f)
            {
                chestWingSpeed = 0.0f;
            }
        }
        Debug.Log(chectWing.transform.localEulerAngles.x);
    }

    private void PlayerCanOpenChest()
    {
        canOpenChest = true;
    }

    private void DeactivateButtons()
    {
        wreckingBarButton.gameObject.SetActive(false);
        axButton.gameObject.SetActive(false);
    }

    private void DeactivateChest()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            wreckingBarButton.gameObject.SetActive(true);
            axButton.gameObject.SetActive(true);
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
            }

            for (int i = 0; i < playerEquipment.transform.childCount; i++)
            {
                string nameOfItemInInventory = playerEquipment.transform.GetChild(i).name;
                if (nameOfItemInInventory.Equals("Ax"))
                {
                    axButton.interactable = true;
                }
            }
        }
    }

    public void WreckingBarButton()
    {
        PlayerCanOpenChest();
        DeactivateButtons();
        DeactivateChest();
    }

    public void AxButton()
    {
        PlayerCanOpenChest();
        DeactivateButtons();
        DeactivateChest();
    }
}
