using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [Tooltip("Will start the fade in after this value (in seconds)")]
    public float timeToStartFading = 2f;
    [Tooltip("Higher values = faster Fade In")]
    public float fadeSpeed = 1f;

    public SpriteRenderer Verse1;
    public SpriteRenderer Verse2;
    public SpriteRenderer Verse3;
    public Button backButton;

    void Start()
    {
        SetAlpha(Verse1, 0);
        SetAlpha(Verse2, 0);
        SetAlpha(Verse3, 0);

        backButton.gameObject.SetActive(false);
        backButton.onClick.AddListener(LoadPreviousScene);

        StartCoroutine(ShowVersesWithFadeIn());
    }

    IEnumerator ShowVersesWithFadeIn()
    {
        yield return new WaitForSeconds(timeToStartFading);

        yield return StartCoroutine(FadeIn(Verse1));
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(FadeIn(Verse2));
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(FadeIn(Verse3));

        backButton.gameObject.SetActive(true);
    }

    IEnumerator FadeIn(SpriteRenderer spriteRenderer)
    {
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;

        while (spriteRenderer.color.a < 1)
        {
            color.a += Time.deltaTime * fadeSpeed;
            spriteRenderer.color = color;
            yield return null;
        }

        color.a = 1;
        spriteRenderer.color = color;
    }

    void SetAlpha(SpriteRenderer spriteRenderer, float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    void LoadPreviousScene()
    {
        if (!string.IsNullOrEmpty(SceneTracker.previousScene))
        {
            SceneManager.LoadScene(SceneTracker.previousScene);
        }
        else
        {
            Debug.LogWarning("No previous scene recorded.");
        }
    }
}
