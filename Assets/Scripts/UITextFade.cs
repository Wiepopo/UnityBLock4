using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextFade : MonoBehaviour
{
    public TextMeshProUGUI introText;
    public float fadeDuration = 2f;
    public float displayTime = 3f;

    void Start()
    {
        StartCoroutine(FadeInAndOut());
    }

    IEnumerator FadeInAndOut()
    {
        // Fade In
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            SetTextAlpha(alpha);
            yield return null;
        }

        yield return new WaitForSeconds(displayTime);

        // Fade Out
        t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            SetTextAlpha(alpha);
            yield return null;
        }
    }

    void SetTextAlpha(float alpha)
    {
        Color color = introText.color;
        color.a = alpha;
        introText.color = color;
    }
}
