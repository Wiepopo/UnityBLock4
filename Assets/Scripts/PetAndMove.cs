using UnityEngine;

public class PetAndMove : MonoBehaviour
{
    [Header("Interaction Settings")]
    public Transform player;
    public float interactionDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    [Header("Movement Settings")]
    public Transform targetPosition;
    public float moveSpeed = 2f;

    [Header("Unlockable Item")]
    public GameObject keyToActivate; // Drag your hidden key object here

    private bool isPlayerNear = false;
    private bool hasMoved = false;
    private bool isMoving = false;

    void Update()
    {
        if (!hasMoved && isPlayerNear && Input.GetKeyDown(interactKey))
        {
            isMoving = true;
        }

        if (isMoving)
        {
            MoveToTarget();
        }

        float distance = Vector3.Distance(player.position, transform.position);
        isPlayerNear = distance <= interactionDistance;
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition.position) < 0.01f)
        {
            transform.position = targetPosition.position;
            isMoving = false;
            hasMoved = true;

            if (keyToActivate != null)
            {
                keyToActivate.SetActive(true);
            }
        }
    }
}
