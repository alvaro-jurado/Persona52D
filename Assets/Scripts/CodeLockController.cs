using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.Examples;

public class CodeLockController : MonoBehaviour
{
    public GameObject door;
    public TMP_InputField inputField;
    public GameObject keypadPanel;
    public bool isCodeCorrect = false;
    public GameObject player;

    private string correctCode = "9346";
    private string inputCode = "";

    void Start()
    {
        keypadPanel.SetActive(false);
    }

    public void OpenKeypad()
    {
        keypadPanel.SetActive(true);
    }

    public void AddDigit(string digit)
    {
        if (inputCode.Length < 4) // 4 digit limit keycode
        {
            inputCode += digit;
            Debug.Log("Current inputCode: " + inputCode);
            inputField.text = inputCode;
            inputField.ForceLabelUpdate();
        }
    }

    public void RemoveDigit()
    {
        if (inputCode.Length > 0)
        {
            inputCode = inputCode.Substring(0, inputCode.Length - 1);
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
            inputField.ForceLabelUpdate();
        }
    }

    public void ConfirmCode()
    {
        if (inputCode == correctCode)
        {
            Debug.Log("Código correcto, puerta abierta");
            door.GetComponent<OpenDoor>().OpenDoorAnimation(); // Calls the method to open the door
            isCodeCorrect = true;
            keypadPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Código incorrecto, intenta de nuevo");
            isCodeCorrect = false;
            inputField.text = "";
            inputCode = "";
        }
    }

    public void ExitTerminal()
    {
        isCodeCorrect = false;
        inputField.text = "";
        inputCode = "";
        
        Vector3 adjustedPosition = new Vector3(0f, -3.97f, 0f);
        player.GetComponent<PlayerMovement>().MoveToObjectPosition(adjustedPosition);
        keypadPanel.SetActive(false);
    }
}
