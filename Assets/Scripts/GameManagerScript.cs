using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public Scene boardScene;

    public Camera boardCam;
    public Camera minigameCam;

    public Player player;
    public Player computer;
    private Player currentPlayer;

    public DiceScript dice;

    public AudioSource BGM;
    public AudioSource diceSFX;
    public AudioSource stepOneSFX;
    public AudioSource stepTwoSFX;
    public AudioSource stepThreeSFX;
    public AudioSource stepFourSFX;

    private bool duringMinigame;
    private bool gamePaused;
    private bool gameOver;
    private bool skipTurn;
    private float speed = 40f;
    private int totalDiceRolls;
    private int stepsForMinigame;

    private void Start()
    {
        boardScene = SceneManager.GetActiveScene();
        stepsForMinigame = 25;
        totalDiceRolls = 0;
        currentPlayer = player;
        diceSFX.Play();
        BGM.Play();

        minigameCam.enabled = false;

        Debug.Log("Player goes first.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // game should be paused when a special effect is applied (item equipped, game initiated, etc.), for now, P will pause the game.
        {
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(boardScene.name);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            player.SetMinigameWinner();
            computer.SetMinigameWinner();
            minigameCam.enabled = false;
            boardCam.enabled = true;
            duringMinigame = false;
        }

        if (!gamePaused && !gameOver && !duringMinigame)
        {
            if (player.IsMinigameWinner() || computer.IsMinigameWinner())
            {
                if (player.IsMinigameWinner())
                {
                    player.RemoveMinigameWinner();
                    StartCoroutine(MoveWinningPlayer(player, 6));
                }

                if (computer.IsMinigameWinner())
                {
                    computer.RemoveMinigameWinner();
                    StartCoroutine(MoveWinningPlayer(computer, 6));
                }
            } 
            else
            {
                if (currentPlayer == player)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && !player.IsMoving() && !computer.IsMoving())
                    {
                        Move(player, computer);
                    }
                }
                else if (currentPlayer == computer)
                {
                    if (!computer.IsMoving() && !player.IsMoving())
                    {
                        Move(computer, player);
                    }
                }
            }
        }
    }

    private void Move(Player currentPlayer, Player nextPlayer)
    {
        diceSFX.Play();
        dice.Roll();

        StartCoroutine(MovePlayer(currentPlayer));

        // if current player CAN move, this means he hasn't reached the end, and the game will continue.
        if (currentPlayer.CanMove(DiceCheckZoneScript.StepsToTake()) && !this.duringMinigame) 
        {
            if (this.skipTurn == true)
            {
                this.currentPlayer = currentPlayer;
                this.skipTurn = false;
            }
            else
            {
                this.currentPlayer = nextPlayer;
            }
            //Debug.Log(nextPlayer.name + "'s turn.");

            StartCoroutine(NextTurnDelay());
            
        }
        else
        {
            // if this statement is true, currentPlayer is the winner.
            this.gameOver = true;
            Debug.Log(currentPlayer.name + " wins!");
            return;
        }
    }

    public IEnumerator MovePlayer(Player currentPlayer)
    {
        yield return new WaitForSeconds(3f);

        // the variable stepsToTake constantly changes within the function, whereas diceResult changes frequently in order to update this.totalDiceRolls
        int stepsToTake = DiceCheckZoneScript.StepsToTake();
        //this.diceResult = stepsToTake;
        this.totalDiceRolls += stepsToTake;

        Debug.Log(currentPlayer.name + " rolled: " + stepsToTake);

        if (currentPlayer.IsMoving())
        {
            yield break;
        }
        currentPlayer.SetMoving(true);

        // loop for the moving animation
        while (stepsToTake > 0 && (currentPlayer.RoutePos() != (currentPlayer.currentRoute.tileList.Count - 1)))
        {
            PlayStepSound();

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

        Debug.Log("Current total dice rolls: " + this.totalDiceRolls);

        ApplySpecialTileEffect(currentPlayer);

        if (currentPlayer == this.computer)
        {

            // if the minigame threshold (this.stepsForMinigame) is met, pause the game and switch to the minigame
            if (this.totalDiceRolls >= this.stepsForMinigame)
            {
                Debug.Log("Minigame should now begin because " + this.totalDiceRolls + " >= " + this.stepsForMinigame);

                this.stepsForMinigame += this.stepsForMinigame;
                this.boardCam.enabled = false;
                this.minigameCam.enabled = true;

                // BEGIN MINIGAME

                this.duringMinigame = true;
            }
        }
    }

    private void MoveToNextNode(Vector3 goalNode, Player currentPlayer)
    {
        currentPlayer.transform.position = Vector3.MoveTowards(currentPlayer.transform.position, goalNode, speed * Time.deltaTime);
    }

    private IEnumerator MoveWinningPlayer(Player winningPlayer, int stepsToTake)
    {
        yield return new WaitForSeconds(1f);

        Debug.Log(winningPlayer.name + " won the Minigame and will now move : " + stepsToTake + " steps.");

        if (winningPlayer.IsMoving())
        {
            yield break;
        }
        winningPlayer.SetMoving(true);

        // loop for the moving animation
        while (stepsToTake > 0 && (winningPlayer.RoutePos() != (winningPlayer.currentRoute.tileList.Count - 1)))
        {
            PlayStepSound();

            Vector3 initialPos = winningPlayer.currentRoute.tileList[winningPlayer.RoutePos()].position;

            winningPlayer.SetRoutePos(winningPlayer.RoutePos() + 1);

            Vector3 finalPos = winningPlayer.currentRoute.tileList[winningPlayer.RoutePos()].position;
            Vector3 nextPos = winningPlayer.transform.position;

            //--------first-------step--------------//

            // first step coordinates
            if (winningPlayer.IsEdgePos(winningPlayer.RoutePos() - 1))
            {
                nextPos.z = finalPos.z;
            }
            else
            {
                nextPos.x = finalPos.x;
            }
            nextPos.y = 8f;


            // first step (only Y changes)
            while (winningPlayer.transform.position != nextPos)
            {
                MoveToNextNode(nextPos, winningPlayer);
                yield return null;
            }

            //---------second-------step-------------//

            // second step coordinates
            if (winningPlayer.IsEdgePos(winningPlayer.RoutePos() - 1))
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
            while (winningPlayer.transform.position != nextPos)
            {
                MoveToNextNode(nextPos, winningPlayer);
                yield return null;
            }

            //---------third-----step----------------//

            // third step coordinates
            nextPos.x = finalPos.x;
            nextPos.y = 8f;
            nextPos.z = finalPos.z;

            // third step (Y changes to slightly lower, and X/Z changes to (finalPos.X/Z)
            while (winningPlayer.transform.position != nextPos)
            {
                MoveToNextNode(nextPos, winningPlayer);
                yield return null;
            }

            //---------final------step---------------//

            // final step coordinates
            nextPos.x = finalPos.x;
            nextPos.y = 5.5f;
            nextPos.z = finalPos.z;

            // final step (Y changes to original height (5.5f))
            while (winningPlayer.transform.position != nextPos)
            {
                MoveToNextNode(nextPos, winningPlayer);
                yield return null;
            }


            yield return new WaitForSeconds(0.1f);

            stepsToTake--;
        }

        winningPlayer.SetMoving(false);

        Debug.Log(winningPlayer.name + " has stopped moving after winning the Minigame.");

        ApplySpecialTileEffect(this.currentPlayer);

        this.currentPlayer = winningPlayer;

        //Debug.Log(nextPlayer.name + "'s turn.");

        StartCoroutine(NextTurnDelay());
    }

    private void ApplySpecialTileEffect(Player currentPlayer)
    {
        string specialEffect = currentPlayer.currentRoute.GetSpecialTiles()[currentPlayer.RoutePos()].GetTileEffect();
        Debug.Log(specialEffect);

        switch (specialEffect)
        {
            case "no-effect":
                break;

            case "extra-life":
                currentPlayer.AddExtraLife();
                break;

            case "super-speed":
                currentPlayer.GiveSuperSpeed();
                break;

            // if landed on skip-turn tile, the opponent may roll the dice twice. if skip-turn was already true, the opponent's skip-turn gets nullified.
            case "skip-turn":
                if (this.skipTurn && this.currentPlayer == computer)
                {
                    this.skipTurn = false;
                }
                else
                {
                    this.skipTurn = true;
                }
                break;
        }
    }

    private IEnumerator NextTurnDelay()
    {
        PauseGame();
        yield return new WaitForSeconds(3.75f);

        if (this.currentPlayer == this.player)
        {
            Debug.Log(this.currentPlayer.name + "'s turn.");
        }
        else
        {
            Debug.Log(this.currentPlayer.name + "'s turn.");
            yield return new WaitForSeconds(1);
        }

        PauseGame();
        yield return new WaitForSeconds(2.5f);
    }

    /*private Player FlipCoin()
    {
        Player[] players = new Player[2];
        players[0] = player;
        players[1] = computer;

        Debug.Log("Flipping coin to decide who goes first!");
        int coin = Random.Range(0, 2);

        currentPlayer = players[coin];
        //Debug.Log(currentPlayer.name + " goes first.");

        StartCoroutine(NextTurnDelay());

        return currentPlayer;
    }*/

    public void PauseGame()
    {
        gamePaused = !gamePaused;
    }

    public void PlayStepSound()
    {
        int sound = Random.Range(1, 5);

        switch (sound)
        {
            case 1: stepOneSFX.Play();
                break;

            case 2: stepTwoSFX.Play();
                break;

            case 3: stepThreeSFX.Play();
                break;

            case 4: stepFourSFX.Play();
                break;
        }
    }
}