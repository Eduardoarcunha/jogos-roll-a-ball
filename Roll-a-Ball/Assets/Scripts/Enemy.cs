using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    private Vector3 playerPosition;
    private float speed = .5f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        playerPosition = player.transform.position;

        LevelManager.OnIncreaseSpeed += IncreaseSpeed;
    }

    void Update()
    {
        playerPosition = player.transform.position;
        Vector3 direction = playerPosition - transform.position;
        rb.AddForce(direction.normalized * speed);
    }

    void IncreaseSpeed()
    {
        speed += .25f;
    }

    void OnDestroy()
    {
        LevelManager.OnIncreaseSpeed -= IncreaseSpeed;
    }
}
