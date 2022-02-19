using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchComputer : MonoBehaviour
{
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

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "obstacle" && OnTouchPlayer.touchedPlayer == false) 
        {
            touchedComputer = true;
            WinText.gameObject.SetActive(true);
        }

        if (touchedComputer == false && OnTouchPlayer.touchedPlayer == false && DeleteObstacleOnTouch.obstacleCounter == 0)
        {
            Draw.gameObject.SetActive(true);
        }

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
}
