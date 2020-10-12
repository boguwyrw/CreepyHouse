using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvasScript : MonoBehaviour
{
    [SerializeField]
    private Button resumeGameButton;
    [SerializeField]
    private Button quitGameButton;

    public void PauseGameButton()
    {
        Time.timeScale = 0.0f;
        resumeGameButton.gameObject.SetActive(true);
        resumeGameButton.gameObject.GetComponent<RectTransform>().SetAsFirstSibling();
        quitGameButton.gameObject.SetActive(true);
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
