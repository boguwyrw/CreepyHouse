using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitDoorScript : MonoBehaviour
{
    [SerializeField]
    private Button openExitDoorButton;
    [SerializeField]
    private Text exitDoorInfoText;
    [SerializeField]
    private GameObject playerEquipment;
    [SerializeField]
    private Transform rotationPoint;

    private Vector3 rotationVector;
    private float doorOpeningSpeed = 50.0f;
    private bool canOpenExitDoor = false;

    private void Start()
    {
        rotationVector = new Vector3(rotationPoint.position.x, rotationPoint.position.y, rotationPoint.position.z);
    }

    private void Update()
    {
        if (canOpenExitDoor)
        {
            transform.RotateAround(rotationVector, Vector3.up, doorOpeningSpeed * Time.deltaTime);
            if (transform.localEulerAngles.y >= 180.0f)
            {
                doorOpeningSpeed = 0.0f;
            }
        }
        
    }

    private void CheckPlayerEquipment()
    {
        if (playerEquipment.transform.childCount > 0)
        {
            for (int i = 0; i < playerEquipment.transform.childCount; i++)
            {
                string nameOfItemInInventory = playerEquipment.transform.GetChild(i).name;
                if (nameOfItemInInventory.Equals("Key"))
                {
                    openExitDoorButton.gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            CheckPlayerEquipment();
        }
    }

    public void OpenExitDoorButton()
    {
        canOpenExitDoor = true;
    }
}
