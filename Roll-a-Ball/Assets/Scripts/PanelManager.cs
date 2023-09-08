using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject gamePanel;
    

    [SerializeField] private GameObject engGamePanel;
    [SerializeField] private GameObject finalScoreText;
    [SerializeField] private GameObject finalText;

    [SerializeField] private GameObject button;

    void Start()
    {
        GameManager.OnBeforeGameStateChange += ChangePanel;
    }

    private void ChangePanel(GameManager.GameState newGameState)
    {
        switch (newGameState)
        {
            case GameManager.GameState.Menu:
                break;
            case GameManager.GameState.Play:
                engGamePanel.SetActive(false);
                gamePanel.SetActive(true);
                break;
            case GameManager.GameState.GameOver:
                EndPanel();
                break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
        }
    }

    void EndPanel()
    {
        gamePanel.SetActive(false);
        engGamePanel.SetActive(true);
        button.GetComponent<UnityEngine.UI.Button>().Select();
    }

    public void Restart()
    {
        GameManager.instance.ChangeState(GameManager.GameState.Play);
    }

    void OnDestroy()
    {
        GameManager.OnBeforeGameStateChange -= ChangePanel;
    }
}
