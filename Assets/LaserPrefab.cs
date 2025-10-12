using UnityEngine;

public class LaserPrefab : MonoBehaviour
{
    public float speed = 100f; 
    public float maxDistance = 100f; 
    public float radius = 0.025f;
    public static float damage = 1f;    
    private Renderer rend;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        rend = GetComponentInChildren<Renderer>();
        if (rend != null)
            UpdateColor();
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
            if(!hit.collider.CompareTag("Player")){
                if (hit.collider.CompareTag("Enemy"))
                {   
                    Destroy(hit.collider.gameObject);
                }
                if (hit.collider.CompareTag("Boss"))
                {
                    Health bossHealth = hit.collider.GetComponent<Health>();
                    if (bossHealth != null)
                    {
                        bossHealth.TakeDamage(damage);
                    }
                }
                Destroy(gameObject);
            }
        }

        // Détruire si trop loin
        if (Vector3.Distance(startPos, transform.position) > maxDistance)
            Destroy(gameObject);
    }

    public void UpdateColor()
    {
        if (rend == null) return;

        switch ((int)damage)
        {
            case 1:
                rend.material.color = new Color(0f, 1f, 0f); // vert fluo
                break;
            case 2:
                rend.material.color = new Color(0f, 1f, 1f); // cyan fluo
                break;
            case 4:
                rend.material.color = new Color(1f, 0f, 1f); // magenta fluo
                break;
            case 8:
                rend.material.color = new Color(1f, 0f, 0f); // rouge fluo
                break;
            default:
                rend.material.color = Color.white; // par défaut
                break;
        }
    }
}
