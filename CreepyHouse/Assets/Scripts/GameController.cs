using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Text endGameInfoText = null;
    [SerializeField]
    private Button resumeGameButton = null;
    [SerializeField]
    private Button quitGameButton = null;

    private int playerNumber = 9;

    private string lostText = "SORRY, YOU LOST";
    private string winText = "CONGRATULATION, YOU WIN";

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (Player.playerHealth == 0)
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
