using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager.GameState gameState;

    void OnEnable()
    {
        InputManager.OnPauseInput += PauseGame;
    }

    void OnDisable()
    {
        InputManager.OnPauseInput -= PauseGame;
    }

    void Start()
    {
        gameState = GameState.Playing;
    }

    void Update()
    {
        ChangeGameState();
    }

    #region Functions

    void PauseGame()
    {
        if (gameState == GameState.Playing)
        {
            gameState = GameState.Pause;
        }
        else if (gameState == GameState.Pause)
        {
            gameState = GameState.Playing;
        }
    }

    /*-----------------------------*/

    void ChangeGameState()
    {
        if (gameState == GameState.Playing)
        {
            Time.timeScale = 1f;
        }

        else if (gameState == GameState.Pause)
        {
            Time.timeScale = 0f;
        }
    }

    #endregion

    #region Enums

    public enum GameState
    {
        Playing,
        Pause,
        Finish
    }

    #endregion
}
