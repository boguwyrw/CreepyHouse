using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuyScript : MonoBehaviour
{
    [SerializeField]
    private NewCharacterScript newCharacter;

    [SerializeField]
    private Text characterNameText;
    [SerializeField]
    private Image characterImage;
    [SerializeField]
    private Text healthValueText;
    [SerializeField]
    private Text strengthValueText;
    [SerializeField]
    private Text dexterityValueText;
    [SerializeField]
    private Text staminaValueText;
    [SerializeField]
    private Text artificeValueText;

    private void Start()
    {
        characterNameText.text = newCharacter.characterName;
        characterImage.sprite = newCharacter.characterImage;
        healthValueText.text = newCharacter.health.ToString();
        strengthValueText.text = newCharacter.strength.ToString();
        dexterityValueText.text = newCharacter.dexterity.ToString();
        staminaValueText.text = newCharacter.stamina.ToString();
        artificeValueText.text = newCharacter.artifice.ToString();
    }
}
