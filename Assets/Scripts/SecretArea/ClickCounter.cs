using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para cambiar de escena

public class ClickCounter : MonoBehaviour
{
    public int maxClicks = 10;       // Número de clics necesarios para cambiar de escena
    private int currentClicks = 0;   // Contador de clics actual
    public string targetSceneName;   // Nombre de la escena de destino (asegúrate de que coincida con el nombre de la escena en Build Settings)

    // Método que detecta los clics en el objeto
    private void OnMouseDown()
    {
        currentClicks++; // Incrementa el contador de clics

        // Comprueba si ha alcanzado el número máximo de clics
        if (currentClicks >= maxClicks)
        {
            LoadTargetScene(); // Carga la escena objetivo
        }
    }

    // Método para cargar la escena objetivo
    private void LoadTargetScene()
    {
        SceneManager.LoadScene(targetSceneName); // Cambia a la escena deseada
    }
}
