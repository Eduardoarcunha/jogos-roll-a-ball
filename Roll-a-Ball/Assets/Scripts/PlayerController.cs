using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private float speed = 13;
    private int count;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        winTextObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
        count = 0;

        AudioManager.instance.PlaySound("BackgroundMusic");

        
        SetCountText();
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

            winTextObject.SetActive(true);
        }
        
    }


    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")){
            AudioManager.instance.PlaySound("PoolBall");
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }  
        
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        GameManager.instance.ChangeState(GameManager.GameState.GameOver);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Check if it collided with an object tagged as "Enemy".
        {   
            // disable the player mov and then change the game state to game over
            gameObject.GetComponent<PlayerInput>().enabled = false;
            Invoke("GameOver", 3);            
        }
    }

}
