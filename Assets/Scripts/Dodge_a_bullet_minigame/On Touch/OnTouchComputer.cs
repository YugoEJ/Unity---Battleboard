using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchComputer : MonoBehaviour
{
    public GameManagerScript board;
    public Player computer;
    /*public GameObject WinText;
    public GameObject LoseText;
    bool loseTextAppears = false;
    bool winTextAppears = false;*/

    public GameObject WinText;
    public GameObject Draw;

    public static bool touchedComputer = false;
    //bool draw = false;

    // Start is called before the first frame update
    void Start()
    {
        /*LoseText.gameObject.SetActive(false);
        WinText.gameObject.SetActive(false);*/
    }

    // Update is called once per frame
    void Update()
    {
        if (touchedComputer == false && OnTouchPlayer.touchedPlayer == false && Timer.timeValue <= 0)
        {
            Draw.gameObject.SetActive(true);
            WinText.gameObject.SetActive(false);
            OnTouchPlayer.isGameOver = true;
            //boardCam();
            computer.RemoveMinigameWinner();
            StartCoroutine(DelayGoToBoard());
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "obstacle" && OnTouchPlayer.touchedPlayer == false && OnTouchPlayer.isGameOver == false) 
        {
            touchedComputer = true;
            WinText.gameObject.SetActive(true);
            //boardCam();
            computer.RemoveMinigameWinner();
            StartCoroutine(DelayGoToBoard());

        }

        /*if (touchedComputer == false && OnTouchPlayer.touchedPlayer == false && Timer.timeValue <= 0)
        {
            Draw.gameObject.SetActive(true);
        }*/

        //////////

        /*if (col.gameObject.tag == "obstacle") // Don't show win text for player even if there is a collision
        {
            WinText.gameObject.SetActive(true);
            winTextAppears = true;
        }
        else if (winTextAppears == false && RandomSpawner.obstacleCounter == 0)
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

        computer.RemoveSuperSpeed();
        board.minigameCam.enabled = false;
        board.boardCam.enabled = true;
        board.boardSFX.boardBGM.Play();
        board.boardSFX.minigameBGM.Stop();
        board.boardUI.ShowAllTexts();
        touchedComputer = false;
        HideTexts();
    }

    public void HideTexts()
    {
        WinText.gameObject.SetActive(false);
        Draw.gameObject.SetActive(false);
    }

/*    public void boardCam()
    {
        computer.RemoveSuperSpeed();
        board.minigameCam.enabled = true;
        board.boardCam.enabled = true;
        board.boardSFX.boardBGM.Play();
        board.boardSFX.minigameBGM.Stop();
        board.boardUI.ShowAllTexts();
    }*/
}
