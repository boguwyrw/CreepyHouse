using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyMovement : MonoBehaviour
{
    [SerializeField]
    private Joystick leftJoystick = null;

    private float horizontalMove = 0.0f;
    private float verticalMove = 0.0f;
    private float playerSpeed = 0.0f;
    private float speed = 3.2f;
    private int obstaclesNumber = 10;

    private void Start()
    {
        playerSpeed = speed;
    }

    private void Update()
    {
        horizontalMove = leftJoystick.Horizontal * playerSpeed * Time.deltaTime;
        verticalMove = leftJoystick.Vertical * playerSpeed * Time.deltaTime;
        transform.Translate(horizontalMove, 0, verticalMove);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == obstaclesNumber)
        {
            playerSpeed = 0.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == obstaclesNumber)
        {
            playerSpeed = speed;
        }
    }
}
