using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 3f;
    private int currentIndex = 0;

    // séquence fixe de types à faire apparaître
    public PowerUpType[] sequence = {
        PowerUpType.DamageBoost,
        PowerUpType.Ally,
        PowerUpType.DamageBoost,
        PowerUpType.Ally,
        PowerUpType.DamageBoost
    };

    public void SpawnPowerUp()
{
    // Si on a atteint la fin de la séquence, on arrête tout
    if (currentIndex >= sequence.Length)
    {
        Debug.Log("Fin de la séquence de power-ups !");
        return;
    }

    // Instanciation désactivée (évite toute collision ou Update avant config)
    GameObject pu = Instantiate(powerUpPrefab, spawnPoint.position, Quaternion.identity);
    pu.SetActive(false);

    PowerUp puScript = pu.GetComponent<PowerUp>();
    if (puScript != null)
    {
        // Assigne le type depuis la séquence
        puScript.SetType(sequence[currentIndex]);
    }

    // Active uniquement le mesh correspondant
    Transform dmgCube = pu.transform.Find("dmgPowerUp");
    Transform allyCube = pu.transform.Find("allyPowerUp");

    if (dmgCube != null) dmgCube.gameObject.SetActive(false);
    if (allyCube != null) allyCube.gameObject.SetActive(false);

    // Active le bon mesh selon le type
    if (puScript != null)
    {
        switch (puScript.type)
        {
            case PowerUpType.DamageBoost:
                if (dmgCube != null) dmgCube.gameObject.SetActive(true);
                break;

            case PowerUpType.Ally:
                if (allyCube != null) allyCube.gameObject.SetActive(true);
                break;
        }
    }

    // Active l'objet une fois tout configuré
    pu.SetActive(true);

    // Passe à l’élément suivant de la séquence
    currentIndex++;
}


}
