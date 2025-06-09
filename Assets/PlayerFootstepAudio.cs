using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootstepAudio : MonoBehaviour
{
    [Header("Dependencies")]
    public Rigidbody playerRigidbody;
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Footstep Settings")]
    public AudioClip defaultFootstepClip;
    public AudioClip concreteFootstepClip;
    public float footstepInterval = 0.5f;
    public float minMoveSpeed = 0.1f;

    private AudioSource audioSource;
    private float footstepTimer = 0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    private void FixedUpdate()
    {
        bool isMoving = playerRigidbody.linearVelocity.magnitude > minMoveSpeed;
        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);

        if (isMoving && isGrounded)
        {
            footstepTimer += Time.fixedDeltaTime;

            if (footstepTimer >= footstepInterval)
            {
                PlayFootstep();
                footstepTimer = 0f;
            }
        }
        else
        {
            footstepTimer = 0f;
            StopFootstep();
        }
    }

   private void PlayFootstep()
{
    AudioClip chosenClip = GetFootstepClip();

    if (chosenClip != null)
    {
        if (audioSource.clip != chosenClip || !audioSource.isPlaying)
        {
            audioSource.Stop(); // Stop current sound even if still playing
            audioSource.clip = chosenClip;
            audioSource.Play();
        }
    }
}


    private AudioClip GetFootstepClip()
    {
        // Cast a ray downward from the groundCheck point to detect the surface
        if (Physics.Raycast(groundCheck.position, Vector3.down, out RaycastHit hit, 1f, groundLayer))
        {
            if (hit.collider.CompareTag("Concrete"))
            {
                return concreteFootstepClip;
            }
        }

        return defaultFootstepClip;
    }

    private void StopFootstep()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}


