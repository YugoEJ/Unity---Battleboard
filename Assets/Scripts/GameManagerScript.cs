using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public Player player;
    public Player computer;
    private Player currentPlayer;

    private bool gameOver;

    private int dice;
    private int stepsToTake;

    private void Start()
    {
        gameOver = false;
        currentPlayer = FlipCoin();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !player.isMoving && !gameOver && currentPlayer == player)
        {
            Move(player, computer);
        }

        if (!player.isMoving && !gameOver && currentPlayer == computer)
        {
            Move(computer, player);
        }
    }

    private void Move(Player currentPlayer, Player nextPlayer)
    {
        RollDice();

        if (currentPlayer.CanMove(stepsToTake))
        {
            StartCoroutine(currentPlayer.Move(stepsToTake));
            this.currentPlayer = nextPlayer;
        }
        else
        {
            StartCoroutine(currentPlayer.Move(stepsToTake));
            gameOver = true;
        }

        if (gameOver)
        {
            Debug.Log("Player wins!");
        }

        
    }

    private void RollDice()
    {
        dice = Random.Range(1, 7);
        stepsToTake = dice;

        Debug.Log("Dice rolled: " + stepsToTake);
    }

    private Player FlipCoin()
    {
        Player[] players = new Player[2];
        players[0] = player;
        players[1] = computer;

        Debug.Log("Flipping coin to decide who goes first!");
        int coin = Random.Range(0, 2);

        currentPlayer = players[coin];
        Debug.Log(currentPlayer.name + " goes first!");

        return currentPlayer;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3.0f);
    }
}
