using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIComputerMovement : MonoBehaviour
{
    public Player computer;
    public float MovementSpeed = 25f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    private int direction;

    Rigidbody rb;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }

        if (direction == -1)
        {
            if (isRotatingRight == true)
            {
                rb.velocity = new Vector3(0, 0, 0);
                transform.eulerAngles = new Vector2(0, -90); //flip the character on its x axis - to the right
                                                                //transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * MovementSpeed;
            }
        }

        if (direction == 1)
        {
            if (isRotatingLeft == true)
            {
                rb.velocity = new Vector3(0, 0, 0);
                transform.eulerAngles = new Vector2(0, 90); //flip the character on its x axis - to the left
                                                            //transform.position += new Vector3(1, 0, 0) * Time.deltaTime * MovementSpeed;
            }
        }

        if (isWalking == true)
        {
            if (computer.HasSuperSpeed())
            {
                rb.AddForce(transform.forward * 40);
                
            }
            else
            {
                rb.AddForce(transform.forward * MovementSpeed);
            }

            animator.SetBool("isRunning", true);
        }

        if (isWalking == false)
        {
            animator.SetBool("isRunning", false);
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    IEnumerator Wander()
    {
        int rotationTime = Random.Range(1, 1);
        int rotateWait = Random.Range(1, 1);
        int rotateDirection = Random.Range(1, 4);
        int walkWait = Random.Range(1, 1);
        int walkTime = Random.Range(1, 3);

        isWandering = true;
        isWalking = true;

        yield return new WaitForSeconds(walkTime);

        isWalking = false;

        yield return new WaitForSeconds(rotateWait);

        if(rotateDirection == 1)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingLeft = false;
            direction = 1;
            
        }

        if (rotateDirection == 2)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingRight = false;
            direction = -1;
        }

        isWandering = false;
    }
}
