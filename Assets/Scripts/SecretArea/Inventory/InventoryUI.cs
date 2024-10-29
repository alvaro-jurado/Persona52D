using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance; // Singleton to access from InventorySystem
    public GameObject inventoryPanel;
    public GameObject descriptionPanel;
    public TMP_Text descriptionText;
    private Coroutine typingCoroutine;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        inventoryPanel.SetActive(false);
        descriptionPanel.SetActive(false);
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        if (!inventoryPanel.activeSelf) {
            descriptionPanel.SetActive(false);
        }
    }

    public void ShowDescription(string description)
    {
        // If text is showing, we stop the previous coroutine
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        descriptionText.text = "";

        descriptionPanel.SetActive(true);

        // Start coroutine to make the text enter slowly
        typingCoroutine = StartCoroutine(TypeText(description));
    }

    public void HideDescription()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        descriptionPanel.SetActive(false);
        descriptionText.text = "";
    }

    // Coroutine to make the text enter slowly
    private IEnumerator TypeText(string description)
    {
        foreach (char letter in description.ToCharArray())
        {
            descriptionText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        typingCoroutine = null;
    }

}
