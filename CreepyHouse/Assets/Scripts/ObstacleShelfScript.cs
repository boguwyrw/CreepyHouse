using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleShelfScript : MonoBehaviour
{
    [SerializeField]
    private Transform rotationAxis;
    [SerializeField]
    private Button useHandsButton;
    [SerializeField]
    private Button useItemButton;
    [SerializeField]
    private Text shelfInfoText;

    private BoxCollider shelfBoxCollider;
    private Vector3 boxColliderSize;
    private Vector3 boxColliderCenter;

    private void Start()
    {
        shelfBoxCollider = GetComponent<BoxCollider>();
        boxColliderSize = new Vector3(shelfBoxCollider.size.x, shelfBoxCollider.size.y, shelfBoxCollider.size.z);
        boxColliderCenter = new Vector3(shelfBoxCollider.center.x, shelfBoxCollider.center.y, shelfBoxCollider.center.z);
    }

    private void InteractionWithShelf()
    {
        boxColliderSize.z = 1;
        boxColliderSize = new Vector3(boxColliderSize.x, boxColliderSize.y, boxColliderSize.z);
        shelfBoxCollider.size = boxColliderSize;

        boxColliderCenter.z = 0;
        boxColliderCenter = new Vector3(boxColliderCenter.x, boxColliderCenter.y, boxColliderCenter.z);
        shelfBoxCollider.center = boxColliderCenter;

        transform.RotateAround(rotationAxis.position, Vector3.left, -15);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            transform.RotateAround(rotationAxis.position, Vector3.left, 15);
            useHandsButton.gameObject.SetActive(true);
            useItemButton.gameObject.SetActive(true);
        }
    }

    public void UseHandsButton()
    {
        InteractionWithShelf();
    }

    public void UseItemButton()
    {
        InteractionWithShelf();
    }
}
