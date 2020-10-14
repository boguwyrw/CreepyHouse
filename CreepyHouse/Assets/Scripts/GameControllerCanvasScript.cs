using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerCanvasScript : MonoBehaviour
{
    [SerializeField]
    private Button resumeGameButton;
    [SerializeField]
    private Button quitGameButton;

    public void PauseGameButton()
    {
        Time.timeScale = 0.0f;
        resumeGameButton.gameObject.SetActive(true);
        quitGameButton.gameObject.SetActive(true);
        if (PlayerScript.playerHealth == 0)
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
