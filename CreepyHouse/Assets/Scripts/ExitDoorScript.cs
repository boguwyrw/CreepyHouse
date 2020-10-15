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
    private int playerNumber = 9;

    private string positiveInfo = "Congratulations, you can leave Creepy House";
    private string lackOfKeyInfo = "You don't have the Key";

    private void Start()
    {
        rotationVector = new Vector3(rotationPoint.position.x, rotationPoint.position.y, rotationPoint.position.z);
    }

    private void Update()
    {
        if (canOpenExitDoor)
        {
            OpeningExitDoor();
        }
        
    }

    private void OpeningExitDoor()
    {
        transform.RotateAround(rotationVector, Vector3.up, doorOpeningSpeed * Time.deltaTime);
        if (transform.localEulerAngles.y >= 180.0f)
        {
            doorOpeningSpeed = 0.0f;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private IEnumerator DisplayPositiveInfo()
    {
        yield return new WaitForSeconds(1);
        ActivateExitDoorInfoText();
        exitDoorInfoText.color = Color.green;
        exitDoorInfoText.text = positiveInfo;
        StartCoroutine(DeactivateExitDoorInfoText());
    }

    private void ActivateExitDoorInfoText()
    {
        exitDoorInfoText.gameObject.SetActive(true);
    }

    private IEnumerator DeactivateExitDoorInfoText()
    {
        yield return new WaitForSeconds(2.2f);
        exitDoorInfoText.gameObject.SetActive(false);
    }

    private void CheckExitDoor()
    {
        for (int i = 0; i < playerEquipment.transform.childCount; i++)
        {
            string nameOfItemInInventory = playerEquipment.transform.GetChild(i).name;
            if (nameOfItemInInventory.Equals("Key"))
            {
                openExitDoorButton.gameObject.SetActive(true);
                exitDoorInfoText.text = "";
            }
            else
            {
                exitDoorInfoText.color = Color.red;
                exitDoorInfoText.text = lackOfKeyInfo;
                ActivateExitDoorInfoText();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerNumber)
        {
            CheckExitDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerNumber)
        {
            openExitDoorButton.gameObject.SetActive(false);
            exitDoorInfoText.gameObject.SetActive(false);
        }
    }

    public void OpenExitDoorButton()
    {
        canOpenExitDoor = true;
        StartCoroutine(DisplayPositiveInfo());
    }
}
