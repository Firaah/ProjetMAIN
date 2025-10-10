using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     // la capsule à spawn
    public int enemiesPerLine = 5;     // nombre d'ennemis par ligne
    public int linesPerWave = 10;      // nombre de lignes par vague
    public float spacingX = 2f;        // espacement horizontal entre les ennemis
    public float spacingZ = 3f;        // espacement entre les lignes
    public Vector3 startPosition = new Vector3(0, 1, 40); // point de départ de la première ligne

    public void SpawnWave()
    {
        for (int line = 0; line < linesPerWave; line++)
        {
            for (int i = 0; i < enemiesPerLine; i++)
            {
                // position de chaque ennemi dans la ligne
                float xPos = (i - (enemiesPerLine - 1) / 2f) * spacingX; // centre la ligne
                float zPos = startPosition.z + (line * spacingZ);
                Vector3 spawnPos = new Vector3(xPos, startPosition.y, zPos);

                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }
        }
    }

    // Pour tester facilement depuis l’éditeur
    void Start()
    {
        SpawnWave();
    }
}
