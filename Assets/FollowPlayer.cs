using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target; // le joueur à suivre
    public Vector3 offset;   // position relative (ex: (-1,0,0) ou (1,0,0))

    void LateUpdate()
    {
        if (target == null) return;

        // La position suit le joueur avec offset fixe
        transform.position = target.position + offset;

        // (Optionnel) garde la même rotation
        transform.rotation = target.rotation;
    }
}
