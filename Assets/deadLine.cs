using UnityEngine;

public class deadLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")||other.CompareTag("Boss"))
        {
            Debug.Log("Un ennemi a franchi la ligne !");
            GameManager.Instance.GameOver();
        }
    }
}
