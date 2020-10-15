using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField]
    private Text endGameInfoText;
    [SerializeField]
    private Button resumeGameButton;
    [SerializeField]
    private Button quitGameButton;

    private int playerNumber = 9;

    private string lostText = "SORRY, YOU LOST";
    private string winText = "CONGRATULATION, YOU WIN";

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (PlayerScript.playerHealth == 0)
        {
            endGameInfoText.color = Color.red;
            endGameInfoText.text = lostText;
            EndGame();
        }
    }

    private void EndGame()
    {
        endGameInfoText.gameObject.SetActive(true);
        quitGameButton.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerNumber)
        {
            endGameInfoText.text = winText;
            resumeGameButton.interactable = false;
            EndGame();
        }
    }
}
