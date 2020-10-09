using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCouchSystemScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject obstacleCouch;

    private Vector3 jumpDirection;
    private float jumpForce = 105.0f;

    private Vector3 couchRotationVector;
    private float rotationForce = 20.0f;
    private bool canMoveCouch = false;

    private int playerStrength;
    private int playerDexterity;

    private void Start()
    {
        jumpDirection = new Vector3(player.transform.position.x, player.transform.position.y + 3, player.transform.position.z + 0.5f);

        couchRotationVector = new Vector3(obstacleCouch.transform.position.x - 1.8f, obstacleCouch.transform.position.y, obstacleCouch.transform.position.z);

        playerStrength = player.transform.GetChild(1).gameObject.GetComponent<PlayerGuyScript>().GetPlayerStrength();
        playerDexterity = player.transform.GetChild(1).gameObject.GetComponent<PlayerGuyScript>().GetPlayerDexterity();
        
    }

    private void Update()
    {
        if (canMoveCouch)
        {
            obstacleCouch.transform.RotateAround(couchRotationVector, Vector3.down, rotationForce * Time.deltaTime);
            if (obstacleCouch.transform.localEulerAngles.y <= 270.0f)
            {
                rotationForce = 0.0f;
                TurnOFFObstacleCouch();
            }
        }
    }

    private void PlayerJumpsOverObstacle()
    {
        player.GetComponent<Rigidbody>().AddForce(jumpDirection * jumpForce * Time.deltaTime, ForceMode.Impulse);
        TurnOFFObstacleCouch();
        if (playerDexterity < 8)
        {
            PlayerGuyScript.playerHealth = PlayerGuyScript.playerHealth - 2;
        }
    }

    private void PlayerMovesObstacle()
    {
        if (playerStrength < 8)
        {
            PlayerGuyScript.playerHealth = PlayerGuyScript.playerHealth - 2;
        }
    }

    private void TurnOFFObstacleCouch()
    {
        obstacleCouch.GetComponent<BoxCollider>().enabled = false;
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
