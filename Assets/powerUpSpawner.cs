using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 3f;

    // séquence fixe de types à faire apparaître
    public PowerUpType[] sequence = {
        PowerUpType.DamageBoost,
        PowerUpType.Ally,
        PowerUpType.DamageBoost,
        PowerUpType.DamageBoost,
        PowerUpType.Ally,
        PowerUpType.DamageBoost
    };

    private int currentIndex = 0;
    private float nextSpawnTime = 0f;

    void Update()
    {
        if (currentIndex >= sequence.Length) return;

        if (Time.time >= nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnInterval;
            SpawnPowerUp(sequence[currentIndex]);
            currentIndex++;
        }
    }

    void SpawnPowerUp(PowerUpType type)
    {
        // Instanciation désactivée (évite toute collision ou Update avant config)
        GameObject pu = Instantiate(powerUpPrefab, spawnPoint.position, Quaternion.identity);
        pu.SetActive(false);

        PowerUp puScript = pu.GetComponent<PowerUp>();
        if (puScript != null)
            puScript.SetType(type);

        // Active uniquement le mesh correspondant
        Transform dmgCube = pu.transform.Find("dmgPowerUp");
        Transform allyCube = pu.transform.Find("allyPowerUp");

        if (dmgCube != null) dmgCube.gameObject.SetActive(type == PowerUpType.DamageBoost);
        if (allyCube != null) allyCube.gameObject.SetActive(type == PowerUpType.Ally);

        // // Assigne le layer "PowerUp" (utile pour ignorer les collisions entre eux)
        // int powerUpLayer = LayerMask.NameToLayer("PowerUp");
        // if (powerUpLayer >= 0)
        //     pu.layer = powerUpLayer;

        // Active l'objet une fois tout configuré
        pu.SetActive(true);
    }

}
