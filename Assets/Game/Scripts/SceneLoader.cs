using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void OnHighScoresButtonClicked()
    {
        SceneManager.LoadScene("HighScores");
    }
    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
