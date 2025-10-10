using UnityEngine;

public class Health : MonoBehaviour
{
    
    public float maxHealth; // PV maximum
    private float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        // Limite à zéro
        currentHealth = Mathf.Max(currentHealth, 0f);

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
