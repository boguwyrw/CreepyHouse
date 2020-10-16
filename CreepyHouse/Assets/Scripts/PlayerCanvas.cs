using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    [SerializeField]
    private Text healthPointsText = null;

    private void Update()
    {
        healthPointsText.text = "Health points: " + Player.playerHealth;
    }
}
