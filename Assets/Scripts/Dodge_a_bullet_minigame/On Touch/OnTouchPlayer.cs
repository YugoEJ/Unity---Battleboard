using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchPlayer : MonoBehaviour
{
    /*public GameObject WinText;
    public GameObject LoseText;
    bool loseTextAppears = false;
    bool winTextAppears = false;*/

    public GameManagerScript board;
    public Player player;

    public GameObject WinText;
    public GameObject LoseText;
    public GameObject Draw;

    public static bool isGameOver = false;

    public static bool touchedPlayer;

    // Start is called before the first frame update
    void Start()
    {
        /*LoseText.gameObject.SetActive(false);
        WinText.gameObject.SetActive(false);*/
    }

    // Update is called once per frame
    void Update()
    {

        if (touchedPlayer == false && OnTouchComputer.touchedComputer == false && Timer.timeValue <= 0)
        {
            Draw.gameObject.SetActive(true);
            WinText.gameObject.SetActive(false);
            LoseText.gameObject.SetActive(false);
            isGameOver = true;
            player.RemoveMinigameWinner();
            StartCoroutine(DelayGoToBoard());
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        //Debug.Log(OnTouchComputer.touchedComputer);

        if (col.gameObject.tag == "obstacle")
        {
            touchedPlayer = true;
        }

        if (touchedPlayer == false && OnTouchComputer.touchedComputer == true && isGameOver == false)
        {
            WinText.gameObject.SetActive(true);
            player.SetMinigameWinner();
            //boardCam();
            StartCoroutine(DelayGoToBoard());

        }

        if (touchedPlayer == true && OnTouchComputer.touchedComputer == false && isGameOver == false)
        {
            LoseText.gameObject.SetActive(true);
            player.RemoveMinigameWinner();
            //boardCam();
            StartCoroutine(DelayGoToBoard());
        }

        

        
        ////////////

        /*if (col.gameObject.tag == "obstacle" && OnTouchComputer.touchedComputer == false)
        {
            LoseText.gameObject.SetActive(true);
        }*/

        /*if (col.gameObject.tag == "rightBound" || col.gameObject.tag == "leftBound")
        {
            LoseText.gameObject.SetActive(false);
            WinText.gameObject.SetActive(false);
        }*/

        /*if (col.gameObject.tag == "obstacle")
        {
            LoseText.gameObject.SetActive(true);
            loseTextAppears = true;

        }
        else if (loseTextAppears == false && RandomSpawner.obstacleCounter == 0)
        {
            WinText.gameObject.SetActive(true);
            winTextAppears = true;
        }

        if (winTextAppears == true)
        {
            LoseText.gameObject.SetActive(false);
            loseTextAppears = false;
        }*/
    }

    public IEnumerator DelayGoToBoard()
    {
        yield return new WaitForSeconds(3f);

        player.RemoveSuperSpeed();
        board.minigameCam.enabled = false;
        board.boardCam.enabled = true;
        board.boardSFX.boardBGM.Play();
        board.boardSFX.minigameBGM.Stop();
        board.boardUI.ShowAllTexts();
        touchedPlayer = false;
        HideTexts();
    }

    public void HideTexts()
    {
        WinText.gameObject.SetActive(false);
        LoseText.gameObject.SetActive(false);
        Draw.gameObject.SetActive(false);
    }

    /*    public void boardCam()
        {
            player.RemoveSuperSpeed();
            board.minigameCam.enabled = false;
            board.boardCam.enabled = true;
            board.boardSFX.boardBGM.Play();
            board.boardSFX.minigameBGM.Stop();
            board.boardUI.ShowAllTexts();
        }*/


}
