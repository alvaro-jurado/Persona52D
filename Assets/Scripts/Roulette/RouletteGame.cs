using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class RouletteGame : MonoBehaviour
{
    public int chips = 100;
    public int targetChips = 800;
    public int betAmount = 100;

    public TextMeshProUGUI chipsText;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI rouletteNumberText;
    public Button betOnRedButton;
    public Button betOnBlackButton;
    public Button betOnZeroButton;
    public Button restartButton;
    [SerializeField] SceneController sceneController;

    void Start()
    {
        UpdateChipsText();
        resultText.text = "";

        betOnRedButton.onClick.AddListener(() => PlaceBet("red"));
        betOnBlackButton.onClick.AddListener(() => PlaceBet("black"));
        betOnZeroButton.onClick.AddListener(() => PlaceBet("zero"));
    }

    void PlaceBet(string color)
    {
        if (chips >= betAmount)
        {
            chips -= betAmount;
            StartCoroutine(SpinRoulette(color));
        }
    }

    IEnumerator SpinRoulette(string color)
    {
        resultText.text = "Girando...";
        rouletteNumberText.text = "";

        for (int i = 0; i < 10; i++)
        {
            int rouletteResult = Random.Range(0, 37);
            UpdateRouletteNumberText(rouletteResult);
            yield return new WaitForSeconds(0.5f);
        }

        int finalResult = Random.Range(0, 37);
        UpdateRouletteNumberText(finalResult);
        yield return new WaitForSeconds(1f);

        bool isWinning = false;
        if (finalResult == 0)
        {
            isWinning = (color == "zero");
            resultText.text = isWinning ? "¡Ganaste!" : "Perdiste...";
        }
        else if ((finalResult % 2 == 0 && color == "black") || (finalResult % 2 != 0 && color == "red"))
        {
            isWinning = true;
            resultText.text = "¡Ganaste!";
        }
        else
        {
            resultText.text = "Perdiste...";
        }

        if (isWinning)
        {
            chips += (color == "zero") ? betAmount * 10 : betAmount * 2;
        }

        UpdateChipsText();

        if (chips >= targetChips)
        {
            sceneController.LoadScene("SecretArea");
        }
        else if (chips < betAmount)
        {
            resultText.text = "Te has quedado sin fichas...";
            betOnRedButton.interactable = false;
            betOnBlackButton.interactable = false;
            betOnZeroButton.interactable = false;
            SceneTracker.previousScene = SceneManager.GetActiveScene().name;
            sceneController.LoadScene("GameOverScene");
        }
    }

    void UpdateRouletteNumberText(int number)
    {
        rouletteNumberText.text = number.ToString();

        if (number == 0)
        {
            rouletteNumberText.color = Color.green;
        }
        else if (number % 2 == 0)
        {
            rouletteNumberText.color = Color.black;
        }
        else
        {
            rouletteNumberText.color = Color.red;
        }
    }

    void UpdateChipsText()
    {
        chipsText.text = chips.ToString();
    }

    void RestartGame()
    {
        sceneController.LoadScene(SceneManager.GetActiveScene().name);
    }
}
