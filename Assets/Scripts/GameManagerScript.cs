using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public DiceScript dice;
    //public DiceCheckZoneScript diceResult;

    public Player player;
    public Player computer;
    private Player currentPlayer;

    private bool gamePaused;
    private bool gameOver;
    private float speed = 40f;

    private void Start()
    {
        currentPlayer = FlipCoin();
    }

    private void Update()
    {
        if (!gamePaused && !gameOver)
        {
            if (currentPlayer == player)
            {
                if (Input.GetKeyDown(KeyCode.Space) && !player.IsMoving() && !computer.IsMoving())
                {
                    Move(player, computer);
                }
            }
            else
            {
                if (!computer.IsMoving() && !player.IsMoving())
                {
                    Move(computer, player);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P)) // game should be paused when a special effect is applied (item equipped, game initiated, etc.), for now, P will pause the game.
        {
            PauseGame();
        }
    }

    private void Move(Player currentPlayer, Player nextPlayer)
    {
        //RollDice();

        dice.Roll();

        StartCoroutine(MovePlayer(currentPlayer));

        // HERE we will check if the player landed on a special tile (with a method from within currentPlayer itself). if so, Pause the game and apply effect (buff/minigame/etc.), then Unpause.

        if (currentPlayer.CanMove()) // if current player CAN move, this means he hasn't reached the end, and the game can continue.
        {
            this.currentPlayer = nextPlayer;

            StartCoroutine(NextTurnDelay());
        }
        else
        {
            gameOver = true;
            Debug.Log(currentPlayer.name + " wins!");
            return;
        }
    }

    public IEnumerator MovePlayer(Player currentPlayer)
    {

        yield return new WaitForSeconds(3f);

        int stepsToTake = DiceCheckZoneScript.StepsToTake();
        Debug.Log(currentPlayer.name + " rolled: " + stepsToTake);

        if (currentPlayer.IsMoving())
        {
            yield break;
        }
        currentPlayer.SetMoving(true);

        while (stepsToTake > 0 && (currentPlayer.RoutePos() != (currentPlayer.currentRoute.tileList.Count - 1)))
        {
            Vector3 initialPos = currentPlayer.currentRoute.tileList[currentPlayer.RoutePos()].position;

            currentPlayer.SetRoutePos(currentPlayer.RoutePos() + 1);

            Vector3 finalPos = currentPlayer.currentRoute.tileList[currentPlayer.RoutePos()].position;
            Vector3 nextPos = currentPlayer.transform.position;

            //--------first-------step--------------//

            // first step coordinates
            if (currentPlayer.IsEdgePos(currentPlayer.RoutePos() - 1))
            {
                nextPos.z = finalPos.z;
            }
            else
            {
                nextPos.x = finalPos.x;
            }
            nextPos.y = 8f;


            // first step (only Y changes)
            while (currentPlayer.transform.position != nextPos)
            {
                MoveToNextNode(nextPos, currentPlayer);
                yield return null;
            }

            //---------second-------step-------------//

            // second step coordinates
            if (currentPlayer.IsEdgePos(currentPlayer.RoutePos() - 1))
            {
                nextPos.z = finalPos.z;
                nextPos.x = (finalPos.x + initialPos.x) / 2;
            }
            else
            {
                nextPos.x = finalPos.x;
                nextPos.z = (finalPos.z + initialPos.z) / 2;
            }
            nextPos.y = 12f;

            // second step (Y changes to slightly higher, and X/Z changes to ((initialPos.X/Z + finalPos.X/Z) / 2)
            while (currentPlayer.transform.position != nextPos)
            {
                MoveToNextNode(nextPos, currentPlayer);
                yield return null;
            }

            //---------third-----step----------------//

            // third step coordinates
            nextPos.x = finalPos.x;
            nextPos.y = 8f;
            nextPos.z = finalPos.z;

            // third step (Y changes to slightly lower, and X/Z changes to (finalPos.X/Z)
            while (currentPlayer.transform.position != nextPos)
            {
                MoveToNextNode(nextPos, currentPlayer);
                yield return null;
            }

            //---------final------step---------------//

            // final step coordinates
            nextPos.x = finalPos.x;
            nextPos.y = 5.5f;
            nextPos.z = finalPos.z;

            // final step (Y changes to original height (5.5f))
            while (currentPlayer.transform.position != nextPos)
            {
                MoveToNextNode(nextPos, currentPlayer);
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);

            stepsToTake--;
        }

        currentPlayer.SetMoving(false);

        yield return new WaitForSeconds(1.5f);

    }

    private void MoveToNextNode(Vector3 goalNode, Player currentPlayer)
    {
        currentPlayer.transform.position = Vector3.MoveTowards(currentPlayer.transform.position, goalNode, speed * Time.deltaTime);
    }

    private Player FlipCoin()
    {
        Player[] players = new Player[2];
        players[0] = player;
        players[1] = computer;

        Debug.Log("Flipping coin to decide who goes first!");
        int coin = Random.Range(0, 2);

        currentPlayer = players[coin];

        StartCoroutine(NextTurnDelay());

        return currentPlayer;
    }

    private IEnumerator NextTurnDelay()
    {
        PauseGame();
        yield return new WaitForSeconds(3 + ((currentPlayer.StepsToTake() / 2) + 2.5f));

        // CHECK IF CURRENT PLAYER LANDED ON SPECIAL TILE - IF SO, APPLY EFFECT.

        if (currentPlayer == player)
        {
            Debug.Log(currentPlayer.name + "'s turn.");
        }
        else
        {
            Debug.Log(currentPlayer.name + "'s turn.");
            yield return new WaitForSeconds(1);
        }

        PauseGame();
        yield return new WaitForSeconds(2.5f);
    }

    public void PauseGame()
    {
        gamePaused = !gamePaused;
    }
}