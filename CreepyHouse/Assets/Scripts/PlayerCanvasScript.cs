using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasScript : MonoBehaviour
{
    [SerializeField]
    private Text healthPointsText;

    private void Update()
    {
        healthPointsText.text = "Health points: " + PlayerScript.playerHealth;
    }
}
