using UnityEngine;

public class InvisibleWallDespawn : MonoBehaviour
{
    private void Start()
    {
        // Make the wall invisible by disabling the MeshRenderer
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object touching the wall is "moveableplank"
        if (collision.gameObject.name == "moveablePlank")
        {
            // Despawn (disable) the wall
            gameObject.SetActive(false);
        }
    }
}
