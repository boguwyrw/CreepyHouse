using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IEquipmentHolder
{
    GameObject getEquipment();
    Dictionary<string, Button> getInteractables();
}
