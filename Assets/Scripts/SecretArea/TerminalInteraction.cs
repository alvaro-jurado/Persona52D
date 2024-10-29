using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalInteraction : MonoBehaviour
{
    public GameObject player;
    public Vector3 targetPosition; // Position where player goes
    public CodeLockController codeLock;
    public SpriteRenderer terminalSprite; // SpriteRenderer of terminal to change visibility
    public float blinkInterval = 0.5f;

    private bool playerInRange = false;
    private bool isBlinking = false;
    private bool terminalClick = false;

    void Start()
    {
        terminalSprite = GetComponent<SpriteRenderer>();
        StartBlinking();
    }

    void Update()
    {
        if (playerInRange && !codeLock.isCodeCorrect && terminalClick)
        {
            codeLock.OpenKeypad();
        }

        if (codeLock.isCodeCorrect && isBlinking)
        {
            StopBlinking();
        }
    }

    private void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(Blink());
        }
    }

    private void StopBlinking()
    {
        isBlinking = false;
        StopCoroutine(Blink());
        terminalSprite.enabled = true; // Make sure terminal is visible after the blinking
    }

    // Corutine that controls the blink
    IEnumerator Blink()
    {
        while (isBlinking && !codeLock.isCodeCorrect) // El parpadeo solo ocurre si el código no es correcto
        {
            terminalSprite.enabled = !terminalSprite.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Jugador dentro del rango del terminal.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            codeLock.keypadPanel.SetActive(false);
            Debug.Log("Jugador fuera del rango del terminal.");
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Objeto interactivo clicado: " + gameObject.name);
        player.GetComponent<PlayerMovement>().MoveToObjectPosition(transform.position);
        terminalClick = true;
    }
}
