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
        // Set the starting color to fully opaque (alpha = 1)
        Color startColor = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
        // Set the target color to fully transparent (alpha = 0)
        Color targetColor = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);

        // Start the fade coroutine and wait for it to finish
        yield return FadeCoroutine(startColor, targetColor, duration);

        // Deactivate the GameObject after the fade-in completes
        gameObject.SetActive(false);
    }

    // Coroutine to handle fading out the image over a specified duration
    public IEnumerator FadeOutCoroutine(float duration)
    {
        // Set the starting color to fully transparent (alpha = 0)
        Color startColor = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
        // Set the target color to fully opaque (alpha = 1)
        Color targetColor = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);

        // Activate the GameObject before starting the fade-out
        gameObject.SetActive(true);
        // Start the fade coroutine and wait for it to finish
        yield return FadeCoroutine(startColor, targetColor, duration);
    }

    // Private coroutine that handles the transition between two colors over a specified duration
    private IEnumerator FadeCoroutine(Color startColor, Color targetColor, float duration)
    {
        float elapsedTime = 0; // Time elapsed since the start of the fade
        float elapsedPercentage = 0; // Percentage of the fade completed (0 to 1)

        // Continue fading until the full duration has elapsed
        while (elapsedPercentage < 1)
        {
            // Calculate the elapsed percentage based on elapsed time and duration
            elapsedPercentage = elapsedTime / duration;
            // Update the fadeImage color based on the linear interpolation between start and target colors
            fadeImage.color = Color.Lerp(startColor, targetColor, elapsedPercentage);

            yield return null; // Wait for the next frame
            elapsedTime += Time.deltaTime; // Increment the elapsed time by the time passed since last frame
        }
    }
}
