using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static event Action<GameState> OnBeforeGameStateChange;
    public static event Action<GameState> OnAfterGameStateChange;
    
    public GameState gameState { get; private set;}

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ChangeState(GameState.Menu);
    }

    public void ChangeState(GameState newGameState)
    {
        if (gameState == newGameState) return;

        OnBeforeGameStateChange?.Invoke(newGameState);

        gameState = newGameState;
        switch (gameState)
        {
            case GameState.Menu:
                break;
            case GameState.Play:
                SceneManager.LoadScene("Minigame");
                break;
            case GameState.GameOver:
                GameOver();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
        }
        
        OnAfterGameStateChange?.Invoke(gameState);
    }

    public enum GameState
    {
        Menu = 0,
        Play = 1,
        GameOver = 2
    }

    public void GameOver()
    {
        AudioManager.instance.StopSound("BackgroundMusic");
        SceneManager.LoadScene("GameOver");
    }
}