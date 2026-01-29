using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] private Canvas pauseMenuCanvas;

    void OnEnable()
    {
        InputManager.OnPauseInput += StartMyCoroutine;
    }

    void OnDisable()
    {
        InputManager.OnPauseInput -= StartMyCoroutine;
    }

    void Start()
    {
        
    }

    void StartMyCoroutine()
    {
        StartCoroutine(PauseMenuOpen());
    }

    IEnumerator PauseMenuOpen()
    {
        yield return new WaitForEndOfFrame();

        if (GameManager.gameState == GameManager.GameState.Pause)       // Pause menüsü açýksa...
        {
            pauseMenuCanvas.gameObject.SetActive(true);                    // Kapat
        }
        else if(GameManager.gameState == GameManager.GameState.Playing) // Pause menüsü kapaliysa...
        {
            pauseMenuCanvas.gameObject.SetActive(false);                   // Aç
        }
    }
}
