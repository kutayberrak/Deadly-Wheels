using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 30f;
    public int maxAmmo = 5;
    private int currentAmmo;
    private float fireCooldown = 1f; 
    private float cooldownTimer = 0f; 
    private GameObject currentTarget; 
    private bool isActive = true; 

    void Start()
    {
        currentAmmo = maxAmmo; 
    }

    void Update()
    {
        if (currentTarget && isActive)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                if (currentAmmo > 0)
                {
                    FireBullet();
                    cooldownTimer = fireCooldown; 
                }
                else
                {
                    isActive = false; 
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isActive)
        {
            currentTarget = other.gameObject; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && other.gameObject == currentTarget)
        {
            currentTarget = null; 
        }
    }

    void FireBullet()
    {
        if (currentTarget != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); 
            Vector3 targetPosition = currentTarget.transform.position + Vector3.up * 3f; 
            Vector3 direction = (targetPosition - firePoint.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed * 7; 
            currentAmmo--; 
        }
    }
}
