using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharactersScript : MonoBehaviour
{
    [SerializeField]
    private Button startGameButton;
    [SerializeField]
    private Toggle cleverGuyToggle;
    [SerializeField]
    private Toggle strongGuyToggle;

    private void Start()
    {
        startGameButton.interactable = false;
    }

    private void Update()
    {
        if (cleverGuyToggle.isOn == false && strongGuyToggle.isOn == false)
        {
            startGameButton.interactable = false;
        }
        else
        {
            startGameButton.interactable = true;
        }
    }

    public void StartGameButton()
    {
        SceneManager.LoadScene("GameScene");
    }
}
