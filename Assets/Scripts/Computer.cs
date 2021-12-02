using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public Route currentRoute;

    public float speed = 50f;

    int routePos;

    int dice;
    int stepsToTake;

    bool isMoving;

    private void Update()
    {
        if (!Player.isPlayersTurn && !isMoving && !Player.isPlayerWinner)
        {
            Debug.Log("Computer's turn!");
            System.Threading.Thread.Sleep(1500);

            RollDice();

            if ((routePos + stepsToTake) < (currentRoute.childObjectList.Count - 1))  // if the amount of steps to take does not overflow, move player piece
            {
                StartCoroutine(Move());
            }
            else
            {
                StartCoroutine(Move());
                Debug.Log("COMPUTER WINS!");
                Player.isPlayerWinner = true;

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

        Player.isPlayersTurn = true;
        Debug.Log("Player's turn!");
    }

    bool MoveToNextNode(Vector3 goalNode)
    {
        return goalNode != (transform.position = Vector3.MoveTowards(transform.position, goalNode, speed * Time.deltaTime));
    }

    void RollDice()
    {
        dice = Random.Range(1, 7);
        stepsToTake = dice;

        Debug.Log("Computer rolled dice: " + stepsToTake);
    }
}
