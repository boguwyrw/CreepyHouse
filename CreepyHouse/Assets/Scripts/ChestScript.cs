using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour, IEquipmentHolderScript
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

    private string positiveInfo = "Congratulations, you opened chest without problems";
    private string negativeInfo = "You hurt yourself by sharp lid";

    private Dictionary<string, Button> interactableDictionary = new Dictionary<string, Button>();
    readonly string wreckingBarItemName = "WreckingBar";
    readonly string axItemName = "Ax";

    private void Start()
    {
        playerStrength = player.GetComponent<PlayerScript>().GetPlayerStrength();
        playerDexterity = player.GetComponent<PlayerScript>().GetPlayerDexterity();

        rotationVector = new Vector3(chectBolt.position.x, chectBolt.position.y, chectBolt.position.z);

        wreckingBarButton.interactable = false;
        axButton.interactable = false;

        interactableDictionary.Add(wreckingBarItemName, wreckingBarButton);
        interactableDictionary.Add(axItemName, axButton);
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
        PlayerScript.playerHealth = PlayerScript.playerHealth - healthDamage;
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
        if (other.gameObject.layer == 9)
        {
            wreckingBarButton.gameObject.SetActive(true);
            axButton.gameObject.SetActive(true);
            EquipmentCheckerScript.CheckPlayerEquipment(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
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
