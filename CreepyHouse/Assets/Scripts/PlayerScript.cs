using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private NewCharacterScript playerCleverGuy;
    [SerializeField]
    private NewCharacterScript playerStrongGuy;

    public static int playerHealth = 0;

    private int playerStrength = 0;
    private int playerDexterity = 0;
    private int playerStamina = 0;
    private int playerArtifice = 0;

    private void Awake()
    {
        if (CharactersScript.cleverGuy)
        {
            playerHealth = playerCleverGuy.health;
            playerStrength = playerCleverGuy.strength;
            playerDexterity = playerCleverGuy.dexterity;
            playerStamina = playerCleverGuy.stamina;
            playerArtifice = playerCleverGuy.artifice;
        }
        else
        {
            playerHealth = playerStrongGuy.health;
            playerStrength = playerStrongGuy.strength;
            playerDexterity = playerStrongGuy.dexterity;
            playerStamina = playerStrongGuy.stamina;
            playerArtifice = playerStrongGuy.artifice;
        }
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
