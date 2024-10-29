using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public GameObject player;
    public InventoryItem itemData;

    void OnMouseDown()
    {
        player.GetComponent<PlayerMovement>().MoveToObjectPosition(transform.position);
        InventorySystem.instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
