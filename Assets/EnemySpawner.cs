using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     // Capsule à spawn
    public GameObject bossPrefab;      // Prefab du boss à assigner dans l’inspector
    public int enemiesPerLine = 5;     // Nombre d'ennemis par ligne
    public int linesPerWave = 10;      // Nombre de lignes par vague
    public float spacingX = 2f;        // Espacement horizontal entre les ennemis
    public float spacingZ = 3f;        // Espacement entre les lignes
    public Vector3 startPosition = new Vector3(0, 1, 40); // Point de départ de la première ligne

    public void SpawnWave()
    {
        for (int line = 0; line < linesPerWave; line++)
        {
            for (int i = 0; i < enemiesPerLine; i++)
            {
                float xPos = (i - (enemiesPerLine - 1) / 2f) * spacingX;
                float zPos = startPosition.z + (line * spacingZ);
                Vector3 spawnPos = new Vector3(xPos, startPosition.y, zPos);

                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }
        }
    }

    public void SpawnBoss()
    {
        // Position du boss : au centre et un peu plus loin que la dernière ligne
        Vector3 bossPos = new Vector3(0, startPosition.y, startPosition.z + linesPerWave * spacingZ + 5f);

        GameObject boss = Instantiate(bossPrefab, bossPos, Quaternion.identity);
        boss.SetActive(true);
                
    }

    void Start()
    {
        SpawnWave();

        // On peut spawn le boss après un délai ou immédiatement
        SpawnBoss();
    }
}
