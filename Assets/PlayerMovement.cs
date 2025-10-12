using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f; // vitesse du joueur
    public float minX = -12.5f;  // limite gauche
    public float maxX = 12.5f;   // limite droite
    public float minZ = -120f;   // limite avant (loin)
    public float maxZ = -105f;   // limite arrière (près de la caméra)
    private bool canMoveForward = false; // au début, on ne peut pas avancer
    private bool movementEnabled = true; // permet d'activer/désactiver les mouvements
    public Rigidbody rb;

    void FixedUpdate() // FixedUpdate pour la physique
    {
        // 🚫 Si les mouvements sont désactivés, on quitte directement
        if (!movementEnabled)
            return;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = canMoveForward ? Input.GetAxis("Vertical") : 0f;

        Vector3 movement = new Vector3(moveX, 0f, moveZ) * speed * Time.fixedDeltaTime;
        Vector3 newPos = rb.position + movement;

        // Clamp les positions pour rester dans les limites
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.z = Mathf.Clamp(newPos.z, minZ, maxZ);

        rb.MovePosition(newPos);
    }

    // ✅ Appelée depuis le GameManager quand la pyramide est détruite
    public void EnableForwardMovement()
    {
        canMoveForward = true;
    }

    // ✅ Appelée depuis le GameManager pour bloquer ou débloquer les mouvements
    public void SetMovementEnabled(bool enabled)
    {
        movementEnabled = enabled;

        // Optionnel : on arrête instantanément la vélocité pour éviter un glissement
        if (!enabled)
            rb.linearVelocity = Vector3.zero;
    }
}

