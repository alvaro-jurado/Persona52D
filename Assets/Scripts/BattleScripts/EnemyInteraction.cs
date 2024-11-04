using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    public GameObject combatHUD;

    void OnMouseDown()
    {
        // Activate combat HUD
        combatHUD.SetActive(true);
    }
}

