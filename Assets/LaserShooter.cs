using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public Transform firePoint;
    public GameObject laserPrefab;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;
    public float damage = 2f;
    public float damageMultiplier = 1f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            ShootAllLasers();
        }
    }

    void ShootAllLasers()
    {
        // Récupère tous les LaserShooters actifs dans la scène
        LaserShooter[] shooters = FindObjectsByType<LaserShooter>(FindObjectsSortMode.None);

        foreach (var shooter in shooters)
        {
            shooter.ShootLaser();
        }
    }

    public void ShootLaser()
    {
        GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        laser.SetActive(true);
    }
}
