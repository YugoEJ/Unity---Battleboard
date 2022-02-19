using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchComputer : MonoBehaviour
{
    public GameManagerScript board;
    public Player computer;
    public static int healthPoints;
    public bool appliedEffects;

    public GameObject WinText;
    public GameObject Draw;

    void Update()
    {
        if (board.duringMinigame && !appliedEffects)
        {
            healthPoints = 1 + computer.GetExtraLife();
            appliedEffects = true;
        }

        if (healthPoints != 0 && OnTouchPlayer.healthPoints != 0 && Timer.timeValue <= 0)
        {
            Draw.gameObject.SetActive(true);
            WinText.gameObject.SetActive(false);
            OnTouchPlayer.isGameOver = true;
            computer.RemoveMinigameWinner();
            StartCoroutine(DelayGoToBoard());
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (healthPoints == 0 && OnTouchPlayer.healthPoints != 0 && OnTouchPlayer.isGameOver == false) 
        {
            WinText.gameObject.SetActive(true);
            OnTouchPlayer.isGameOver = true;
            computer.RemoveMinigameWinner();
            StartCoroutine(DelayGoToBoard());
        }

        if (col.gameObject.tag == "obstacle" && OnTouchPlayer.isGameOver == false)
        {
            Destroy(col.gameObject);
            OnTouchPlayer.isGameOver = true;
            computer.RemoveExtraLife();
            board.boardUI.PCExtraLifeText.text = "Extra Life: " + computer.GetExtraLife();
            healthPoints--;
        }
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
        HideTexts();
        board.duringMinigame = false;
    }

    public void HideTexts()
    {
        WinText.gameObject.SetActive(false);
        Draw.gameObject.SetActive(false);
    }
}
