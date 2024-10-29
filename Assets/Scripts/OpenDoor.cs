using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoorAnimation()
    {
        Debug.Log("Abre");
        if (!isOpen)
        {
            animator.SetBool("isOpening", true);
            isOpen = true;
        }
    }
}
