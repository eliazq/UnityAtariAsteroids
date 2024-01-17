using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject asteroid;

    private void Start()
    {
        InvokeRepeating("SpawnAsteroids", 0f, 2f);
    }

    private void SpawnAsteroids()
    {
        int asteroidSpawnAmount = Random.Range(1, 4);
        for (int i = 0; i < asteroidSpawnAmount; i++)
        {
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(asteroid, randomSpawnPoint.position, Quaternion.identity);
        }
        
    }
}
