using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;

    public GameObject gamePanel;
    public GameObject endPanel;
    

    //private void Update()
    //{
    //    if(isPaused)
    //    {
    //        Time.timeScale = 0f;
    //    } else
    //    {
    //        Time.timeScale = 1f;

    //    }
    //}

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void FinishGame()
    {
        gamePanel.SetActive(false);
        endPanel.SetActive(true);

    }
}
