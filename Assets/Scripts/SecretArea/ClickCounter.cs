using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickCounter : MonoBehaviour
{
    public int maxClicks = 10;
    private int currentClicks = 0;
    public string targetSceneName;

    private void OnMouseDown()
    {
        currentClicks++;

        if (currentClicks >= maxClicks)
        {
            LoadTargetScene();
        }
    }

    private void LoadTargetScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
