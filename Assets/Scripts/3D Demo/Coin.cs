using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Coin : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private int value = 1;

    [Header("Idle Motion")]
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float bobSpeed = 1f;
    [SerializeField] private float minY = 0.5f;
    [SerializeField] private float maxY = 1.5f;

    private CoinSpawner spawner;
    private int bobDirection = 1;

    /// <summary>
    /// Receives a reference to the CoinSpawner 
    /// </summary>
    public void Initialize(CoinSpawner owningSpawner)
    {
        spawner = owningSpawner;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        float newY = transform.position.y + bobDirection * bobSpeed * Time.deltaTime;

        if (newY > maxY)
        {
            bobDirection = -1;
        }
        else if (newY < minY)
        {
            bobDirection = 1;
        }

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        spawner.AddScore(value);
        Destroy(gameObject);
    }
}