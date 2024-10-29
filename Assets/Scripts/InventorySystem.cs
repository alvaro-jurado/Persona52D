using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance; // Singleton for global access
    public List<InventoryItem> items = new List<InventoryItem>();
    public GameObject inventoryPanel;
    public GameObject inventoryItemPrefab;
    public TMP_Text descriptionText;

    private int currentSelectedIndex = -1;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(InventoryItem itemName)
    {
        items.Add(itemName);
        UpdateInventoryUI();
        Debug.Log("Item collected: " + itemName);
    }

    void UpdateInventoryUI()
    {
        // Clear the inventory panel before adding new slots
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject); // Remove existing slots
        }

        // Create a new slot for each item in the inventory
        for (int i = 0; i < items.Count; i++)
        {
            GameObject newSlot = Instantiate(inventoryItemPrefab, inventoryPanel.transform); // Instantiate a new slot

            Image slotImage = newSlot.transform.GetChild(0).GetComponent<Image>();
            slotImage.sprite = items[i].itemSprite;

            Button itemButton = newSlot.GetComponent<Button>();

            // Create a copy of i to ensure the listener works correctly
            int index = i;

            // Add a listener to the button to execute InspectItem when clicked
            itemButton.onClick.AddListener(() => InspectItem(index));
        }
    }

    public void InspectItem(int index)
    {
        if (index < items.Count)
        {
            // If the same item is clicked, hide the panel
            if (currentSelectedIndex == index)
            {
                Debug.Log("Clicked on the same item, hiding the panel.");
                InventoryUI.instance.HideDescription();
                currentSelectedIndex = -1;
            }
            else
            {
                InventoryItem selectedItem = items[index];
                Debug.Log("Inspecting: " + selectedItem.itemName);

                // Show the description of the selected item
                InventoryUI.instance.ShowDescription(selectedItem.description);

                // Update the currently selected item index
                currentSelectedIndex = index;
            }
        }
    }
}
