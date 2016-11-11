using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum GameState
{
    start,
    inGame,
    gameOver,
    levelComplete,
    youWin
}

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameState currentGameState = GameState.start;

    public GameObject StartScreen;
    public GameObject InGameScreen;
    public GameObject GameOverScreen;
    public GameObject YouWonScreen;

    public int score;
    // Use this for initialization
    void Awake()
    {
        instance = this;
    }

    public void StartGame ()
    {
        SetGameState(GameState.inGame);
        TileGenerator.instance.StartGame();
	}

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void YouWon()
    {
        SetGameState(GameState.youWin);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.start)
        {
            StartScreen.SetActive(true);
            InGameScreen.SetActive(false);
            GameOverScreen.SetActive(false);
            YouWonScreen.SetActive(false);
        }
        else if (newGameState == GameState.inGame)
        {
            StartScreen.SetActive(false);
            InGameScreen.SetActive(true);
            GameOverScreen.SetActive(false);
            YouWonScreen.SetActive(false);
        }
        else if (newGameState == GameState.levelComplete)
        {
            StartScreen.SetActive(false);
            InGameScreen.SetActive(false);
            GameOverScreen.SetActive(false);
            YouWonScreen.SetActive(false);
        }
        else if (newGameState == GameState.gameOver)
        {
            StartScreen.SetActive(false);
            InGameScreen.SetActive(true);
            GameOverScreen.SetActive(true);
            YouWonScreen.SetActive(false);
        }
        else if (newGameState == GameState.youWin)
        {
            StartScreen.SetActive(false);
            InGameScreen.SetActive(false);
            GameOverScreen.SetActive(false);
            YouWonScreen.SetActive(true);
        }
        currentGameState = newGameState;
    }
    // Update is called once per frame
    void Update () {
	
	}
}
