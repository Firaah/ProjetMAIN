using UnityEngine;
using System.Collections;

public class SarcophageTop : MonoBehaviour
{
    public float openDistance = 2f;
    public float duration = 2f;
    private bool isOpen = false;

    public IEnumerator OpenSarchophage()
    {
        if (isOpen) yield break;
        isOpen = true;

        Vector3 startPos = transform.localPosition;
        Vector3 targetPos = startPos + Vector3.forward * openDistance;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.localPosition = Vector3.Lerp(startPos, targetPos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        Debug.Log("✅ Couvercle du sarcophage ouvert !");
    }
}
