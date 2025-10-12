using UnityEngine;

public class RocketShooter : MonoBehaviour
{
    public Transform firePoint;
    public GameObject rocketPrefab;
    public bool hasBazooka = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&hasBazooka)
        {
            ShootRocket();
        }
    }


    public void ShootRocket()
    {
        GameObject rocket = Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);
        rocket.SetActive(true);
    }
}
