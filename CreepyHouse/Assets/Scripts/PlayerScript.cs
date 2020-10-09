using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject playerCleverGuy;
    [SerializeField]
    private GameObject playerStrongGuy;

    private float playerSpeed = 2.0f;

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

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * playerSpeed * Time.deltaTime);
        }
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
            playerSpeed = 2.0f;
        }
    }
}
