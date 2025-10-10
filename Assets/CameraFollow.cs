using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;          // le joueur
    public float smoothSpeed = 0.1f;  // vitesse de suivi
    public Vector3 offset = new Vector3(0, 15, -200); // position relative du joueur

    // bornes X pour la caméra
    public float camMinX = -8f;
    public float camMaxX = 8f;

    void LateUpdate()
    {
        // position cible : même Y/Z qu'offset, X du joueur
        Vector3 desiredPos = new Vector3(player.position.x, offset.y, offset.z);

        // clamp X pour ne pas dépasser les limites
        desiredPos.x = Mathf.Clamp(desiredPos.x, camMinX, camMaxX);

        // interpolation pour un mouvement fluide
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothedPos;
    }
}
