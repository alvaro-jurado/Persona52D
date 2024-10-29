using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    public GameObject combatHUD; // Referencia al HUD del combate

    void OnMouseDown()
    {
        // Activar el HUD de combate
        combatHUD.SetActive(true);
    }
}

