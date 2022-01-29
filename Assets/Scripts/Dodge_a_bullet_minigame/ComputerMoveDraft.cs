using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ComputerMoveDraft : MonoBehaviour
{
    System.Random random = new System.Random();

    public float speed;
    public float horizontalMove = 0f;
    public float runSpeed = 1f;
    public float MovementSpeed = 5;
    public float JumpForce = 6f;

    int rightBound = -60;
    int leftBound = 205;

    float distanceToMove;
    int direction;

    Boolean isMoving = false;

    private Rigidbody2D rigidbody;

    public Animator animator;

    public GameObject computerPlayer;

    void Start()
    {
        computerPlayer = GameObject.FindWithTag("Computer");
        Vector3 computerPosition = computerPlayer.transform.position;
        rigidbody = GetComponent<Rigidbody2D>();

        StartCoroutine(waiter());
    }

    void Update()
    {
        
        do
         {
             direction = random.Next(-1, 2);

         } while (direction == 0); // randomize until getting -1 (left) or 1 (right)


        // x of plane -50 (most right) 200 (most left)

         do
         {
             distanceToMove = random.Next(0, 265); // 265 is the width of the plane the players stand on.

         } while ((distanceToMove + computerPlayer.transform.position.x < rightBound && direction == 1) ||
                  (distanceToMove + computerPlayer.transform.position.x > leftBound && direction == -1));

        //distanceToMove += computerPlayer.transform.position.x;
         Debug.Log("before waiter...");

         StartCoroutine(waiter());
         isMoving = false;

         Debug.Log("after watier!");

         horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
         animator.SetFloat("Speed", Mathf.Abs(horizontalMove));


         if(direction == 1) // moves right randomaly
         {
             do
             {
                if(isMoving == false)
                {
                    transform.eulerAngles = new Vector2(0, -90); //flip the character on its x axis - to the right
                    transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * MovementSpeed;
                }

             } while (computerPlayer.transform.position.x == distanceToMove + computerPlayer.transform.position.x);

         }

         if (direction == -1) // moves left randomaly
         {
            do
            {
                if (isMoving == false)
                {
                    transform.eulerAngles = new Vector2(0, 90); //flip the character on its x axis - to the left
                    transform.position += new Vector3(1, 0, 0) * Time.deltaTime * MovementSpeed;
                }

            } while (computerPlayer.transform.position.x == distanceToMove + computerPlayer.transform.position.x);
         }

        /*if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector2(0, -90); //flip the character on its x axis - to the right
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * MovementSpeed;
        }



        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector2(0, 90); //flip the character on its x axis - to the left
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * MovementSpeed;
        }*/

    }

    IEnumerator waiter()
    {
        isMoving = true;
        yield return new WaitForSeconds(4);
    }
}
