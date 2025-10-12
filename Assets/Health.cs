using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public event System.Action<float> healthChange;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    public GameObject bazooka; // le bazooka du joueur

    public bool isDead => currentHealth <= 0;

    void Start()
    {
        currentHealth = maxHealth;
        healthChange?.Invoke(getNormalizedHealth());
    }

    public float getNormalizedHealth()
    {
        return currentHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
        healthChange?.Invoke(getNormalizedHealth());

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // Lance la séquence de mort complète
        GameManager.Instance.BossDead(gameObject);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
