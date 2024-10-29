using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para cambiar de escena

public class ClickCounter : MonoBehaviour
{
    public int maxClicks = 10;       // N�mero de clics necesarios para cambiar de escena
    private int currentClicks = 0;   // Contador de clics actual
    public string targetSceneName;   // Nombre de la escena de destino (aseg�rate de que coincida con el nombre de la escena en Build Settings)

    // M�todo que detecta los clics en el objeto
    private void OnMouseDown()
    {
        currentClicks++; // Incrementa el contador de clics

        // Comprueba si ha alcanzado el n�mero m�ximo de clics
        if (currentClicks >= maxClicks)
        {
            LoadTargetScene(); // Carga la escena objetivo
        }
    }

    // M�todo para cargar la escena objetivo
    private void LoadTargetScene()
    {
        SceneManager.LoadScene(targetSceneName); // Cambia a la escena deseada
    }
}
