using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string difficulty;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BtnNewGameOnClick()
    {
        SceneManager.LoadScene("play_difficulty");
    }

    public void BtnStartEasyGameOnClick()
    {
        difficulty = "Easy";
        PlayerPrefs.SetString("Difficulty", difficulty);
        SceneManager.LoadScene("game");
    }

    public void BtnStartMediumGameOnClick()
    {
        difficulty = "Medium";
        PlayerPrefs.SetString("Difficulty", difficulty);
        SceneManager.LoadScene("game");
    }

    public void BtnStartHardGameOnClick()
    {
        difficulty = "Hard";
        PlayerPrefs.SetString("Difficulty", difficulty);
        SceneManager.LoadScene("game");
    }

    public void BtnShowHighscoresOnClick()
    {
        SceneManager.LoadScene("highscores");
    }
    public void BtnSettingsOnClick()
    {
        SceneManager.LoadScene("settings");
    }

    public void BtnMenuOnClick()
    {
        SceneManager.LoadScene("main_menu");
    }

    public void BtnQuitOnClick()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

}
