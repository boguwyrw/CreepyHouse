using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour, IEquipmentHolder
{
    [SerializeField]
    private Button wreckingBarButton = null;
    [SerializeField]
    private Button axButton = null;
    [SerializeField]
    private Text chestInfoText = null;
    [SerializeField]
    private GameObject playerEquipment = null;
    [SerializeField]
    private GameObject player = null;

    [SerializeField]
    private GameObject chectWing = null;
    [SerializeField]
    private Transform chectBolt = null;

    private bool canOpenChest = false;
    private Vector3 rotationVector;
    private float chestWingSpeed = 150.0f;
    private int playerNumber = 9;

    private int healthDamage = 2;
    private int minimumRequiredPoints = 8;

    private int playerStrength = 0;
    private int playerDexterity = 0;

    private string positiveInfo = "Congratulations, you opened chest without problems";
    private string negativeInfo = "You hurt yourself by sharp lid";

    private Dictionary<string, Button> interactableDictionary = new Dictionary<string, Button>();

    private void Start()
    {
        playerStrength = player.GetComponent<Player>().GetPlayerStrength();
        playerDexterity = player.GetComponent<Player>().GetPlayerDexterity();

        rotationVector = new Vector3(chectBolt.position.x, chectBolt.position.y, chectBolt.position.z);

        wreckingBarButton.interactable = false;
        axButton.interactable = false;

        interactableDictionary.Add(ItemsNames.wreckingBarItemName, wreckingBarButton);
        interactableDictionary.Add(ItemsNames.axItemName, axButton);
    }

    private void Update()
    {
        if (canOpenChest)
        {
            OpeningChest();
        }
    }

    private void OpeningChest()
    {
        chectWing.transform.RotateAround(rotationVector, Vector3.forward, chestWingSpeed * Time.deltaTime);
        if (chectWing.transform.localEulerAngles.x <= 272.5f)
        {
            chestWingSpeed = 0.0f;
        }
    }

    private void PlayerUseWreckingBar()
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

    private void PlayerUseAx()
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

    private void PlayerHealthDamage()
    {
        Player.playerHealth = Player.playerHealth - healthDamage;
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

    private IEnumerator DisplayPositiveInfo()
    {
        yield return new WaitForSeconds(1);
        ActivateChestInfoText();
        chestInfoText.color = Color.green;
        chestInfoText.text = positiveInfo;
        StartCoroutine(DeactivateChestInfoText());
    }

    private IEnumerator DisplayNegativeInfo()
    {
        yield return new WaitForSeconds(1);
        ActivateChestInfoText();
        chestInfoText.color = Color.red;
        chestInfoText.text = negativeInfo;
        StartCoroutine(DeactivateChestInfoText());
    }

    private void ActivateChestInfoText()
    {
        chestInfoText.gameObject.SetActive(true);
    }

    private IEnumerator DeactivateChestInfoText()
    {
        yield return new WaitForSeconds(2.2f);
        chestInfoText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerNumber)
        {
            wreckingBarButton.gameObject.SetActive(true);
            axButton.gameObject.SetActive(true);
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

    public void WreckingBarButton()
    {
        PlayerCanOpenChest();
        DeactivateButtons();
        DeactivateChest();
        PlayerUseWreckingBar();
    }

    public void AxButton()
    {
        PlayerCanOpenChest();
        DeactivateButtons();
        DeactivateChest();
        PlayerUseAx();
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
