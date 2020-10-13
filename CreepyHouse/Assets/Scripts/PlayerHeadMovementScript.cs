﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadMovementScript : MonoBehaviour
{
    [SerializeField]
    private Joystick rightJoystick;

    private Vector2 direction;
    private Transform playerBody;
    private float horizontalRotation = 0.0f;
    private float verticalRotation = 0.0f;
    private float rotationLimit = 80.0f;
    private float headMovementSpeed = 0.85f;

    private void Start()
    {
        playerBody = transform.parent;
    }

    private void Update()
    {
        horizontalRotation = rightJoystick.Horizontal;
        verticalRotation = rightJoystick.Vertical;

        Vector2 changesPosition = new Vector2(horizontalRotation, verticalRotation);
        direction += changesPosition;

        direction.y = Mathf.Clamp(direction.y, -rotationLimit, rotationLimit);
        transform.localRotation = Quaternion.AngleAxis(-direction.y * headMovementSpeed, Vector3.right);
        playerBody.localRotation = Quaternion.AngleAxis(direction.x * headMovementSpeed, Vector3.up);

        if (Time.timeScale == 0.0f)
        {
            headMovementSpeed = 0.01f;
        }
        else
        {
            headMovementSpeed = 0.85f;
        }
    }
}
