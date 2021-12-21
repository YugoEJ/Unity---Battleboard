using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public Player player;
    public Computer computer;

    private bool playerTurn;
    private bool gameOver;

    private int dice;
    private int stepsToTake;

    private void Start()
    {
        gameOver = false;
        playerTurn = true;
        //FlipCoin();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !player.isMoving && gameOver == false && playerTurn == true)
        {
            MovePlayer();
        }
        else if (!computer.isMoving && gameOver == false && playerTurn == false) // (if computer's turn)
        {
            MoveComputer();
        }
    }

    private void MovePlayer()
    {
        RollDice();

        if (player.canMove(stepsToTake))
        {
            StartCoroutine(player.Move(stepsToTake));
            playerTurn = !playerTurn;
        }
        else
        {
            StartCoroutine(player.Move(stepsToTake));
            gameOver = true;
        }

        if (!gameOver)
        {
            Debug.Log("Computer's turn.");
        }
        else
        {
            Debug.Log("Player wins!");
        }

    }

    private void MoveComputer()
    {
        RollDice();

        if (computer.canMove(stepsToTake))
        {
            StartCoroutine(computer.Move(stepsToTake));
            playerTurn = !playerTurn;
        }
        else
        {
            StartCoroutine(player.Move(stepsToTake));
            gameOver = true;
        }

        if (!gameOver)
        {
            Debug.Log("Player's turn.");
        }
        else
        {
            Debug.Log("Computer wins!");
        }
    }

    private void RollDice()
    {
        dice = Random.Range(1, 7);
        stepsToTake = dice;

        Debug.Log("Dice rolled: " + stepsToTake);
    }

    /*bool FlipCoin()
    {
        Debug.Log("Flipping coin to decide who goes first!");
        StartCoroutine(Delay());
        int coin = Random.Range(0, 2);

        if (coin == 0)
        {
            playerTurn = true;
            Debug.Log("Coin: Head");
            Debug.Log("Player's turn!");
        }
        else
        {
            playerTurn = false;
            Debug.Log("Coin: Tails");
            Debug.Log("Computer's turn!");
        }

        return playerTurn;
    }*/

    private IEnumerator Delay()
    {
        Debug.Log("Waiting 5 seconds...");
        yield return new WaitForSeconds(5);
    }
}
