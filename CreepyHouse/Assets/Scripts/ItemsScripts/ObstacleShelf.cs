using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleShelf : MonoBehaviour, IEquipmentHolder
{
    [SerializeField]
    private Transform rotationAxis = null;
    [SerializeField]
    private Button useHandsButton = null;
    [SerializeField]
    private Button useItemButton = null;
    [SerializeField]
    private Text shelfInfoText = null;
    [SerializeField]
    private GameObject playerEquipment = null;
    [SerializeField]
    private GameObject player = null;
 
    private BoxCollider shelfBoxCollider;
    private Vector3 boxColliderSize;
    private Vector3 boxColliderCenter;

    private int healthDamage = 2;
    private int minimumRequiredPoints = 6;
    private int playerNumber = 9;

    private int playerStamina = 0;
    private int playerArtifice = 0;

    private string positiveInfo = "Congratulations, you blocked the shelf, you can go forward";
    private string negativeInfo = "You hurt yourself on falling ledge";

    private Dictionary<string, Button> interactableDictionary = new Dictionary<string, Button>();

    private void Start()
    {
        playerStamina = player.GetComponent<Player>().GetPlayerStamina();
        playerArtifice = player.GetComponent<Player>().GetPlayerArtifice();

        shelfBoxCollider = GetComponent<BoxCollider>();
        boxColliderSize = new Vector3(shelfBoxCollider.size.x, shelfBoxCollider.size.y, shelfBoxCollider.size.z);
        boxColliderCenter = new Vector3(shelfBoxCollider.center.x, shelfBoxCollider.center.y, shelfBoxCollider.center.z);

        useItemButton.interactable = false;

        interactableDictionary.Add(ItemsNames.boardItemName, useItemButton);
    }

    private void InteractionWithShelf()
    {
        boxColliderSize.z = 0.5f;
        boxColliderSize = new Vector3(boxColliderSize.x, boxColliderSize.y, boxColliderSize.z);
        shelfBoxCollider.size = boxColliderSize;

        boxColliderCenter.z = 0.0f;
        boxColliderCenter = new Vector3(boxColliderCenter.x, boxColliderCenter.y, boxColliderCenter.z);
        shelfBoxCollider.center = boxColliderCenter;

        transform.RotateAround(rotationAxis.position, Vector3.left, -15);
    }

    private void PlayerUseHands()
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

    private void PlayerUseItem()
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
        Player.playerHealth = Player.playerHealth - healthDamage;
    }

    private IEnumerator DisplayPositiveInfo()
    {
        yield return new WaitForSeconds(1);
        ActivateShelfInfoText();
        shelfInfoText.color = Color.green;
        shelfInfoText.text = positiveInfo;
        StartCoroutine(DeactivateShelfInfoText());
    }

    private IEnumerator DisplayNegativeInfo()
    {
        yield return new WaitForSeconds(1);
        ActivateShelfInfoText();
        shelfInfoText.color = Color.red;
        shelfInfoText.text = negativeInfo;
        StartCoroutine(DeactivateShelfInfoText());
    }

    private void ActivateShelfInfoText()
    {
        shelfInfoText.gameObject.SetActive(true);
    }

    private IEnumerator DeactivateShelfInfoText()
    {
        yield return new WaitForSeconds(2.2f);
        shelfInfoText.gameObject.SetActive(false);
    }

    private void DeactivateButtons()
    {
        useHandsButton.gameObject.SetActive(false);
        useItemButton.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerNumber)
        {
            transform.RotateAround(rotationAxis.position, Vector3.left, 15);
            useHandsButton.gameObject.SetActive(true);
            useItemButton.gameObject.SetActive(true);
            EquipmentChecker.CheckPlayerEquipment(this);
        }
    }

    public void UseHandsButton()
    {
        InteractionWithShelf();
        PlayerUseHands();
        DeactivateButtons();
    }

    public void UseItemButton()
    {
        InteractionWithShelf();
        PlayerUseItem();
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
