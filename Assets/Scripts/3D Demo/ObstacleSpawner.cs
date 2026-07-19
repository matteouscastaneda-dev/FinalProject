using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private Obstacle obstaclePrefab;

    [Header("References")]
    [SerializeField] private GameObject playerReference;
    [SerializeField] private BoxCollider gameBounds;
    [SerializeField] private float spawnHeight = 0.5f;

    [Header("Timing")]
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private int maxCount = 5;

    private readonly List<Obstacle> activeObstacles = new List<Obstacle>();
    private Bounds gameArea;

    private void Start()
    {
        gameArea = gameBounds.bounds;
        StartCoroutine(SpawnLoop());
    }

    private void Update()
    {
        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            if (activeObstacles[i] == null)
            {
                activeObstacles.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Spawns one obstacle each interval while under the cap
    /// Until this GameObject is destroyed.
    /// </summary>
    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (activeObstacles.Count < maxCount)
            {
                SpawnObstacle();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    /// <summary>
    /// Spawns an obstacle at a random position in bounds
    /// Initialize and registers it in the active list
    /// </summary>
    private void SpawnObstacle()
    {
        float randomX = Random.Range(gameArea.min.x, gameArea.max.x);
        float randomZ = Random.Range(gameArea.min.z, gameArea.max.z);
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, randomZ);

        Obstacle newSpawn = Instantiate(obstaclePrefab, spawnPosition, obstaclePrefab.transform.rotation);
        newSpawn.Initialize(this, playerReference);
        activeObstacles.Add(newSpawn);
    }
}