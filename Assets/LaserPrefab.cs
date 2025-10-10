using UnityEngine;

public class LaserPrefab : MonoBehaviour
{
    public float speed = 100f;       // vitesse du laser
    public float maxDistance = 100f; // distance maximale avant destruction
    public float radius = 0.025f;
    public float damage = 1f;    // largeur du rayon pour SphereCast

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        // Avancer le laser
        transform.Translate(Vector3.forward * step, Space.Self);


        // Collision avec SphereCast pour correspondre à l'épaisseur
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, step))
        {
            if (hit.collider.CompareTag("Enemy"))
            {   
                Destroy(hit.collider.gameObject);
                Destroy(gameObject); // détruit le laser
            }
        }

        // Détruire si trop loin
        if (Vector3.Distance(startPos, transform.position) > maxDistance)
            Destroy(gameObject);
    }
}
