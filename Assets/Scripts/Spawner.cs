using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float velocityIntensity;

    public GameObject[] prefabs;
    public Transform[] spawnPoints;
    public float initialSpawnTimer = 2f; // Initial spawn timer (2 seconds)
    public float minSpawnTimer = 1f; // Minimum spawn timer (1 second)
    public float spawnTimerDecreaseRate = 0.05f; // Rate at which spawn timer decreases over time
    private float currentSpawnTimer; // Current spawn timer
    private float timer;

    private bool gameStarted = false;

    void Start()
    {
        currentSpawnTimer = initialSpawnTimer;
        CountdownTimer.OnCountdownComplete += StartSpawning;
    }

    void OnDestroy()
    {
        CountdownTimer.OnCountdownComplete -= StartSpawning;
    }

    void Update()
    {
        if (gameStarted)
        {
            timer += Time.deltaTime;
            if (timer > currentSpawnTimer)
            {
                Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Length)];

                GameObject spawnedPrefab = Instantiate(randomPrefab, randomPoint.position, randomPoint.rotation);

                timer -= currentSpawnTimer;

                Rigidbody rb = spawnedPrefab.GetComponent<Rigidbody>();
                rb.velocity = randomPoint.forward * velocityIntensity;
            }
        }
    }

    void StartSpawning()
    {
        gameStarted = true;
        Debug.Log("Spawning started!");
        StartCoroutine(DecreaseSpawnTimer());
        StartCoroutine(StopSpawning());
    }

    IEnumerator DecreaseSpawnTimer()
    {
        while (currentSpawnTimer > minSpawnTimer)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second before decreasing spawn timer
            currentSpawnTimer -= spawnTimerDecreaseRate;
            currentSpawnTimer = Mathf.Max(currentSpawnTimer, minSpawnTimer); // Ensure spawn timer doesn't go below the minimum
        }
    }

    IEnumerator StopSpawning()
    {
        yield return new WaitForSeconds(120f); // Wait for 2 minutes (end of the game)
        gameStarted = false;
        Debug.Log("Spawning stopped!");
    }
}
