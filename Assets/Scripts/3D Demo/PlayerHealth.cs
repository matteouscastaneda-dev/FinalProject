using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;

    private int currentHealth;

    public int CurrentHealth { get { return currentHealth; } }
    public int MaxHealth { get { return maxHealth; } }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
     
    }

    /// <summary>
    /// Reduces health by the given amount 
    /// starts the invulnerability window.
    /// Logs a death message
    /// </summary>
    public void TakeDamage(int amount)
    {
        if (amount <= 0)
        {
            return;
        }
        currentHealth -= amount;

        Debug.Log("Player HP: " + currentHealth + " / " + maxHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player died");
        }
    }
}