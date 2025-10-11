using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     // Capsule à spawn
    public GameObject bossPrefab;   // Prefab du boss à assigner dans l’inspector
    public GameObject bossHealthBarPrefab;     
    public Transform canvasHUD; 
    public int enemiesPerLine = 5;     // Nombre d'ennemis par ligne
    public float spacingX = 2f;        // Espacement horizontal entre les ennemis
    public Vector3 startPosition = new Vector3(0, 1, 40); // Point de départ de la première ligne

    public void SpawnLine()
    {
            for (int i = 0; i < enemiesPerLine; i++)
            {
                float xPos = (i - (enemiesPerLine - 1) / 2f) * spacingX;
                Vector3 spawnPos = new Vector3(xPos, startPosition.y, 40);
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }
    }

    public void SpawnBoss()
{
    // 1. Spawn le boss
    GameObject boss = Instantiate(bossPrefab, startPosition, Quaternion.identity);
    boss.SetActive(true);

    // 2. Récupère le Health du boss
    Health bossHealth = boss.GetComponent<Health>();
    if (bossHealth == null)
    {
        Debug.LogError("Le boss n’a pas de script Health !");
        return;
    }

    // 3. Spawn la barre dans le Canvas HUD
    GameObject hpBarObj = Instantiate(bossHealthBarPrefab, canvasHUD);
    hpBarObj.SetActive(true);
    Debug.Log("Activation de : " + hpBarObj.name + " | ActiveSelf=" + hpBarObj.activeSelf + " | ActiveInHierarchy=" + hpBarObj.activeInHierarchy);



    // 4. Lie la barre au boss
    BossHealthBar hpBar = hpBarObj.GetComponent<BossHealthBar>();
    if (hpBar != null)
    {
        hpBar.bossHealth = bossHealth;
        hpBar.UpdateBarInstant();
    }
    else
    {
        Debug.LogError("Le prefab n’a pas de script BossHealthBar !");
    }

    // 5. (Optionnel) Forcer le Canvas à se mettre à jour visuellement
    Canvas.ForceUpdateCanvases();
}

}
