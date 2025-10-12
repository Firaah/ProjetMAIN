using UnityEngine;

public enum PowerUpType { DamageBoost, Ally }

public class PowerUp : MonoBehaviour
{
    public float speed = 5f; // vitesse d'avancée sur Z    
    
    public PowerUpType type { get; private set; }
    
    void Update()
    {
        // Déplacement vers Z-
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
    }
   

    public void SetType(PowerUpType newType)
    {
        type = newType;
    }
}
