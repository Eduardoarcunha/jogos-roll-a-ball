using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelManager : MonoBehaviour
{
    
    [SerializeField] private GameObject gamePanel;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] private GameObject popUp;

    [SerializeField] private GameObject engGamePanel;
    [SerializeField] private GameObject finalScoreText;
    [SerializeField] private GameObject finalText;
    [SerializeField] private GameObject button;


    private int count = 0;
    private GameObject popUpText;
    
    void Start()
    {
        GameManager.OnBeforeGameStateChange += ChangePanel;
        PlayerController.OnPickupCollected += IncreaseCount;
        PlayerController.OnGameOver += GameOver;
        LevelManager.OnEnemySpawned += EnemySpawned;
        LevelManager.OnIncreaseSpeed += SpeedPopup;

        finalText.SetActive(false);
        finalScoreText.SetActive(false);
        countText.text = "Count: " + count.ToString();
        popUpText = popUp.transform.GetChild(0).gameObject;
    }

    private void IncreaseCount(GameObject pickup)
    {
        count++;
        countText.text = "Count: " + count.ToString();
    }
        
    void GameOver()
    {
        finalText.SetActive(true);
        finalScoreText.SetActive(true);

        
        finalText.GetComponent<TextMeshProUGUI>().text = "Game End!";
        finalScoreText.GetComponent<TextMeshProUGUI>().text = "Final Score: " + count.ToString();


        GameManager.instance.ChangeState(GameManager.GameState.GameOver);
    }

    void EnemySpawned()
    {
        StartCoroutine(EnemyPopup());
    }

    void SpeedPopup()
    {
        StartCoroutine(SpeedPopupCoroutine());
    }

    IEnumerator EnemyPopup()
    {
        popUp.SetActive(true);
        popUpText.GetComponent<TextMeshProUGUI>().text = "Enemy Spawned!";
        yield return new WaitForSeconds(1);
        popUp.SetActive(false);
    }

    IEnumerator SpeedPopupCoroutine()
    {
        popUp.SetActive(true);
        popUpText.GetComponent<TextMeshProUGUI>().text = "Enemy got faster!";
        yield return new WaitForSeconds(1);
        popUp.SetActive(false);
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
        LevelManager.OnEnemySpawned -= EnemySpawned;
        LevelManager.OnIncreaseSpeed -= SpeedPopup;
    }
}
