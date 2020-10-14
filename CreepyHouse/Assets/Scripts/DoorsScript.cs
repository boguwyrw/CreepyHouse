using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorsScript : MonoBehaviour
{
    [SerializeField]
    private Button wreckingBarButton;
    [SerializeField]
    private Button skeletonKeyButton;
    [SerializeField]
    private Text doorInfoText;
    [SerializeField]
    private GameObject player;

    private bool canOpenDoor = false;
 
    private int healthDamage = 2;
    private int minimumRequiredPoints = 6;

    private int playerStamina = 0;
    private int playerArtifice = 0;

    private void Start()
    {
        playerStamina = player.GetComponent<PlayerScript>().GetPlayerStamina();
        playerArtifice = player.GetComponent<PlayerScript>().GetPlayerArtifice();

        wreckingBarButton.interactable = false;
        skeletonKeyButton.interactable = false;
    }

    private void PlayerUseWreckingBar()
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

    private void PlayerUseSkeletonKey()
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
        PlayerScript.playerHealth = PlayerScript.playerHealth - healthDamage;
    }

    private void DeactivateButtons()
    {
        wreckingBarButton.gameObject.SetActive(false);
        skeletonKeyButton.gameObject.SetActive(false);
    }

    private void DoorCanBeOpen()
    {
        canOpenDoor = true;
    }

    private void DoorCanNotBeOpen()
    {
        canOpenDoor = false;
    }

    private IEnumerator DisplayPositiveInfo()
    {
        yield return new WaitForSeconds(1);
        ActivateDoorInfoText();
        doorInfoText.color = Color.green;
        doorInfoText.text = "Congratulations, you opened door without problems";
        StartCoroutine(DeactivateDoorInfoText());
    }

    private IEnumerator DisplayNegativeInfo()
    {
        yield return new WaitForSeconds(1);
        ActivateDoorInfoText();
        doorInfoText.color = Color.red;
        doorInfoText.text = "You hurt yourself by protruding board";
        StartCoroutine(DeactivateDoorInfoText());
    }

    private void ActivateDoorInfoText()
    {
        doorInfoText.gameObject.SetActive(true);
    }

    private IEnumerator DeactivateDoorInfoText()
    {
        yield return new WaitForSeconds(2.2f);
        doorInfoText.gameObject.SetActive(false);
        Invoke("DoorCanNotBeOpen", 3.0f);
    }

    public bool GetCanOpenDoor()
    {
        return canOpenDoor;
    }

    public void WreckingBarButton()
    {
        PlayerUseWreckingBar();
        DoorCanBeOpen();
        DeactivateButtons();
    }

    public void SkeletonKeyButton()
    {
        PlayerUseSkeletonKey();
        DoorCanBeOpen();
        DeactivateButtons();
    }
}
