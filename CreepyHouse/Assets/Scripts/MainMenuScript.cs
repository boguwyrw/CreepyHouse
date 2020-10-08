using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void NewGameButton()
    {
        SceneManager.LoadScene("SelectCharacterScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
