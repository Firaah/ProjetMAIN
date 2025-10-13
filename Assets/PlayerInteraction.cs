using UnityEngine;
using System.Collections;
using TMPro; // si tu utilises TextMeshPro

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 1f; // distance max pour interagir
    private bool nearSarcophage = false;
    private int allyCount=0;
    public GameObject allyPrefab; // assigner dans l’inspector
    public LaserPrefab laserPrefab;
    public TMP_Text interactionText; // référence à ton texte "Appuyez sur E..."

    void Start()
    {
        if (interactionText != null)
            interactionText.gameObject.SetActive(false); // masqué au démarrage
    }

    void Update()
    {
        if (nearSarcophage && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interact pressed!");
            interactionText.gameObject.SetActive(false); // on cache le texte
            GameManager.Instance.StartCoroutine(GameManager.Instance.OpenSarchophage());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sarcophage"))
        {
            nearSarcophage = true;
            Debug.Log("Sarcophage à portée !");
            if (interactionText != null)
                interactionText.text = "Appuyez sur E pour intéragir";
                // interactionText.gameObject.SetActive(true); // affiche le texte
        }
        if(other.CompareTag("PowerUp"))
        {
            PowerUp powerUp = other.gameObject.GetComponent<PowerUp>();
            switch(powerUp.type)
            {
                case PowerUpType.DamageBoost:
                    ApplyDamageBoost();
                    break;

                case PowerUpType.Ally:
                    SpawnAllyNextToPlayer();
                    break;
            }
            Destroy(other.gameObject);
        }
    
    }

    private void ApplyDamageBoost()
    {
        LaserPrefab.damage *=2;
        ShowTMPMessage("Degats X2");
    }

    void SpawnAllyNextToPlayer()
    {
        allyCount++;
        ShowTMPMessage("+1 Allié");

        // Décide l'offset à gauche ou à droite
        Vector3 offset = allyCount == 1 ? new Vector3(-1.5f, 0, 0) : new Vector3(1.5f, 0, 0);

        // Instancie le prefab dédié aux alliés
        GameObject ally = Instantiate(allyPrefab, transform.position + offset, Quaternion.identity);

        // Ajouter le script de suivi
        FollowPlayer follow = ally.AddComponent<FollowPlayer>();
        follow.target = transform;
        follow.offset = offset;

        // Évite collision avec le joueur
        Collider allyCol = ally.GetComponent<Collider>();
        Collider playerCol = GetComponent<Collider>();
        if (allyCol != null && playerCol != null)
            Physics.IgnoreCollision(allyCol, playerCol);

        ally.SetActive(true);
    }

    public void ShowTMPMessage(string text)
    {
        StartCoroutine(ShowTMPMessageCoroutine(text));
    }

    private IEnumerator ShowTMPMessageCoroutine(string text)
    {
        interactionText.text = text;
        interactionText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f); // ← affiché 1 seconde
        interactionText.gameObject.SetActive(false);
    }
}
