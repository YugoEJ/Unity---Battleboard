using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private int InstructionsScene = 0;
    private int GameScene = 1;

    public void StartGame()
    {
        SceneManager.LoadScene(GameScene);
    }

    public void Instructions()
    {
        SceneManager.LoadScene(InstructionsScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}