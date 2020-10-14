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
    private Button backButton;
    [SerializeField]
    private Toggle cleverGuyToggle;
    [SerializeField]
    private Toggle strongGuyToggle;
    [SerializeField]
    private GameObject cleverGuyGO;
    [SerializeField]
    private GameObject strongGuyGO;

    public static bool cleverGuy = false;
    public static bool strongGuy = false;

    private void Start()
    {
        startGameButton.interactable = false;
    }

    private void Update()
    {
        ActivationStartGameButton();
        CharacterChoice();
    }

    private void ActivationStartGameButton()
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
    
    private void CharacterChoice()
    {
        if (cleverGuyToggle.isOn == true)
        {
            cleverGuy = true;
            strongGuy = false;
        }
        
        if (strongGuyToggle.isOn == true)
        {
            cleverGuy = false;
            strongGuy = true;
        }
    }
    
    public void StartGameButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
