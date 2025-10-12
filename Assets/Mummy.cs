using UnityEngine;
using System.Collections;

public class Mummy : MonoBehaviour
{
    public float moveUpDistance = 4.5f;  // Distance à monter
    public float duration = 2f;          // Durée du mouvement (en secondes)
    private bool isMoving = false;

    public void RiseAndRotate()
    {
        if (!isMoving)
            StartCoroutine(RiseAndRotateCoroutine());
    }

    private IEnumerator RiseAndRotateCoroutine()
    {
        isMoving = true;

        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + Vector3.up * moveUpDistance;

        Quaternion startRot = transform.rotation;
        Quaternion targetRot = Quaternion.Euler(0f, 0f, 0f); // garde le Y actuel, remet X/Z à 0

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(startPos, targetPos, Mathf.SmoothStep(0f, 1f, t));
            transform.rotation = Quaternion.Slerp(startRot, targetRot, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        isMoving = false;
    }
}
