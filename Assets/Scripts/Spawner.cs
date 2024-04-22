using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float velocityIntensity;

    public GameObject[] prefabs;
    public Transform[] spawnPoints;
    public float spawnTimer = 2;
    private float timer;

    private bool gameStarted = false;

    void Start()
    {
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
            if (timer > spawnTimer)
            {
                Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Length)];

                GameObject spawnedPrefab = Instantiate(randomPrefab, randomPoint.position, randomPoint.rotation);

                timer -= spawnTimer;

                Rigidbody rb = spawnedPrefab.GetComponent<Rigidbody>();
                rb.velocity = randomPoint.forward * velocityIntensity;
            }
        }
    }

    void StartSpawning()
    {
        gameStarted = true;
        Debug.Log("Spawning started!");
    }
}
