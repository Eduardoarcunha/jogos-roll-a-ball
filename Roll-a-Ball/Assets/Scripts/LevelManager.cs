using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyPrefab;

    private float enemyCooldown = 5f;
    private float enemyCooldownRemaining;
    private bool enemySpawned = false;

    private float pickupCooldown = 5f;

    public static event Action OnIncreaseSpeed;
    public static event Action OnEnemySpawned;


    // Start is called before the first frame update
    void Start()
    {
        enemyCooldownRemaining = enemyCooldown;
        PlayerController.OnPickupCollected += SpawnPickup;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCooldownRemaining -= Time.deltaTime;

        if (enemyCooldownRemaining <= 0)
        {
            enemyCooldownRemaining = enemyCooldown;

            if (!enemySpawned)
            {
                SpawnEnemy();
                enemySpawned = true;
                OnEnemySpawned?.Invoke();

            } else {
                OnIncreaseSpeed?.Invoke();
            }
        }
    }

    void SpawnEnemy()
    {
        Vector3 random_position;
        GameObject enemy = Instantiate(enemyPrefab);

        do 
        {
            random_position = new Vector3(UnityEngine.Random.Range(-6,6), 6, UnityEngine.Random.Range(-6,6));
        } while (Vector3.Distance(random_position, player.transform.position) < 2.0f);
    
        enemy.transform.position = new Vector3(UnityEngine.Random.Range(-6,6), 6, UnityEngine.Random.Range(-6,6));
    }

    void SpawnPickup(GameObject pickup)
    {
        StartCoroutine(SpawnPickupCoroutine(pickup));
    }

    IEnumerator SpawnPickupCoroutine(GameObject pickup)
    {
        yield return new WaitForSeconds(pickupCooldown);
        pickup.SetActive(true);
    }

    void OnDestroy()
    {
        PlayerController.OnPickupCollected -= SpawnPickup;
    }
}
