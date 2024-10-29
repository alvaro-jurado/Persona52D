using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class RouletteGame : MonoBehaviour
{
    public int chips = 100; // Fichas iniciales del jugador
    public int targetChips = 2000; // Meta para ganar
    public int betAmount = 100; // Cantidad de la apuesta

    public TextMeshProUGUI chipsText; // Texto que muestra las fichas del jugador
    public TextMeshProUGUI resultText; // Texto que muestra el resultado de la apuesta
    public TextMeshProUGUI rouletteNumberText; // Texto que muestra el n�mero de la ruleta
    public Button betOnRedButton; // Bot�n para apostar en blanco
    public Button betOnBlackButton; // Bot�n para apostar en negro
    public Button betOnZeroButton; // Bot�n para apostar al 0
    public Button restartButton; // Bot�n para reiniciar el juego
    [SerializeField] SceneController sceneController;

    void Start()
    {
        // Inicializar la interfaz
        UpdateChipsText();
        resultText.text = "";
        //restartButton.gameObject.SetActive(false); // Desactivar el bot�n de reinicio al inicio

        // Agregar eventos a los botones
        betOnRedButton.onClick.AddListener(() => PlaceBet("red"));
        betOnBlackButton.onClick.AddListener(() => PlaceBet("black"));
        betOnZeroButton.onClick.AddListener(() => PlaceBet("zero")); // Nueva apuesta al 0
        //restartButton.onClick.AddListener(RestartGame);
    }

    void PlaceBet(string color)
    {
        if (chips >= betAmount)
        {
            // Realizar la apuesta
            chips -= betAmount;

            // Iniciar la simulaci�n de la ruleta
            StartCoroutine(SpinRoulette(color));
        }
    }

    IEnumerator SpinRoulette(string color)
    {
        resultText.text = "Girando...";
        rouletteNumberText.text = "";

        // Simular la ruleta girando mostrando n�meros aleatorios
        for (int i = 0; i < 10; i++) // 10 giros
        {
            int rouletteResult = Random.Range(0, 37); // N�meros de 0 a 36
            UpdateRouletteNumberText(rouletteResult); // Actualizar el n�mero y su color
            yield return new WaitForSeconds(0.5f); // Esperar medio segundo
        }

        // Resultado final
        int finalResult = Random.Range(0, 37); // N�mero final de la ruleta
        UpdateRouletteNumberText(finalResult); // Actualizar el n�mero final y su color
        yield return new WaitForSeconds(1f); // Esperar un segundo para mostrar el resultado

        // Determinar el color del n�mero
        bool isWinning = false;
        if (finalResult == 0)
        {
            isWinning = (color == "zero"); // Ganar solo si se apost� al 0
            resultText.text = isWinning ? "�Ganaste!" : "Perdiste...";
        }
        else if ((finalResult % 2 == 0 && color == "black") || (finalResult % 2 != 0 && color == "red"))
        {
            isWinning = true;
            resultText.text = "�Ganaste!";
        }
        else
        {
            resultText.text = "Perdiste...";
        }

        if (isWinning)
        {
            chips += (color == "zero") ? betAmount * 10 : betAmount * 2; // Duplicar fichas si gana, 10 veces si gana apostando al 0
        }

        // Actualizar las fichas en la UI
        UpdateChipsText();

        // Comprobar condiciones de victoria o derrota
        if (chips >= targetChips)
        {
            // Ganaste el juego
            sceneController.LoadScene("SecretArea");
        }
        else if (chips < betAmount)
        {
            // Perdiste el juego
            resultText.text = "Te has quedado sin fichas...";
            betOnRedButton.interactable = false;
            betOnBlackButton.interactable = false;
            betOnZeroButton.interactable = false; // Desactivar el bot�n de apuesta al 0
            //restartButton.gameObject.SetActive(true); // Activar el bot�n de reinicio
            SceneTracker.previousScene = SceneManager.GetActiveScene().name;
            sceneController.LoadScene("GameOverScene");
        }
    }

    void UpdateRouletteNumberText(int number)
    {
        rouletteNumberText.text = number.ToString();

        // Cambiar el color del texto seg�n el n�mero
        if (number == 0)
        {
            rouletteNumberText.color = Color.green; // 0 es verde
        }
        else if (number % 2 == 0)
        {
            rouletteNumberText.color = Color.black; // Pares son negros
        }
        else
        {
            rouletteNumberText.color = Color.red; // Impares son rojos
        }
    }

    void UpdateChipsText()
    {
        chipsText.text = chips.ToString();
    }

    void RestartGame()
    {
        // Reiniciar la escena actual
        sceneController.LoadScene(SceneManager.GetActiveScene().name);
    }
}
