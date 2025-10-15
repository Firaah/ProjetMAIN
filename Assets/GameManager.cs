using UnityEngine;
using System.Collections;
using TMPro;

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
    

    [Header("Références")]
    public GameObject gameOverScreen;
    public GameObject bazooka;
    public Transform player;
    public Camera mainCamera;
    public GameObject sarcophage;
    public RocketShooter rocketShooter;
    public Mummy amumu;
    public TMP_Text interactionText;
    
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

    public void BossDead(GameObject boss)
    {
        // 🔥 On détruit le boss
        Destroy(boss);

        // 🚀 On lance la séquence cinématique
        StartCoroutine(BossDeathSequence());
    }

    private IEnumerator BossDeathSequence()
    {   
        CameraController cam = mainCamera.GetComponent<CameraController>();
        GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
        player.GetComponent<PlayerMovement>().SetMovementEnabled(false);
        foreach (GameObject ally in allies)
            Destroy(ally);

        yield return new WaitForSeconds(0.5f);

        // 2️ Petit traveling caméra vers l’épaule droite du joueur
        yield return StartCoroutine(cam.TravelToShoulder(player));
        
        // 3️ Activation progressive du bazooka
        yield return StartCoroutine(ActivateBazookaSmooth());
        

        yield return new WaitForSeconds(1.8f);
        cam.isFollowing = true;
        cam.ActivateBazookaView();
        player.GetComponent<PlayerMovement>().SetMovementEnabled(true);
   

    }

    
    private IEnumerator ActivateBazookaSmooth()
    {
        if (bazooka == null)
        {
            Debug.LogWarning("Bazooka non assigné !");
            yield break;
        }

        bazooka.SetActive(true);

        Vector3 startPos = bazooka.transform.localPosition;
        Quaternion startRot = bazooka.transform.localRotation;

        Vector3 targetPos = startPos + new Vector3(0f, 0f, 0.5f);
        Quaternion targetRot = startRot * Quaternion.Euler(90f, 0f, 0f);

        float t = 0f;
        float duration = 1.5f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            bazooka.transform.localPosition = Vector3.Lerp(startPos, targetPos, t);
            bazooka.transform.localRotation = Quaternion.Slerp(startRot, targetRot, t);
            yield return null;
        }
        rocketShooter.hasBazooka = true;
        Debug.Log("Bazooka activé et positionné !");
        interactionText.text = "Appuyez sur ESPACE pour utiliser le bazooka";
        interactionText.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;


        StopAllCoroutines();
        enemySpawner.enabled = false;
        powerUpSpawner.enabled = false;

        Time.timeScale = 0f;

        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);
    }

    public void pyramidDestroyed(GameObject pyramid){
        interactionText.gameObject.SetActive(false);
        Destroy(pyramid);
        bazooka.SetActive(false);
        rocketShooter.hasBazooka = false;
        sarcophage.SetActive(true);
        player.GetComponent<PlayerMovement>().EnableForwardMovement();
        interactionText.text = "Avancez jusqu'au sarcophage";
        interactionText.gameObject.SetActive(true);
    }

    public IEnumerator OpenSarchophage()
    {
        SarcophageTop top = sarcophage.transform.Find("top").GetComponent<SarcophageTop>();
        player.GetComponent<PlayerMovement>().SetMovementEnabled(false);

        // ⏳ Lance l’ouverture du sarcophage et attend la fin
        yield return StartCoroutine(top.OpenSarchophage());
        interactionText.gameObject.SetActive(false);
        Debug.Log("Sarcophage ouvert !");
        
        // 🎥 Ensuite, la caméra se place
        CameraController cam = mainCamera.GetComponent<CameraController>();
        cam.SetFinalCamera();

        // 🧟‍♂️ Et enfin la momie s’anime
        amumu.RiseAndRotate();
        interactionText.text = "Felicitation vous avez liberé Amumu !";
        interactionText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        Time.timeScale = 0f;
        StopAllCoroutines();
        enemySpawner.enabled = false;
        powerUpSpawner.enabled = false;
    }


    
}

