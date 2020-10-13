using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField]
    private Text endGameInfoText;

    private void Update()
    {
        if (PlayerGuyScript.playerHealth == 0)
        {
            endGameInfoText.color = Color.red;
            endGameInfoText.text = "SORRY, YOU LOST";
            EndGame();
        }
    }

    private void EndGame()
    {
        endGameInfoText.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            endGameInfoText.text = "CONGRATULATION, YOU WIN";
            EndGame();
        }
    }
}
