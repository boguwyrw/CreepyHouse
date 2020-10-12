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

    private float doorWidth = 0.0f;
    private Vector3 rotationVector;
    private float doorOpeningSpeed = 50.0f;

    private void Start()
    {
        doorWidth = gameObject.GetComponent<Renderer>().bounds.size.x;
        rotationVector = new Vector3(transform.position.x + doorWidth / 2, transform.position.y, transform.position.z);
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
        transform.RotateAround(rotationVector, Vector3.up, doorOpeningSpeed * Time.deltaTime);
    }
}
