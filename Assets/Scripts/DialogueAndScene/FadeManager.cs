using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    private Image fadeImage;

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    // Coroutine to handle fading in the image over a specified duration
    public IEnumerator FadeInCoroutine(float duration)
    {
        Color startColor = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
        Color targetColor = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);

        yield return FadeCoroutine(startColor, targetColor, duration);

        gameObject.SetActive(false);
    }

    // Coroutine to handle fading out the image over a specified duration
    public IEnumerator FadeOutCoroutine(float duration)
    {
        Color startColor = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
        Color targetColor = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);

        gameObject.SetActive(true);

        yield return FadeCoroutine(startColor, targetColor, duration);
    }

    // Private coroutine that handles the transition between two colors over a specified duration
    private IEnumerator FadeCoroutine(Color startColor, Color targetColor, float duration)
    {
        float elapsedTime = 0;
        float elapsedPercentage = 0;

        // Continue fading until the full duration has elapsed
        while (elapsedPercentage < 1)
        {
            elapsedPercentage = elapsedTime / duration;
            fadeImage.color = Color.Lerp(startColor, targetColor, elapsedPercentage);

            yield return null;
            elapsedTime += Time.deltaTime;
        }
    }
}
