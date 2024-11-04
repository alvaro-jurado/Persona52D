using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public GameObject player;
    public Vector3 doorPosition;
    public CodeLockController codeLock;
    [SerializeField] SceneController sceneController;

    private bool doorIsOpen = false;

    void Update()
    {
        if (codeLock.isCodeCorrect && !doorIsOpen)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        doorIsOpen = true;
        Debug.Log("The door is open.");
    }

    // Detects if the player is within the door's range
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sceneController.LoadScene("CombatScene");
            Debug.Log("Starting fade to black and changing scene.");
        }
    }

    // Detects if the player leaves the door's range
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player out of the door's range.");
        }
    }

    // Method to handle mouse clicks on the door
    private void OnMouseDown()
    {
        if (doorIsOpen)
        {
            Vector3 adjustedPosition = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);

            // If the door is open and the player is in range, move the player to the adjusted position
            player.GetComponent<PlayerMovement>().MoveToObjectPosition(adjustedPosition);
            Debug.Log("Player moving to an adjusted position to the right.");
        }
        else
        {
            Debug.Log("The door is closed or the player is out of range.");
        }
    }
}
