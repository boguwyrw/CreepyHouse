using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject playerCleverGuy;
    [SerializeField]
    private GameObject playerStrongGuy;

    private void Start()
    {
        if (CharactersScript.cleverGuy == false)
        {
            Destroy(playerCleverGuy);
        }
        else if (CharactersScript.strongGuy == false)
        {
            Destroy(playerStrongGuy);
        }
    }
}
