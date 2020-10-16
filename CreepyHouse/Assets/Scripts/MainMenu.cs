using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
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
