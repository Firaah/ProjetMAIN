using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f; // vitesse du joueur
    public float minX = -12.5f;  // limite gauche
    public float maxX = 12.5f;   // limite droite
    
    void Update()
    {
        float move = Input.GetAxis("Horizontal"); // A/D ou flèches
        Vector3 newPos = transform.position + Vector3.right * move * speed * Time.deltaTime;

        // Clamp pour rester entre les murs internes
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        transform.position = newPos;
    }
}
