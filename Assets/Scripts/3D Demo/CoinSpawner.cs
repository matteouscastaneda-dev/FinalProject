using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Coin[] coinPrefabs;

    [Header("Spawn Area")]
    [SerializeField] private BoxCollider gameBounds;
    [SerializeField] private float spawnHeight = 1f;

    [Header("Timing")]
    [SerializeField] private float spawnInterval = 3f;

    [Header("Win Condition")]
    [SerializeField] private int scoreToWin = 12;

    private float nextSpawnTime;
    private int currentScore;
    private bool hasWon;
    private Bounds gameArea;

    public int CurrentScore { get { return currentScore; } }
    public int ScoreToWin { get { return scoreToWin; } }
    public bool HasWon { get { return hasWon; } }

    private void Start()
    {
        gameArea = gameBounds.bounds;
    }

    private void Update()
    {
        if (hasWon) return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnCoin();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    /// <summary>
    /// Picks a random coin prefab
    /// spawns in bounds 
    /// a reference back to this spawner
    /// </summary>
    private void SpawnCoin()
    {
        int prefabIndex = Random.Range(0, coinPrefabs.Length);
        Coin newSpawn = coinPrefabs[prefabIndex];

        float randomX = Random.Range(gameArea.min.x, gameArea.max.x);
        float randomZ = Random.Range(gameArea.min.z, gameArea.max.z);
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, randomZ);

        Coin spawned = Instantiate(newSpawn, spawnPosition, newSpawn.transform.rotation);
        spawned.Initialize(this);
    }

    /// <summary>
    /// Add to coin score and logs that and win
    /// </summary>
    public void AddScore(int value)
    {
        currentScore += value;
        Debug.Log("Score: " + currentScore + " / " + scoreToWin);

        if (!hasWon && currentScore >= scoreToWin)
        {
            hasWon = true;
            Debug.Log("You win!");
        }
    }
}