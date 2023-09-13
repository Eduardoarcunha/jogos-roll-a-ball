using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static event Action<GameObject> OnPickupCollected;
    public static event Action OnGameOver;

    private Rigidbody rb;

    private float movementX;
    private float movementY;
    private float speed = 14;
    private int count = 0;

    private bool gameEnd = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        AudioManager.instance.PlaySound("BackgroundMusic");
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
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
            OnPickupCollected?.Invoke(other.gameObject);
            count++;
        }   
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {   
            gameObject.GetComponent<PlayerInput>().enabled = false;
            if (!gameEnd){
                OnGameOver?.Invoke();
                gameEnd = true;
            }
        }
    }
}
