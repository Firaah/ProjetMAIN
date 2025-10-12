using UnityEngine;

public class RocketPrefab : MonoBehaviour
{
    public float speed = 5f;       // vitesse de la rocket
    public float maxDistance = 100f; // distance max avant destruction
    public float radius = 0.1f;     // épaisseur pour la SphereCast

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        // Avancer la rocket
        transform.Translate(Vector3.forward * step, Space.Self);

        // SphereCast pour collision avec la pyramide
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, step))
        {
            if (hit.collider.CompareTag("Pyramid"))
            {
                // Ici tu peux ajouter un effet visuel/son
                Destroy(gameObject);              // détruit la rocket
                GameManager.Instance.pyramidDestroyed(hit.collider.gameObject);
            }
        }

        // Détruire si trop loin
        if (Vector3.Distance(startPos, transform.position) > maxDistance)
            Destroy(gameObject);
    }
}
