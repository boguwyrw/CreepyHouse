using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField]
    private Text endGameInfoText;

    private void Update()
    {
        if (PlayerGuyScript.playerHealth == 0)
        {
            Time.timeScale = 0.0f;
            endGameInfoText.color = Color.red;
            endGameInfoText.text = "SORRY, YOU LOST";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Time.timeScale = 0.0f;
            endGameInfoText.text = "CONGRATULATION, YOU WIN";
            endGameInfoText.gameObject.SetActive(true);
        }
    }
}
