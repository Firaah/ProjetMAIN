using UnityEngine;

public enum PowerUpType { DamageBoost, Ally }

public class PowerUp : MonoBehaviour
{
    public float speed = 5f; // vitesse d'avancée sur Z-
    public float damageMultiplier = 2f; // pour DamageBoost
    private static int allyCount = 0; // pour savoir côté gauche/droite
    public GameObject allyPrefab; // assigner dans l’inspector
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
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger détecté avec : " + other.name);

        if (!other.CompareTag("Player")) return;
        Debug.Log("C’est bien le joueur !");
        if (!other.CompareTag("Player")) return;

        switch(type)
        {
            case PowerUpType.DamageBoost:
                ApplyDamageBoost(other.gameObject);
                break;

            case PowerUpType.Ally:
                SpawnAllyNextToPlayer(other.gameObject);
                break;
        }

        Destroy(gameObject);
    }

    void ApplyDamageBoost(GameObject player)
    {
        var shooter = player.GetComponent<LaserShooter>();
        if (shooter != null)
            shooter.damageMultiplier *= damageMultiplier; // multiplicateur cumulatif
    }

    

    void SpawnAllyNextToPlayer(GameObject player)
    {
        allyCount++;

        // Décide l'offset à gauche ou à droite
        Vector3 offset = allyCount == 1 ? new Vector3(-1.5f, 0, 0) : new Vector3(1.5f, 0, 0);

        // Instancie le prefab dédié aux alliés
        GameObject ally = Instantiate(allyPrefab, player.transform.position + offset, Quaternion.identity);

        // Ajouter le script de suivi
        FollowPlayer follow = ally.AddComponent<FollowPlayer>();
        follow.target = player.transform;
        follow.offset = offset;

        // Évite collision avec le joueur
        Collider allyCol = ally.GetComponent<Collider>();
        Collider playerCol = player.GetComponent<Collider>();
        if (allyCol != null && playerCol != null)
            Physics.IgnoreCollision(allyCol, playerCol);

        ally.SetActive(true);
    }

}
