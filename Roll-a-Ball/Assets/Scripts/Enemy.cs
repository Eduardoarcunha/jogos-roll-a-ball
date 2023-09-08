using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private GameObject player;
    private Rigidbody rb;
    private Vector3 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        Vector3 direction = playerPosition - transform.position;
        rb.AddForce(direction.normalized * 2);

        // apply force to move towards player according to a speed


    }
}
