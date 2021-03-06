﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleCouch : MonoBehaviour
{
    [SerializeField]
    private GameObject player = null;
    [SerializeField]
    private Button jumpButton = null;
    [SerializeField]
    private Button moveButton = null;
    [SerializeField]
    private Text couchInfoText = null;

    private Vector3 jumpDirection;
    private float jumpForce = 150.0f;

    private Vector3 couchRotationVector;
    private float rotationForce = 20.0f;
    private bool canMoveCouch = false;
    private int healthDamage = 2;
    private int minimumRequiredPoints = 8;
    private int playerNumber = 9;

    private int playerStrength = 0;
    private int playerDexterity = 0;

    private string positiveInfo = "Congratulations, you are going forward";
    private string negativeInfo = "You hurt yourself by protruding spring";

    private void Start()
    {
        playerStrength = player.GetComponent<Player>().GetPlayerStrength();
        playerDexterity = player.GetComponent<Player>().GetPlayerDexterity();

        jumpDirection = new Vector3(player.transform.position.x, player.transform.position.y + 2.0f, player.transform.position.z + 1.0f);

        couchRotationVector = new Vector3(transform.position.x - 1.8f, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if (canMoveCouch)
        {
            transform.RotateAround(couchRotationVector, Vector3.down, rotationForce * Time.deltaTime);
            if (transform.localEulerAngles.y <= 270.0f)
            {
                rotationForce = 0.0f;
                TurnOffObstacleCouch();
            }
        }
    }

    private void PlayerJumpsOverObstacle()
    {
        player.GetComponent<Rigidbody>().AddForce(jumpDirection * jumpForce * Time.deltaTime, ForceMode.Impulse);
        if (playerDexterity < minimumRequiredPoints)
        {
            PlayerHealthDamage();
            StartCoroutine(DisplayNegativeInfo());
        }
        else
        {
            StartCoroutine(DisplayPositiveInfo());
        }
    }

    private void PlayerMovesObstacle()
    {
        if (playerStrength < minimumRequiredPoints)
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

    private void TurnOffObstacleCouch()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    private IEnumerator DisplayPositiveInfo()
    {
        yield return new WaitForSeconds(1);
        ActivateCouchInfoText();
        couchInfoText.color = Color.green;
        couchInfoText.text = positiveInfo;
        StartCoroutine(DeactivateCouchInfoText());
    }

    private IEnumerator DisplayNegativeInfo()
    {
        yield return new WaitForSeconds(1);
        ActivateCouchInfoText();
        couchInfoText.color = Color.red;
        couchInfoText.text = negativeInfo;
        StartCoroutine(DeactivateCouchInfoText());
    }

    private void ActivateCouchInfoText()
    {
        couchInfoText.gameObject.SetActive(true);
    }

    private IEnumerator DeactivateCouchInfoText()
    {
        yield return new WaitForSeconds(2.2f);
        couchInfoText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerNumber)
        {
            jumpButton.gameObject.SetActive(true);
            moveButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerNumber)
        {
            jumpButton.gameObject.SetActive(false);
            moveButton.gameObject.SetActive(false);
            TurnOffObstacleCouch();
        }
    }

    public void JumpButton()
    {
        PlayerJumpsOverObstacle();
    }

    public void MoveButton()
    {
        PlayerMovesObstacle();
        canMoveCouch = true;
    }
}
