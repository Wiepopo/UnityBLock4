using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;

public class MouseOnlyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Animator animator;
    private bool isPressing = false;

    [Header("Time to wait before executing action")]
    public float pressAnimationDuration = 0.15f;

    [Header("Function to run after press")]
    public UnityEvent onClick;

    public void PlaySound() {
        // Do nothing. Just here to silence the AnimationEvent.
    }


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isPressing)
            animator.SetBool("selected", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isPressing)
            animator.SetBool("selected", false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(ClickAnimation());
    }

    private IEnumerator ClickAnimation()
    {
        isPressing = true;
        animator.SetTrigger("pressed");

        // Temporarily clear selected so it doesn’t override press
        animator.SetBool("selected", false);

        yield return new WaitForSeconds(pressAnimationDuration);

        onClick?.Invoke();
        isPressing = false;

        // Optionally: re-enable hover if still hovered
        animator.SetBool("selected", true);
    }
}
