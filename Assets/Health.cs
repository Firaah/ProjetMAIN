using UnityEngine;

public class Health : MonoBehaviour
{
    public event System.Action<float> healthChange;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    public bool isDead => currentHealth <=0;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthChange != null)
        {
            healthChange(getNormalizedHealth());
        }
    }

    public float getNormalizedHealth()
    {
        return currentHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if(isDead) return;
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);

        if (healthChange != null)
        {
            healthChange(getNormalizedHealth());
        }

        // Vérifie si l'objet doit mourir
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // Ici tu peux ajouter un effet visuel, son, explosion, etc.
        Destroy(gameObject);
    }

    
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
   
}
