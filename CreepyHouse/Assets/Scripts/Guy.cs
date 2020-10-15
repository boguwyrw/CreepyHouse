using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guy : MonoBehaviour
{
    [SerializeField]
    private NewCharacter newCharacter = null;

    [SerializeField]
    private Text characterNameText = null;
    [SerializeField]
    private Image characterImage = null;
    [SerializeField]
    private Text healthValueText = null;
    [SerializeField]
    private Text strengthValueText = null;
    [SerializeField]
    private Text dexterityValueText = null;
    [SerializeField]
    private Text staminaValueText = null;
    [SerializeField]
    private Text artificeValueText = null;

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
