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
    public Button backButton; // Referencia al botón "Volver"

    void Start()
    {
        // Inicializa la opacidad de los versos a 0
        SetAlpha(Verse1, 0);
        SetAlpha(Verse2, 0);
        SetAlpha(Verse3, 0);

        // Desactiva el botón al inicio y agrega el listener para cargar la escena anterior
        backButton.gameObject.SetActive(false);
        backButton.onClick.AddListener(LoadPreviousScene);

        // Comienza la corrutina para mostrar los versos con un fade in
        StartCoroutine(ShowVersesWithFadeIn());
    }

    IEnumerator ShowVersesWithFadeIn()
    {
        // Espera antes de empezar el fade in del primer verso
        yield return new WaitForSeconds(timeToStartFading);

        // Fade in para cada verso
        yield return StartCoroutine(FadeIn(Verse1));
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(FadeIn(Verse2));
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(FadeIn(Verse3));

        // Una vez que aparecen todos los versos, activa el botón
        backButton.gameObject.SetActive(true);
    }

    IEnumerator FadeIn(SpriteRenderer spriteRenderer)
    {
        Color color = spriteRenderer.color;
        color.a = 0; // Asegúrate de que empiece con alpha en 0
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
        // Verifica si existe una escena anterior guardada en SceneTracker
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
