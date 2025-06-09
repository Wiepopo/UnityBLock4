using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootstepAudio : MonoBehaviour
{
    [Header("Dependencies")]
    public Rigidbody playerRigidbody;
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Footstep Settings")]
    public AudioClip footstepClip;
    public float footstepInterval = 0.5f;
    public float minMoveSpeed = 0.1f;

    private AudioSource audioSource;
    private float footstepTimer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    void FixedUpdate()
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
            StopFootstep(); // 🔈 Stop sound if movement stops or player is not grounded
        }
    }

    void PlayFootstep()
    {
        if (footstepClip != null && !audioSource.isPlaying)
        {
            audioSource.clip = footstepClip;
            audioSource.Play();
        }
    }

    void StopFootstep()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}

