using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Spawners")]
    public EnemySpawner enemySpawner;
    public PowerUpSpawner powerUpSpawner;

    [Header("Timing")]
    public float delayBetweenWaves = 3f;        // Temps entre chaque vague
    private float delayBetweenPowerUp = 3f;     // Temps entre le spawn des vagues et le spawn des powerups
    private float delayBetweenLinesSpawn = 0.7f; // Délai entre lignes d'une vague
    private float delayBetweenPowerUpSpawn = 0.7f;   // Délai entre chaque PowerUp
    public GameObject gameOverScreen;

    
    private bool isGameOver = false;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(GameSequence());
    }

    private IEnumerator GameSequence()
    {
        Debug.Log("=== Début de la séquence ===");

        Debug.Log("Vague 1 - 3 lignes");
        yield return StartCoroutine(SpawnMultipleLines(3));
        Debug.Log("→ Fin vague 1");
        yield return new WaitForSeconds(delayBetweenPowerUp);

        Debug.Log("PowerUps après vague 1");
        yield return StartCoroutine(SpawnMultiplePowerUps(2));
        Debug.Log("→ Fin powerups 1");
        yield return new WaitForSeconds(delayBetweenWaves);

        Debug.Log("Vague 2 - 4 lignes");
        yield return StartCoroutine(SpawnMultipleLines(4));
        Debug.Log("→ Fin vague 2");
        yield return new WaitForSeconds(delayBetweenPowerUp);

        Debug.Log("PowerUps après vague 2");
        yield return StartCoroutine(SpawnMultiplePowerUps(1));
        Debug.Log("→ Fin powerups 2");
        yield return new WaitForSeconds(delayBetweenWaves);

        Debug.Log("Vague 3 - 5 lignes");
        yield return StartCoroutine(SpawnMultipleLines(5));
        Debug.Log("→ Fin vague 3");
        yield return new WaitForSeconds(delayBetweenPowerUp);

        Debug.Log("PowerUps après vague 3");
        yield return StartCoroutine(SpawnMultiplePowerUps(2));
        Debug.Log("→ Fin powerups 3");
        yield return new WaitForSeconds(delayBetweenWaves);

        Debug.Log("=== Boss ===");
        enemySpawner.SpawnBoss();
    }

    // ---------- Méthodes utilitaires ----------

    private IEnumerator SpawnMultipleLines(int count)
    {
        for (int i = 0; i < count; i++)
        {
            enemySpawner.SpawnLine();
            yield return new WaitForSeconds(delayBetweenLinesSpawn);
        }
    }

    private IEnumerator SpawnMultiplePowerUps(int count)
    {
        for (int i = 0; i < count; i++)
        {
            powerUpSpawner.SpawnPowerUp();
            yield return new WaitForSeconds(delayBetweenPowerUpSpawn);
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("💀 GAME OVER 💀");

        StopAllCoroutines();
        enemySpawner.enabled = false;
        powerUpSpawner.enabled = false;

        Time.timeScale = 0f;

        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);
    }

    
}
