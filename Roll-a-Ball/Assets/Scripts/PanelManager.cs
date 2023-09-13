using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelManager : MonoBehaviour
{
    
    [SerializeField] private GameObject gamePanel;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] private GameObject timeText;

    [SerializeField] private GameObject engGamePanel;
    [SerializeField] private GameObject finalScoreText;
    [SerializeField] private GameObject finalText;
    [SerializeField] private GameObject button;

    private int count = 0;
    private float time = 0;
    private float secondsPassed = 0;
    
    void Start()
    {
        finalText.SetActive(false);
        finalScoreText.SetActive(false);
        
        GameManager.OnBeforeGameStateChange += ChangePanel;
        PlayerController.OnPickupCollected += IncreaseCount;
        PlayerController.OnGameOver += GameOver;

        countText.text = "Count: " + count.ToString();
    }

    void Update()
    {
        secondsPassed += Time.deltaTime;
        time = secondsPassed;
        timeText.GetComponent<TextMeshProUGUI>().text = ((int) secondsPassed).ToString() + ":" + ((int) (secondsPassed * 100) % 100).ToString();
    }

    private void IncreaseCount()
    {
        count++;
        countText.text = "Count: " + count.ToString();
    }
        
    void GameOver()
    {
        finalText.SetActive(true);
        finalScoreText.SetActive(true);

        if (count >= 12){
            finalText.GetComponent<TextMeshProUGUI>().text = "You Won! Try to improve your time!";
            finalScoreText.GetComponent<TextMeshProUGUI>().text = "Final Score: " + count.ToString() + "\nTime: " + ((int) time).ToString() + "." + ((int) (time * 100) % 100).ToString();
        } else {
            finalText.GetComponent<TextMeshProUGUI>().text = "You Lost!";
            finalScoreText.GetComponent<TextMeshProUGUI>().text = "Final Score: " + count.ToString();
        }

        GameManager.instance.ChangeState(GameManager.GameState.GameOver);
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
        PlayerController.OnPickupCollected -= IncreaseCount;
        PlayerController.OnGameOver -= GameOver;
    }
}
