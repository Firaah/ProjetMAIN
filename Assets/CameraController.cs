using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform target;         // le joueur à suivre
    public Vector3 offsetNormal = new Vector3(0, 15, -10);
    public Vector3 offsetBazooka = new Vector3(0f, 2f, -4f);
    public bool isFollowing = true; // permet d'activer/désactiver le suivi
    public float smoothSpeed = 5f;    
    private bool bazookaView = false;
    private bool isTraveling = false;

    void LateUpdate()
    {
        if (!isFollowing || target == null) return;

        // La position suit le joueur avec offset fixe
        Vector3 desiredPosition = target.position + (bazookaView ? offsetBazooka : offsetNormal);
        transform.position = desiredPosition;

        // Garde la même rotation que le joueur (optionnel)
        transform.rotation = Quaternion.Euler(35f, target.eulerAngles.y, target.eulerAngles.z);
        if (bazookaView)
            transform.LookAt(target.position + Vector3.up * 0.5f);
    }

    // --- Appelée quand le joueur sort le bazooka ---
    public void ActivateBazookaView()
    {
        bazookaView = true;
    }

    // --- Appelée quand il range le bazooka ---
    public void ResetView()
    {
        bazookaView = false;
    }

    public IEnumerator TravelToShoulder(Transform player, float duration = 1.5f, float holdTime = 2f)
    {
        if (isTraveling) yield break;
        isTraveling = true;

        // Désactive le suivi du joueur pendant la cinématique
        isFollowing = false;

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        // 🎯 Position cible : épaule droite du joueur
        Vector3 targetPos = player.position 
            + player.transform.right * 5f   // vers la droite
            + player.transform.up * 1.2f    // au-dessus
            - player.transform.forward * 5f; // léger recul

        // 🎥 Rotation pivotée de -45° sur Y
        Quaternion targetRot = Quaternion.LookRotation(Quaternion.Euler(0f, -45f, 0f) * player.forward, Vector3.up);

        float t = 0f;

        // ➡️ Transition vers la caméra épaule
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            yield return null;
        }
        isTraveling = false;
    }

    // ✅ --- Nouvelle fonction : placer la caméra à une position/rotation fixe ---
    public void SetFinalCamera()
    {
        isFollowing = false; // désactive le suivi du joueur
        transform.position = new Vector3(-10f, 8f, -120f);
        transform.rotation = Quaternion.Euler(10f, 0f, 0f);
    }
}
