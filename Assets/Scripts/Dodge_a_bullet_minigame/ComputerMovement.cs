using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ComputerMovement : MonoBehaviour
{
    System.Random random = new System.Random();

    public float speed;
    public float horizontalMove = 0f;
    public float runSpeed = 1f;
    public float MovementSpeed = 60;
    public float JumpForce = 6f;

    float distanceToMove;
    int direction;
    float distance = 40;
    float rightBound = 0;
    float leftBound = 270;
    float computerCurrentX;

    float idleTime = 3; //seconds

    Boolean isMoving = false;

    private Rigidbody2D rigidbody;

    public Animator animator;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        computerCurrentX = transform.position.x;
        StartCoroutine(waiter());
    }
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (idleTime <= 0)
        {
            do
            {
                direction = random.Next(-1, 2);

            } while (direction == 0); // randomize until getting -1 (left) or 1 (right)

            do
            {
                distanceToMove = random.Next(0, 265); // ~265 is the width of the plane the players stand on.

            } while ((computerCurrentX - distanceToMove < rightBound && direction == 1) ||
                      (computerCurrentX + distanceToMove > leftBound && direction == -1));

            Debug.Log(direction);

            if (direction == 1)
            {
                //StartCoroutine(waiter());
                transform.eulerAngles = new Vector2(0, -90); //flip the character on its x axis - to the right

                while(computerCurrentX != computerCurrentX + distanceToMove)
                {
                    transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * MovementSpeed;
                }

            }

            if (direction == -1)
            {
                transform.eulerAngles = new Vector2(0, 90); //flip the character on its x axis - to the left

                while (computerCurrentX != computerCurrentX + distanceToMove)
                {
                    transform.position += new Vector3(1, 0, 0) * Time.deltaTime * MovementSpeed;
                }
                

            }
        }
        else
        {
            idleTime -= Time.deltaTime;
        }
    }

    

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(4);
        
    }
}
