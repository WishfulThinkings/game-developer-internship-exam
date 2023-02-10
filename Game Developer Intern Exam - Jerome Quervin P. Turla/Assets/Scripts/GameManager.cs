using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("Countdown Timer")]
    public TextMeshProUGUI countdownTimerText;
    public float countdownTime;
    private bool isPaused;
    [Header("Buttons")]
    public GameObject retryButton;
    public GameObject exitButton;
    public GameObject pauseButton;
    public GameObject resumeButton;

    [Header("Highscore")]
    public int highScore;
    public TextMeshProUGUI highscoreText;

    public HoleMovement player;
    void Start()
    {
        highscoreText.text = PlayerPrefs.GetInt("highscore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        countdownTime -= Time.deltaTime;
        int seconds = (int)countdownTime % 60;
        countdownTimerText.text = seconds.ToString();

        if (countdownTime < 0)
        {
            SetActiveManager();
        }
   
        else if(countdownTime >0 && isPaused == false){ Time.timeScale = 1f; }

        if(player.score > highScore)
        {
            PlayerPrefs.SetInt("highscore", player.score);
            highScore = player.score;
            highscoreText.text = player.score.ToString();
        }
    }
    void SetActiveManager()
    {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        retryButton.SetActive(true);
        exitButton.SetActive(true);
    }

    public void UnPause()
    {
        isPaused = false;
        Time.timeScale = 1f;
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        resumeButton.SetActive(true);
        pauseButton.SetActive(false);
    }

}
