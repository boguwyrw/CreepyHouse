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
    private GameObject playerEquipment;
    [SerializeField]
    private GameObject player;

    private int healthDamage = 2;
    private int minimumRequiredPoints = 8;

    private int playerStrength = 0;
    private int playerDexterity = 0;

    private void Start()
    {
        playerStrength = player.transform.GetChild(1).gameObject.GetComponent<PlayerGuyScript>().GetPlayerStrength();
        playerDexterity = player.transform.GetChild(1).gameObject.GetComponent<PlayerGuyScript>().GetPlayerDexterity();
    }

    private void PlayerUseForce()
    {
        if (playerStrength < minimumRequiredPoints)
        {
            PlayerHealthDamage();
        }
    }

    private void PlayerUseItem()
    {
        if (playerDexterity < minimumRequiredPoints)
        {
            PlayerHealthDamage();
        }
    }

    private void PlayerHealthDamage()
    {
        PlayerGuyScript.playerHealth = PlayerGuyScript.playerHealth - healthDamage;
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
            forceButton.gameObject.SetActive(false);
            useItemButton.gameObject.SetActive(false);
        }
    }

    private void CheckPlayerEquipment()
    {
        if (playerEquipment.transform.childCount > 0)
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
        else
        {
            useItemButton.interactable = false;
        }
    }

    public void ForceDoorButton()
    {
        PlayerUseForce();
    }

    public void UseItemButton()
    {
        PlayerUseItem();
    }
}
