using UnityEngine;

public class Drawers : MonoBehaviour
{
    [SerializeField]
    private GameObject player = null;

    private PlayerTakeOpenObject playerTakeOpen;

    private string objectName = "";
    private bool canOpen = false;
    private float drawerOpeningLength = 1.4f;

    private void Start()
    {
        playerTakeOpen = player.GetComponent<PlayerTakeOpenObject>();
    }

    private void Update()
    {
        objectName = playerTakeOpen.GetObjectName();
        canOpen = playerTakeOpen.GetCanOpen();

        if (gameObject.name.Equals(objectName) && canOpen && transform.localPosition.z <= drawerOpeningLength)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }
}
