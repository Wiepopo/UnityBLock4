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

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

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

        // Update animation speed parameter
        float currentSpeed = isMoving ? moveSpeed : 0f;
        animator.SetFloat("Speed", currentSpeed);

    }

    void MoveToTarget()
    {
        // Move towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);

        // Rotate to face the target while moving
        Vector3 direction = (targetPosition.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(-direction); // use -direction for correct facing
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        // Check if reached destination
        if (Vector3.Distance(transform.position, targetPosition.position) < 0.01f)
        {
            transform.position = targetPosition.position;
            isMoving = false;
            hasMoved = true;

            // After reaching the point, rotate to face "forward" (your custom direction)
            Vector3 backwardDirection = transform.position - player.position; // opposite of (player - transform)
            if (backwardDirection != Vector3.zero)
            {
                Quaternion faceBackward = Quaternion.LookRotation(backwardDirection);
                transform.rotation = faceBackward;
            }

            if (keyToActivate != null)
            {
                keyToActivate.SetActive(true);
            }
        }
    }
}



