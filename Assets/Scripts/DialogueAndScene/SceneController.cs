using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    [SerializeField]
    private float sceneFadeDuration;

    // A reference to a FadeManager, which manages the fade-in and fade-out effects.
    private FadeManager sceneFade;

    private void Awake()
    {
        sceneFade = GetComponentInChildren<FadeManager>();
    }

    // It ensures that the scene begins with a fade-in effect by using the FadeManager's FadeInCoroutine method.
    private IEnumerator Start()
    {
        yield return sceneFade.FadeInCoroutine(sceneFadeDuration);
    }

    // It starts a coroutine to handle the fade-out and loading process smoothly.
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    // Coroutine that handles the process of fading out and loading a new scene asynchronously.
    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return sceneFade.FadeOutCoroutine(sceneFadeDuration);
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}
