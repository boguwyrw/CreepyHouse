using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleShelfScript : MonoBehaviour
{
    [SerializeField]
    private Transform rotationAxis;
    [SerializeField]
    private Button useHandsButton;
    [SerializeField]
    private Button useItemButton;
    [SerializeField]
    private Text shelfInfoText;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject playerEquipment;

    private BoxCollider shelfBoxCollider;
    private Vector3 boxColliderSize;
    private Vector3 boxColliderCenter;

    private int healthDamage = 2;
    private int minimumRequiredPoints = 6;

    private int playerStamina = 0;
    private int playerArtifice = 0;

    private void Start()
    {
        shelfBoxCollider = GetComponent<BoxCollider>();
        boxColliderSize = new Vector3(shelfBoxCollider.size.x, shelfBoxCollider.size.y, shelfBoxCollider.size.z);
        boxColliderCenter = new Vector3(shelfBoxCollider.center.x, shelfBoxCollider.center.y, shelfBoxCollider.center.z);

        playerStamina = player.transform.GetChild(1).gameObject.GetComponent<PlayerGuyScript>().GetPlayerStamina();
        playerArtifice = player.transform.GetChild(1).gameObject.GetComponent<PlayerGuyScript>().GetPlayerArtifice();

        useItemButton.interactable = false;
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
        PlayerGuyScript.playerHealth = PlayerGuyScript.playerHealth - healthDamage;
    }

    private IEnumerator DisplayPositiveInfo()
    {
        yield return new WaitForSeconds(1);
        ActivateShelfInfoText();
        shelfInfoText.color = Color.green;
        shelfInfoText.text = "Congratulations, you blocked the shelf, you can go forward";
        StartCoroutine(DeactivateShelfInfoText());
    }

    private IEnumerator DisplayNegativeInfo()
    {
        yield return new WaitForSeconds(1);
        ActivateShelfInfoText();
        shelfInfoText.color = Color.red;
        shelfInfoText.text = "You hurt yourself on falling ledge";
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

    private void CheckPlayerEquipment()
    {
        for (int i = 0; i < playerEquipment.transform.childCount; i++)
        {
            string nameOfItemInInventory = playerEquipment.transform.GetChild(i).name;
            if (nameOfItemInInventory.Equals("Board"))
            {
                useItemButton.interactable = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            transform.RotateAround(rotationAxis.position, Vector3.left, 15);
            useHandsButton.gameObject.SetActive(true);
            useItemButton.gameObject.SetActive(true);
            CheckPlayerEquipment();
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
}
