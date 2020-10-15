using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadMovement : MonoBehaviour
{
    [SerializeField]
    private Joystick rightJoystick = null;

    private Vector2 direction;
    private Transform playerBody;
    private float horizontalRotation = 0.0f;
    private float verticalRotation = 0.0f;
    private float rotationLimit = 80.0f;
    private float headMovementSpeed = 55.0f;

    private void Start()
    {
        playerBody = transform.parent;
    }

    private void FixedUpdate()
    {
        horizontalRotation = rightJoystick.Horizontal;
        verticalRotation = rightJoystick.Vertical;
        
        Vector2 changesPosition = new Vector2(horizontalRotation, verticalRotation);
        direction += changesPosition;

        direction.y = Mathf.Clamp(direction.y, -rotationLimit, rotationLimit);
        transform.localRotation = Quaternion.AngleAxis(-direction.y * headMovementSpeed * Time.deltaTime, Vector3.right);
        playerBody.localRotation = Quaternion.AngleAxis(direction.x * headMovementSpeed * Time.deltaTime, Vector3.up);
    }
}
