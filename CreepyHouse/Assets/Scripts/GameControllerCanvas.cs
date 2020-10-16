using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerCanvas : MonoBehaviour
{
    [SerializeField]
    private Button resumeGameButton = null;
    [SerializeField]
    private Button quitGameButton = null;

    public void PauseGameButton()
    {
        Time.timeScale = 0.0f;
        resumeGameButton.gameObject.SetActive(true);
        quitGameButton.gameObject.SetActive(true);
        if (Player.playerHealth == 0)
        {
            resumeGameButton.interactable = false;
        }
    }

    public void ResumeGameButton()
    {
        Time.timeScale = 1.0f;
        resumeGameButton.gameObject.SetActive(false);
        quitGameButton.gameObject.SetActive(false);
    }

    public void QuitGameButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenuScene");
    }
}
