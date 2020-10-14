using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IEquipmentHolderScript
{
    GameObject getEquipment();
    Dictionary<string, Button> getInteractables();
}
