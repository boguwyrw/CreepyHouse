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
            wreckingBarButton.gameObject.SetActive(false);
            axButton.gameObject.SetActive(false);
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
                    axButton.interactable = false;
                }
                else if (nameOfItemInInventory.Equals("Ax"))
                {
                    wreckingBarButton.interactable = false;
                    axButton.interactable = true;
                }
            }
        }
        else
        {
            wreckingBarButton.interactable = false;
            axButton.interactable = false;
        }
    }
}
