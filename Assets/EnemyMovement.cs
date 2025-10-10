using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;      // vitesse de déplacement vers le joueur

    void Update()
    {
        // Avancer vers -Z
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        // transform.position += Vector3.forward * -speed * Time.deltaTime;


    }

}
