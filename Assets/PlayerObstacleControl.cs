using UnityEngine;
using UnityEngine.AI;

public class PlayerObstacleControl : MonoBehaviour
{
    public NavMeshObstacle playerObstacle;

    private Vector3 lastFramePosition;
    private float stillThreshold = 0.05f;

    void Start()
    {
        lastFramePosition = transform.position;
    }

    void Update()
    {
        Vector3 velocity = (transform.position - lastFramePosition) / Time.deltaTime;

        if (velocity.magnitude > stillThreshold)
        {
            // Player is moving
            if (playerObstacle != null)
                playerObstacle.carving = false;
        }
        else
        {
            // Player is idle
            if (playerObstacle != null)
                playerObstacle.carving = true;
        }

        lastFramePosition = transform.position;
    }
}
