using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleCouchSystemScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Vector3 jumpDirection;
    private float jumpForce = 105.0f;

    private int playerStrength;
    private int playerDexterity;

    private void Start()
    {
        jumpDirection = new Vector3(player.transform.position.x, player.transform.position.y + 3, player.transform.position.z + 0.5f);

        playerStrength = player.transform.GetChild(1).gameObject.GetComponent<PlayerGuyScript>().GetPlayerStrength();
        playerDexterity = player.transform.GetChild(1).gameObject.GetComponent<PlayerGuyScript>().GetPlayerDexterity();
        
    }

    private void PlayerJumpsOverObstacle()
    {
        player.GetComponent<Rigidbody>().AddForce(jumpDirection * jumpForce * Time.deltaTime, ForceMode.Impulse);
        if (playerDexterity < 8)
        {
            PlayerGuyScript.playerHealth = PlayerGuyScript.playerHealth - 2;
        }
    }

    public void JumpButton()
    {
        PlayerJumpsOverObstacle();
    }
}
