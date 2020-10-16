using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Doors : MonoBehaviour
{
    [SerializeField]
    private Button wreckingBarButton = null;
    [SerializeField]
    private Button skeletonKeyButton = null;
    [SerializeField]
    private Text doorInfoText = null;
    [SerializeField]
    private GameObject player = null;

    private bool canOpenDoor = false;
 
    private int healthDamage = 2;
    private int minimumRequiredPoints = 6;

    private int playerStamina = 0;
    private int playerArtifice = 0;

    private string positiveInfo = "Congratulations, you opened door without problems";
    private string negativeInfo = "You hurt yourself by protruding door board";

    private void Start()
    {
        playerStamina = player.GetComponent<Player>().GetPlayerStamina();
        playerArtifice = player.GetComponent<Player>().GetPlayerArtifice();

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
        Player.playerHealth = Player.playerHealth - healthDamage;
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
        doorInfoText.text = positiveInfo;
        StartCoroutine(DeactivateDoorInfoText());
    }

    private IEnumerator DisplayNegativeInfo()
    {
        yield return new WaitForSeconds(1);
        ActivateDoorInfoText();
        doorInfoText.color = Color.red;
        doorInfoText.text = negativeInfo;
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
