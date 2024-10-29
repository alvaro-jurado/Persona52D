using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 targetPosition; // Position where player will move
    private bool isMoving = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMoving)
        {
            MovePlayer();
        }
    }

    public void MoveToObjectPosition(Vector3 objectPosition)
    {
        targetPosition = new Vector3(objectPosition.x, transform.position.y, 0f);
        isMoving = true;

        animator.SetBool("isWalking", true);
    }

    void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, transform.position.y, 0f), speed * Time.deltaTime);
        
        if (Mathf.Abs(transform.position.x - targetPosition.x) < 0.1f) 
        {
            isMoving = false;
            animator.SetBool("isWalking", false);
        }
    }


}

