using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentCheckerScript : MonoBehaviour
{
    public static void CheckPlayerEquipment(IEquipmentHolderScript equipmentHolder)
    {
        for (int i = 0; i < equipmentHolder.getEquipment().transform.childCount; i++)
        {
            string nameOfItemInInventory = equipmentHolder.getEquipment().transform.GetChild(i).name;
            Button interactableButton = null;
            if (equipmentHolder.getInteractables().TryGetValue(nameOfItemInInventory, out interactableButton))
            {
                interactableButton.interactable = true;
            }
        }
    }
}
