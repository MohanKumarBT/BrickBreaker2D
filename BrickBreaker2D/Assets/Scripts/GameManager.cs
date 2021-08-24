using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class GameManager : MonoBehaviour
{
    public int score;
    public int lives;
    public Text scoretext;
    public Text scoretext1;
    public Text lifetext;
    public Text highscoreText;
    public GameObject gameOverCanvas;
    public GameObject loadingCanvas;
    public int numberofbricks;
    private bool isPaused;
    public GameObject pauseUI;
    public Transform[] levels;
    private int currenrLevelIndex = 0;
    public BallController ballController;


    void Start()
    {
        lifetext.text = "Lives: " + lives;
        scoretext.text = "Score: " + score;
        
        ballController = GameObject.Find("Ball").GetComponent<BallController>();
    }

    void Update()
    {
        numberofbricks = GameObject.FindGameObjectsWithTag("brick").Length + GameObject.FindGameObjectsWithTag("specialbrick").Length;
    }
    public void UpdateLives(int changeInLives)
    {
        lives += changeInLives;
        if(lives<= 0)
        {
            lives = 0;
            GameOver();
        }
        lifetext.text = "Lives: " + lives;
    }
    public void UpdateScore(int points)
    {
        score += points;
        scoretext.text = "Score: " + score;
    }
    public void UpdateNumberofBricks()
    {
        numberofbricks--;
        if (numberofbricks <= 0)
        {
            if (currenrLevelIndex >= levels.Length - 1)
            {
                GameOver();
            }
            else
            {
                ballController.inPlay = false;
                ballController.BallPosition();
                loadingCanvas.SetActive(true);
              
                Invoke ("LoadLevel", 3f);

            }
        }
    }
    void LoadLevel()
    {
        currenrLevelIndex++;
        Instantiate(levels[currenrLevelIndex], Vector2.zero, Quaternion.identity);
        loadingCanvas.SetActive(false);
    }
    void GameOver()
    {
        
        gameOverCanvas.SetActive(true);
        int highscore = PlayerPrefs.GetInt("HIGH SCORE");
        if(score > highscore)
        {
            PlayerPrefs.SetInt("HIGH SCORE", score);
            highscoreText.text = "New High Score! " + score;
;
        }
        else
        {
            scoretext1.text = "Your Score: " + score;
            highscoreText.text = "High Score: " + highscore;
        }
    }
    public void PauseButtonClick()
    {
        Time.timeScale = 0f;
        pauseUI.gameObject.SetActive(true);
        
    }
    public void ResumeButtonClick()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseUI.gameObject.SetActive(false);
    }
    public void Replay()
    {
        SceneManager.LoadScene("MainScene");
       
    }
    public void Home()
    {
        SceneManager.LoadScene("Lobby");

    }
}
