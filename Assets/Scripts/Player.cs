using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Route currentRoute;

    public float speed = 50f;

    int routePos;

    int dice;
    int stepsToTake;

    public static bool isPlayersTurn;
    public static bool isPlayerWinner;
    bool isMoving;

    private void Start()
    {
        isPlayersTurn = true;
        isPlayerWinner = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving && isPlayersTurn && !isPlayerWinner)
        {
            RollDice();

            if ((routePos + stepsToTake) < (currentRoute.childObjectList.Count - 1))  // if the amount of steps to take does not overflow, move player piece
            {
                StartCoroutine(Move());
            } 
            else
            {
                StartCoroutine(Move());
                isPlayerWinner = true;
                Debug.Log("PLAYER WINS!!");

                // announce winner
            }
        }
    }

    IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while (stepsToTake > 0 && (routePos != (currentRoute.childObjectList.Count - 1)))
        {
            routePos++;

            Vector3 nextPos = currentRoute.childObjectList[routePos].position;

            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);

            stepsToTake--;
        }

        isMoving = false;

        // check if landed on final tile; announce winner

        // check if landed on a special tile; apply special effect / initiate minigame / etc.

        isPlayersTurn = false;
    }

    bool MoveToNextNode(Vector3 goalNode)
    {
        return goalNode != (transform.position = Vector3.MoveTowards(transform.position, goalNode, speed * Time.deltaTime));
    }

    void RollDice()
    {
        dice = Random.Range(1, 7);
        stepsToTake = dice;

        Debug.Log("Player rolled dice: " + stepsToTake);
    }
}
