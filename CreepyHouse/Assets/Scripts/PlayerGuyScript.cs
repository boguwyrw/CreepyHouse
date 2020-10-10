using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuyScript : MonoBehaviour
{
    [SerializeField]
    private NewCharacterScript newCharacter;

    public static int playerHealth = 0;

    private int playerStrength = 0;
    private int playerDexterity = 0;
    private int playerStamina = 0;
    private int playerArtifice = 0;

    private void Start()
    {
        playerHealth = newCharacter.health;
        playerStrength = newCharacter.strength;
        playerDexterity = newCharacter.dexterity;
        playerStamina = newCharacter.stamina;
        playerArtifice = newCharacter.artifice;
    }

    public int GetPlayerStrength()
    {
        return playerStrength;
    }

    public int GetPlayerDexterity()
    {
        return playerDexterity;
    }

    public int GetPlayerStamina()
    {
        return playerStamina;
    }

    public int GetPlayerArtifice()
    {
        return playerArtifice;
    }
}
