using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyMovementScript : MonoBehaviour
{
    [SerializeField]
    private Joystick leftJoystick;

    private float horizontalMove = 0.0f;
    private float verticalMove = 0.0f;
    private float playerSpeed = 3.0f;

    private void Update()
    {
        horizontalMove = leftJoystick.Horizontal * playerSpeed * Time.deltaTime;
        verticalMove = leftJoystick.Vertical * playerSpeed * Time.deltaTime;
        transform.Translate(horizontalMove, 0, verticalMove);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            playerSpeed = 0.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            playerSpeed = 3.0f;
        }
    }
}
