using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // public static event Action<GameState> OnBeforeGameStateChange;

    public TextMeshProUGUI countText;
    public GameObject finalText;
    public GameObject finalScoreText;

    [SerializeField] private GameObject timeText;

    private float speed = 14;
    private int count;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private float secondsPassed = 0;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        finalText.SetActive(false);
        finalScoreText.SetActive(false);
        rb = GetComponent<Rigidbody>();
        count = 0;

        AudioManager.instance.PlaySound("BackgroundMusic");

        SetCountText();
    }

    void Update()
    {
        secondsPassed += Time.deltaTime;
        timeText.GetComponent<TextMeshProUGUI>().text = ((int) secondsPassed).ToString() + ":" + ((int) (secondsPassed * 100) % 100).ToString();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12){
            time = secondsPassed;
            GameOver();
        }
        
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            AudioManager.instance.PlaySound("PoolBall");
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }   
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {   
            gameObject.GetComponent<PlayerInput>().enabled = false;
            Invoke("GameOver", 1);            
        }
    }
}
