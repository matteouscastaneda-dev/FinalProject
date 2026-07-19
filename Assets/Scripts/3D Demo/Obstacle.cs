using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 3.5f;

    [Header("Damage")]
    [SerializeField] private int damageAmount = 1;

    private ObstacleSpawner spawner;
    private GameObject playerReference;
    private PlayerHealth playerHealth;

    /// <summary>
    /// Receives references to the owning spawner and the player from
    /// ObstacleSpawner immediately after Instantiate. Caches PlayerHealth
    /// so on-contact damage doesn't need a GetComponent per hit.
    /// </summary>
    public void Initialize(ObstacleSpawner owningSpawner, GameObject player)
    {
        spawner = owningSpawner;
        playerReference = player;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        Vector3 direction = playerReference.transform.position - transform.position;
        direction.y = 0f;
        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;
    }

    /// <summary>
    /// Damages the player when this obstacle's trigger collider overlaps
    /// the player. Non-player overlaps are ignored via the Player tag.
    /// </summary>
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damageAmount);
        }
        Destroy(gameObject);
    }
}